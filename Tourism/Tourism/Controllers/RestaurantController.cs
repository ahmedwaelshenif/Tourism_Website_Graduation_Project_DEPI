using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Security.Claims;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.Repository;
using Tourism.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Table = Tourism.Models.Table;

namespace Tourism.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class RestaurantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICreditCardRepository _creditcardRepository;
        private readonly IMealRepository _mealRepository;
        private readonly ITableRepository _tableRepository;
        public RestaurantController(IRestaurantRepository restaurantRepository, ICreditCardRepository creditcardRepository, IUnitOfWork unitOfWork, IMealRepository mealRepository, ITableRepository tableRepository)
        {
            _restaurantRepository = restaurantRepository;
            _creditcardRepository = creditcardRepository;
            _unitOfWork = unitOfWork;
            _mealRepository = mealRepository;
            _tableRepository = tableRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View(new RestaurantSignUpViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(RestaurantSignUpViewModel restaurantSignUp)
        {
            if (!ModelState.IsValid)
                return View(restaurantSignUp);


            var card = _creditcardRepository.GetCreditCard(
            restaurantSignUp.creditCard.CardNumber,
            restaurantSignUp.creditCard.CVV,
            restaurantSignUp.creditCard.ExpiryDate);

            if (card == null)
            {
                ModelState.AddModelError("", "Invalid credit card details.");
                return View(restaurantSignUp);
            }

            byte[] imageBytes;
            using (var ms = new MemoryStream())
            {
                restaurantSignUp.UploadedImage.CopyTo(ms);
                imageBytes = ms.ToArray();
            }

            var restaurant = new Restaurant
            {
                name = restaurantSignUp.name,
                email = restaurantSignUp.email,
                hotline = restaurantSignUp.hotline,
                address = restaurantSignUp.address,
                description = restaurantSignUp.description,
                creditCard = card,
                Image = imageBytes
            };

            var hasher = new PasswordHasher<Restaurant>();
            restaurant.passwordHash = hasher.HashPassword(restaurant, restaurantSignUp.passwordHash);

            _restaurantRepository.Add(restaurant);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel restaurantlogin)
        {

            if (!ModelState.IsValid)
            {
                restaurantlogin.msg = "";
                return View(restaurantlogin);
            }

            var restaurant = _restaurantRepository.GetByEmail(restaurantlogin.email);

            if (restaurant == null)
            {
                restaurantlogin.msg = "Email or Password is incorrect! Please try again.";
                return View(restaurantlogin);
            }

            var passwordHasher = new PasswordHasher<Restaurant>();
            var result = passwordHasher.VerifyHashedPassword(
                restaurant,
                restaurant.passwordHash,
                restaurantlogin.passwordHash
            );

            if (result == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, restaurant.name),
            new Claim(ClaimTypes.Role, "Restaurant"),
            new Claim("RestaurantId", restaurant.id.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }
                );
                return RedirectToAction("RestaurantDashboard");
            }
            else
            {
                restaurantlogin.msg = "Email or Password is incorrect! Please try again.";
                return View(restaurantlogin);
            }
        }

        [HttpGet]
        public async Task<IActionResult> RestaurantDashBoardAsync()
        {
            int id = int.Parse(User.FindFirst("RestaurantId").Value);
            RestaurantDashboardViewModel dashboardModel = new();
            await PrepareDashboardAsync(dashboardModel, id);
            var claim = User.FindFirst(ClaimTypes.Name);
            ViewBag.RestaurantName = claim != null ? claim.Value : "Name";
            dashboardModel.BookedTables = await _restaurantRepository.GetBookingsForRestaurant(id);
            return View(dashboardModel);
        }


        public IActionResult SendVerificationRequest()
        {
            return View(new Restaurant());
        }
        [HttpPost]
        public async Task<IActionResult> SendVerificationRequest(IFormFile verificationDocuments)
        {
            // Validate file presence
            if (verificationDocuments == null || verificationDocuments.Length == 0)
            {
                ModelState.AddModelError("", "Please upload a valid PDF file.");
                return View();
            }

            // Validate file type
            if (verificationDocuments.ContentType != "application/pdf")
            {
                ModelState.AddModelError("", "Only PDF files are allowed.");
                return View();
            }

            if (verificationDocuments.Length > 8 * 1024 * 1024)
            {
                ModelState.AddModelError("", "File size must be under 8 MB.");
                return View();
            }


            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);


            byte[] pdfBytes;
            using (var ms = new MemoryStream())
            {
                await verificationDocuments.CopyToAsync(ms);
                pdfBytes = ms.ToArray();
            }

            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound("Restaurant not found.");
            }

            restaurant.verificationDocuments = pdfBytes;
            _restaurantRepository.UpdateVerificationDocument(restaurant, pdfBytes);
            var request = new VerificationRequest
            {
                provider_Id = restaurantId,
                role = "Restaurant"
            };
            await _unitOfWork.VerificationRequests.AddAsync(request);
            await _unitOfWork.VerificationRequests.SaveAsync();
            TempData["Success"] = "Verification file uploaded successfully. Your verification status is pending review.";
            return View("RestaurantDashBoard");
        }

        public async Task<IActionResult> AddMealAsync()
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);
            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(restaurantId);
            if (restaurant.verified == false)
            {
                return RedirectToAction("SendVerificationRequest");
            }

            return View(new MealViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddMealAsync(MealViewModel mealFromRequest)
        {
            if (mealFromRequest.UploadedImage == null)
            {
                ModelState.AddModelError("UploadedImage", "Please upload an images.");
                return View(mealFromRequest);
            }

            if (!mealFromRequest.UploadedImage.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("UploadedImage", "Only image files are allowed.");
                return View(mealFromRequest);
            }

            if (ModelState.IsValid)
            {
                int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);
                Meal m = new Meal
                {
                    restaurantId = restaurantId,
                    restaurant = await _unitOfWork.Restaurants.GetByIdAsync(restaurantId),
                    price = mealFromRequest.price,
                    accepted = false,
                    description = mealFromRequest.description,
                    name = mealFromRequest.name,
                    dateAdded = DateTime.Now
                };
                await _unitOfWork.Meals.AddAsync(m);
                await _unitOfWork.Meals.SaveAsync();
                using var ms = new MemoryStream();
                await mealFromRequest.UploadedImage.CopyToAsync(ms);

                Image image = new Image
                {
                    imageData = ms.ToArray(),
                    serviceType = "Meal",
                    serviceId = m.id,
                };

                await _unitOfWork.Images.AddAsync(image);
                await _unitOfWork.Images.SaveAsync();

                ServiceRequest req = new ServiceRequest
                {
                    role = "Restaurant",
                    serviceId = m.id
                };
                await _unitOfWork.ServiceRequests.AddAsync(req);
                await _unitOfWork.ServiceRequests.SaveAsync();
                TempData["SuccessMessage"] = "Meal added successfully!";
                return View("RestaurantDashboard");
            }
            return View(mealFromRequest);
        }

        public async Task<IActionResult> AddTableAsync()
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);
            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(restaurantId);
            if (restaurant.verified == false)
            {
                return RedirectToAction("SendVerificationRequest");
            }

            return View(new TableViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddTableAsync(TableViewModel tableFromRequest)
        {
            if (ModelState.IsValid)
            {
                Table t = new();
                int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);
                t.restaurantId = restaurantId;
                t.restaurant = await _unitOfWork.Restaurants.GetByIdAsync(restaurantId);
                t.bookingPrice = tableFromRequest.bookingPrice;
                t.booked = false;
                t.number = tableFromRequest.number;
                t.numberOfPersons = tableFromRequest.numberOfPersons;
                await _unitOfWork.Tables.AddAsync(t);
                await _unitOfWork.Tables.SaveAsync();
                TempData["SuccessMessage"] = "Table added successfully!";
                return View("RestaurantDashboard");
            }
            return View(tableFromRequest);
        }

        [HttpGet]
        public IActionResult ShowMeals()
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            var meals = _mealRepository.GetMealViewModel(restaurantId);

            return View(meals);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMealAsync(int id)
        {
            var image = await _unitOfWork.Images
       .FirstOrDefaultAsync(i => i.serviceType == "Meal" && i.serviceId == id);

            if (image != null)
            {
                _unitOfWork.Images.Delete(image);
                await _unitOfWork.Images.SaveAsync();
            }

            var meal = await _unitOfWork.Meals.GetByIdAsync(id);
            if (meal != null)
            {
                var req = _restaurantRepository.GetServiceRequest(id);
                if (req != null)
                {
                    _unitOfWork.ServiceRequests.Delete(req);
                    await _unitOfWork.ServiceRequests.SaveAsync();
                }
                _unitOfWork.Meals.Delete(meal);
                await _unitOfWork.Meals.SaveAsync();
            }
            return RedirectToAction("ShowMeals");
        }

        [HttpGet]
        public IActionResult EditMeal(int id)
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            var meal = _mealRepository.RestaurantMeal(id, restaurantId);
            MealViewModel mealviewmodel = new();
            mealviewmodel.price = meal.price;
            mealviewmodel.id = meal.id;
            mealviewmodel.description = meal.description;
            mealviewmodel.name = meal.name;

            return View(mealviewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMealAsync(MealViewModel mealFromRequest)
        {
            if (!ModelState.IsValid)
                return View(mealFromRequest);

            var oldMeal = await _unitOfWork.Meals.GetByIdAsync((int)mealFromRequest.id);
            if (oldMeal == null)
                return NotFound();

            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            // update fields
            oldMeal.name = mealFromRequest.name;
            oldMeal.description = mealFromRequest.description;
            oldMeal.price = mealFromRequest.price;
            oldMeal.accepted = false;
            oldMeal.restaurantId = restaurantId;

            var oldImage = await _unitOfWork.Images
           .FirstOrDefaultAsync(i => i.serviceType == "Meal" && i.serviceId == oldMeal.id);
            // upload new image
            if (mealFromRequest.UploadedImage != null)
            {
                if (oldImage != null)
                {
                    _unitOfWork.Images.Delete(oldImage);
                    await _unitOfWork.Images.SaveAsync();
                }

                using var ms = new MemoryStream();
                await mealFromRequest.UploadedImage.CopyToAsync(ms);

                Image newImage = new Image
                {
                    imageData = ms.ToArray(),
                    serviceType = "Meal",
                    serviceId = oldMeal.id,
                };

                await _unitOfWork.Images.AddAsync(newImage);
                await _unitOfWork.Images.SaveAsync();
            }

            // replace service request
            var req = _restaurantRepository.GetServiceRequest(oldMeal.id);
            if (req != null)
            {
                _unitOfWork.ServiceRequests.Delete(req);
                await _unitOfWork.ServiceRequests.SaveAsync();
            }

            req = new ServiceRequest
            {
                role = "Restaurant",
                serviceId = oldMeal.id
            };

            await _unitOfWork.ServiceRequests.AddAsync(req);
            await _unitOfWork.ServiceRequests.SaveAsync();

            // save updated meal
            await _unitOfWork.Meals.SaveAsync();

            return RedirectToAction("ShowMeals");
        }


        [HttpGet]
        public IActionResult ShowTables()
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            var tables = _tableRepository.GetTableViewModel(restaurantId);

            return View(tables);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(id);
            if (table != null)
            {
                var req = _restaurantRepository.GetServiceRequest(id);
                if (req != null)
                {
                    _unitOfWork.ServiceRequests.Delete(req);
                    await _unitOfWork.ServiceRequests.SaveAsync();
                }
                _unitOfWork.Tables.Delete(table);
                await _unitOfWork.Tables.SaveAsync();
            }
            return RedirectToAction("ShowTables");
        }

        [HttpGet]
        public IActionResult EditTable(int id)
        {
            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            var table = _tableRepository.RestaurantTable(id, restaurantId);
            TableViewModel tableviewmodel = new();
            tableviewmodel.bookingPrice = table.bookingPrice;
            tableviewmodel.id = table.id;
            tableviewmodel.number = table.number;
            tableviewmodel.numberOfPersons = table.numberOfPersons;

            return View(tableviewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTableAsync(TableViewModel tableFromRequest)
        {
            if (!ModelState.IsValid)
                return View(tableFromRequest);

            int restaurantId = int.Parse(User.FindFirst("RestaurantId")?.Value);

            // Get original tracked entity
            var oldtable = await _unitOfWork.Tables.GetByIdAsync((int)tableFromRequest.id);

            if (oldtable == null)
                return NotFound("Table not found");

            // Update only the fields
            oldtable.number = tableFromRequest.number;
            oldtable.numberOfPersons = tableFromRequest.numberOfPersons;
            oldtable.bookingPrice = tableFromRequest.bookingPrice;

            // Remove old service request
            var req = _restaurantRepository.GetServiceRequest(oldtable.id);
            if (req != null)
            {
                _unitOfWork.ServiceRequests.Delete(req);
                await _unitOfWork.ServiceRequests.SaveAsync();
            }

            // No need for Update() because EF is already tracking oldtable
            await _unitOfWork.Tables.SaveAsync();

            return RedirectToAction("ShowTables");
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Restaurant");
        }

        public async Task PrepareDashboardAsync(RestaurantDashboardViewModel dashboardModel, int id)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAsync(id);
            var today = DateTime.Today;

            dashboardModel.msg = _restaurantRepository.GetMessages(id);
        }

        [Authorize(Roles = "Restaurant")] // Only restaurant owners/managers can view
        public async Task<IActionResult> BookedTables()
        {
            var restaurantId = int.Parse(User.FindFirst("RestaurantId").Value);
            var restaurantBookings = await _restaurantRepository.GetBookingsForRestaurant(restaurantId);
            return View(restaurantBookings);
        }
    }
}

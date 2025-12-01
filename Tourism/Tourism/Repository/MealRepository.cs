using Microsoft.EntityFrameworkCore;
using Tourism.IRepository;
using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.Repository
{
    public class MealRepository : IMealRepository
    {

        private readonly TourismDbContext _Context;

        public MealRepository(TourismDbContext context)
        {
            _Context = context;
        }
        public Image GetPic(int mealId)
        {
            //return _Context.Images.Where(i => i.serviceType == "Meal" && i.serviceId == mealId).ToList();
            return _Context.Images.FirstOrDefault(i => i.serviceType == "Meal" && i.serviceId == mealId);
        }

        public List<MealViewModel> GetMealViewModel(int restaurantId)
        {
            var meals = _Context.Meals
            .Where(m => m.restaurantId == restaurantId)
            .Select(m => new MealViewModel
            {
                id = m.id,
                name = m.name,
                description = m.description,
                price = m.price,
                accepted = m.accepted,
                RestaurantId = m.restaurantId,
                ImageBytes = null
            })
                .ToList();

            foreach (var meal in meals)
            {
                var img = GetPic(meal.id ?? 0);
                meal.ImageBytes = img?.imageData;
            }
            return meals;
        }

        public Meal RestaurantMeal(int mealId, int restaurantId)
        {
            return _Context.Meals.FirstOrDefault(m => m.id == mealId && m.restaurantId == restaurantId);
        }

        public IEnumerable<Meal> GetAcceptedMeals(int restaurantId)
        {
            return _Context.Meals
                .Where(m => m.restaurantId == restaurantId && m.accepted==true)
                .ToList();
        }

    }
}

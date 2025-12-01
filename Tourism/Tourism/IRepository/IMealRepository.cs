using Tourism.Models;
using Tourism.Models.Relations;
using Tourism.ViewModel;

namespace Tourism.IRepository
{
    public interface IMealRepository
    {
       public Image GetPic(int mealId);
        public List<MealViewModel> GetMealViewModel(int restaurantId);
        public Meal RestaurantMeal(int mealId, int restaurantId);
        public IEnumerable<Meal> GetAcceptedMeals(int restaurantId);
    }
}

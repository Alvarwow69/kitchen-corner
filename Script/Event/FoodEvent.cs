namespace KitchenCorner.Script.Event;

public delegate void FoodEventHandler(Ingredient ingredient);

public class FoodEvent
{
    public static FoodEventHandler OnFoodCreated;
    public static FoodEventHandler OnFoodSliced;
    public static FoodEventHandler OnFoodCooked;
    public static FoodEventHandler OnFoodBurned;

    public static void PerformFoodCreated(Ingredient ingredient)
    {
        OnFoodCreated?.Invoke(ingredient);
    }

    public static void PerformFoodSliced(Ingredient ingredient)
    {
        OnFoodSliced?.Invoke(ingredient);
    }

    public static void PerformFoodCooked(Ingredient ingredient)
    {
        OnFoodCooked?.Invoke(ingredient);
    }

    public static void PerformFoodBurned(Ingredient ingredient)
    {
        OnFoodBurned?.Invoke(ingredient);
    }
}
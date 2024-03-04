namespace KitchenCorner.Script.Event;

public delegate void IngredientThrow(Ingredient ingredient);

public class TrashEvent
{
    public static IngredientThrow OnIngredientThrow;

    public static void PerformOnIngredientThrow(Ingredient ingredient)
    {
        OnIngredientThrow.Invoke(ingredient);
    }
}
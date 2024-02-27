namespace KitchenCorner.Script.Event;

public delegate void NewIngredientHandler(Plate plate, Ingredient newIngredient);
public delegate void PlateHandler(Plate plate);

public class PlateEvent
{
    public static NewIngredientHandler OnFoodAddedPlateEvent;
    public static PlateHandler OnDirtyPlateSpawn;
    public static PlateHandler OnPlateCleaned;
    public static PlateHandler OnMovePlateOnDishRack;

    public static void PerformFoodAddedPlateEvent(Plate plate, Ingredient newIngredient)
    {
        OnFoodAddedPlateEvent?.Invoke(plate, newIngredient);
    }

    public static void PerformDirtyPlateSpawn(Plate plate)
    {
        OnDirtyPlateSpawn?.Invoke(plate);
    }

    public static void PerformCleanPlate(Plate plate)
    {
        OnPlateCleaned?.Invoke(plate);
    }

    public static void PerformMovePlateOnDishRack(Plate plate)
    {
        OnMovePlateOnDishRack?.Invoke(plate);
    }
}
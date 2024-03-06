namespace KitchenCorner.Script.Event;

public delegate void OrderEventHandler(Plate plate);

public class OrderEvent
{
    public static OrderEventHandler OnOrderPlaced;

    public static void PerformOrderPlaced(Plate plate)
    {
        OnOrderPlaced?.Invoke(plate);
    }
}
namespace KitchenCorner.Script.Event;

public delegate void CounterEventHandler(CounterInteractable counter);

public class CounterEvent
{
    public static CounterEventHandler OnInteractableRemoved;
    public static CounterEventHandler OnInteractablePlaced;

    public static void PerformFoodPlaced(CounterInteractable counter)
    {
        OnInteractablePlaced?.Invoke(counter);
    }

    public static void PerformFoodRemoved(CounterInteractable counter)
    {
        OnInteractableRemoved?.Invoke(counter);
    }
}
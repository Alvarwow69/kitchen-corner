namespace KitchenCorner.Script.Event;

public delegate void TimeUp();

public class TimeEvent
{
    public static TimeUp OnTimeUp;

    public static void PerformOnTimeUp()
    {
        OnTimeUp?.Invoke();
    }
}
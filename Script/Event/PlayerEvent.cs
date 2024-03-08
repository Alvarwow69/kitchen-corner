namespace KitchenCorner.Script.Event;

public delegate void PlayerHandler(Player player);

public class PlayerEvent
{
    public static PlayerHandler OnPlayerHitByCar;

    public static void PerformPlayerHitByCar(Player player)
    {
        OnPlayerHitByCar?.Invoke(player);
    }
}
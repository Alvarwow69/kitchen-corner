using Godot;

namespace KitchenCorner.Script.Event;

public delegate void GameStateChange(GameManager.GameState newGameState);

public class GameEvent
{
    public static GameStateChange onGameStateChange;

    public static void PerformanceOnGameStateChange(GameManager.GameState newGameState)
    {
        onGameStateChange.Invoke(newGameState);
    }
}
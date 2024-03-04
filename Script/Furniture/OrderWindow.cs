using Godot;
using System;
using System.Diagnostics;
using KitchenCorner.Script.Event;

public partial class OrderWindow : SelectionInteractable
{
    [Export] private CommandManager _commandManager;
    [Export] private PlateManager _plateManager;

    public override void PerformAction(Player player)
    {
        if (player.GetInteractable() is not Plate)
            return;
        var plate = player.RemoveInteractable() as Plate;
        OrderEvent.PerformOrderPlaced(plate);
        var list = plate.RemoveAllIngredient();
        _commandManager?.CheckCommands(list);
        foreach (var ingredient in list)
            ingredient.QueueFree();
        _plateManager.AddPlate(plate);
    }
}

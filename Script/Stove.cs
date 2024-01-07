using Godot;
using System;

public partial class Stove : CounterInteractable
{
    protected override void PlaceInteractable(Player player)
    {
        base.PlaceInteractable(player);
        (_interactable as Cooker)?.StartCooking();
    }

    protected override void RemoveInteractable(Player player)
    {
        (_interactable as Cooker)?.StopCooking();
        base.RemoveInteractable(player);
    }
}

using Godot;
using System;
using System.Collections.Generic;

public partial class DishRack : CounterInteractable
{
	private Stack<Plate> _plates = new Stack<Plate>();
	public void AddPlate(Plate plate)
	{
		plate.Freeze();
		plate.GlobalPosition = _anchor.GlobalPosition;
		plate.GlobalRotation = _anchor.GlobalRotation;
		_anchor.Reparent(plate);
		_plates.Push(plate);
	}
	
	public void RemovePlatePlate(Player player)
	{
		Plate plate = _plates.Pop();
		player.AddInteractable(plate);
	}

	public override void PerformAction(Player player)
	{
		if (!player.HasInteractable())
			RemoveInteractable(player);
	}

	protected override void RemoveInteractable(Player player)
	{
		if (_plates.Count == 0 || player.HasInteractable())
			return;
		player.AddInteractable(_plates.Pop());
	}
}

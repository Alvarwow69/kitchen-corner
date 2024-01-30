using Godot;
using System;
using System.Collections.Generic;

public partial class DishRack : Node
{
	[Export] private Node3D _anchor;

	private Stack<Plate> _plates = new Stack<Plate>();
	public void AddPlate(Plate plate)
	{
		plate.Freeze();
		plate.GlobalPosition = _anchor.GlobalPosition;
		plate.GlobalRotation = _anchor.GlobalRotation;
		_anchor.AddChild(plate);
		_plates.Push(plate);
	}
	
	public void RemovePlatePlate(Player player)
	{
		Plate plate = _plates.Pop();
		player.AddInteractable(plate);
	}
}

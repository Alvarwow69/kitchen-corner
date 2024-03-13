using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class OutOfBound : Area3D
{
	#region Properties

	[Export] private PlateManager _plateManager;

	#endregion

	public void OnPlayerEnter(Node3D player)
	{
		var p = player as Player;

		if (p.HasInteractable())
		{
			var plate = p.RemoveInteractable() as Ingredient;
			var list = plate.RemoveAllIngredient();
			foreach (var ingredient in list)
				ingredient.QueueFree();
			if (plate is Plate)
				_plateManager.AddPlate(plate as Plate, -1);
			else
				plate.QueueFree();
		}
		PlayerEvent.PerformPlayerHitByCar(p);
	}
}

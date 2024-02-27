using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class Trash : SelectionInteractable
{
	public override void PerformAction(Player player)
	{
		if (!player.HasInteractable())
			return;
		if (player.GetInteractable() is Container)
		{
			var ingredients = (player.GetInteractable() as Container)?.RemoveAllIngredient();
			TrashEvent.PerformOnIngredientThrow(ingredients[0] as Ingredient);
			foreach (var ingredient in ingredients)
				ingredient.QueueFree();
		}
		else
		{
			var ingredient = player.RemoveInteractable();
			TrashEvent.PerformOnIngredientThrow(ingredient as Ingredient);
			ingredient.QueueFree();
		}
	}
}

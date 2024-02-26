using Godot;
using System;
using System.Diagnostics;
using KitchenCorner.Script.Event;

public partial class FoodProvider : SelectionInteractable
{
	[Export]
	private PackedScene _ingredient;

	[Export] private AnimationPlayer _animation;


	public override void PerformAction(Player player)
	{
		if (player.HasInteractable() && player.GetInteractable() is not Container)
			return;
		var newIngredient = GD.Load<PackedScene>(_ingredient.ResourcePath).Instantiate();
		AddChild(newIngredient);
		player.AddInteractable(newIngredient as RigidInteractable);
		_animation.Play("Create_provider");
		FoodEvent.PerformFoodCreated(newIngredient as Ingredient);
	}
}

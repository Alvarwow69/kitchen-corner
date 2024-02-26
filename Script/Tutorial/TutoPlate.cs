using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class TutoPlate : Target
{
	[Export] private int _targetStep;

	public override void EnableTarget()
	{
		PlateEvent.OnFoodAddedPlateEvent += OnFoodAddedPlateEvent;
	}

	public override void DisableTarget()
	{
		PlateEvent.OnFoodAddedPlateEvent -= OnFoodAddedPlateEvent;
	}

	private void OnFoodAddedPlateEvent(Plate plate, Ingredient newIngredient)
	{
		if (TutorialManager.GetCurrentStep() != _targetStep)
			return;
		if (newIngredient.GetNameState() == "Ham_Cooked" && plate.GetIngredients().Count >= 1 && plate.GetIngredients()[0].GetNameState() == "Carrot_Sliced")
			TargetEvent.PerformTargetReached();
	}
}

using Godot;
using System;
using Godot.Collections;
using KitchenCorner.Script.Event;

public partial class TutoPlate : Target
{
	[Export] private int _targetStep;
	[Export] private Array<string> _OptionalStep = new Array<string>();

	public override void EnableTarget()
	{
		PlateEvent.OnFoodAddedPlateEvent += OnFoodAddedPlateEvent;
		FoodEvent.OnFoodBurned += OnFoodBurned;
	}

	public override void DisableTarget()
	{
		PlateEvent.OnFoodAddedPlateEvent -= OnFoodAddedPlateEvent;
		FoodEvent.OnFoodBurned -= OnFoodBurned;
	}

	private void OnFoodAddedPlateEvent(Plate plate, Ingredient newIngredient)
	{
		if (TutorialManager.GetCurrentStep() != _targetStep)
			return;
		if (newIngredient.GetNameState() == "Ham_Cooked" && plate.GetIngredients().Count >= 1 && plate.GetIngredients()[0].GetNameState() == "Carrot_Sliced")
			TargetEvent.PerformTargetReached();
		else
			TargetEvent.PerformNewCustomStep(_OptionalStep[0]);
	}

	private void OnFoodBurned(Ingredient ingredient)
	{
		if (TutorialManager.GetCurrentStep() != _targetStep)
			return;
		TargetEvent.PerformNewCustomStep(_OptionalStep[1]);
		FoodEvent.OnFoodBurned -= OnFoodBurned;
	}
}

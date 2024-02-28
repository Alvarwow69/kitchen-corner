using Godot;
using System;
using Godot.Collections;
using KitchenCorner.Script.Event;

public partial class StepPlate : Target
{
	[Export] private int _targetStep;
	[Export] private Array<string> _OptionalStep = new Array<string>();
	[Export] private Dictionary<string, Ingredient.FoodState> _IngredientList = new Dictionary<string, Ingredient.FoodState>();

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

	private bool CheckList(Array<Ingredient> list)
	{
		foreach (var ingredient in list)
			if (!_IngredientList.ContainsKey(ingredient.Name) || _IngredientList[ingredient.Name] != ingredient.GetState())
				return false;
		return true;
	}

	private void OnFoodAddedPlateEvent(Plate plate, Ingredient newIngredient)
	{
		if (TutorialManager.GetCurrentStep() != _targetStep)
			return;
		if (CheckList(plate.GetIngredients()))
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

using Godot;
using System;
using System.Diagnostics;
using KitchenCorner.Script.Event;

public partial class CookIngredient : SliceIngredients
{
	private double _cookTime = 0.0f;
	[Export] private float _cookedTime = 5.0f;
	[Export] private float _burnedTime = 10.0f;

	public void UpdateCookTime(double addTime)
	{
		if (State == FoodState.Raw)
			return;
		_cookTime += addTime;
		if (_cookTime >= _cookedTime && _cookTime < _burnedTime)
		{
			SetState(FoodState.Cooked);
			FoodEvent.PerformFoodCooked(this);
		}

		if (_cookTime >= _burnedTime)
		{
			SetState(FoodState.Burned);
			FoodEvent.PerformFoodBurned(this);
		}

	}

	public double GetProgress()
	{
		return _cookTime / _cookedTime * 100.0;
	}
}

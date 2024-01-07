using Godot;
using System;
using System.Diagnostics;

public partial class CookIngredient : Ingredient
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
			SetState(FoodState.Cooked);
		if (_cookTime >= _burnedTime)
			SetState(FoodState.Burned);
	}
}

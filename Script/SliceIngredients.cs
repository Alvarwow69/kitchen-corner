using Godot;
using System;

public partial class SliceIngredients : Ingredient
{
	private double _sliceTime = 0.0f;
	[Export] private float _slicedTime = 5.0f;

	public void UpdateSliceTime(double addTime)
	{
		if (State != FoodState.Raw)
			return;
		_sliceTime += addTime;
		if (_sliceTime >= _slicedTime)
			SetState(FoodState.Sliced);
	}

	public double GetProgress()
	{
		return _sliceTime / _slicedTime * 100;
	}
}

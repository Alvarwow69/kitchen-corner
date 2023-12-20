using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Food : RigidInteractable
{
	public enum State
	{
		Raw,
		Sliced,
		Cooked,
		Burned
	}

	[Export] private State _state = State.Raw;
	[Export] private String _name = "Food";
	[Export] private Array<string> _compatibility;

	public override void PerformAction(Player player)
	{
		if (!player.HasInteractable())
		{
			player.AddInteractable(this);
			Freeze();
		}
	}

	public override void Drop(Player player)
	{
		player.RemoveInteractable();
		Activate();
	}

	public bool IsCompatible(Food food)
	{
		Debug.Print(_compatibility[0]);
		return _compatibility.Contains(food.GetNameState());
	}

	public string GetNameState()
	{
		return _name + "_" + _state;
	}
}

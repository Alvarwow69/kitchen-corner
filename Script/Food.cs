using Godot;
using System;
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

	public override void _Ready()
	{
		base._Ready();
		GetNode<MeshInstance3D>("RigidBody3D/" + State.Raw).Visible = false;
		SetState(_state);
	}

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
		return _compatibility.Contains(food.GetNameState());
	}

	public string GetNameState()
	{
		return _name + "_" + _state;
	}
	
	public void SetState(State newState)
	{
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = false;
		_state = newState;
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = true;
	}
}

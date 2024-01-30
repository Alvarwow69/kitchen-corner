using Godot;
using System;
using Godot.Collections;
using Array = System.Array;

public partial class Bowl : Ingredient
{
	public enum BowlState
	{
		Clean,
		Dirty,
		Full
	}

	[Export] private BowlState _state = BowlState.Clean;
	private Node3D _anchor;
	private Array<Ingredient> Foods { get; } = new Array<Ingredient>();

	public override void _Ready()
	{
		_anchor = GetNode<Node3D>("RigidBody3D/Anchor");
		GetNode<MeshInstance3D>("RigidBody3D/" + BowlState.Clean).Visible = false;
		SetState(_state);
		base._Ready();
	}
	
	public void SetState(BowlState newState)
	{
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = false;
		_state = newState;
		GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = true;
	}

	public void AddSoup(Array<Ingredient> ingredients)
	{
		foreach (var ingredient in ingredients)
		{
			Foods.Add(ingredient);
		}
	}
}

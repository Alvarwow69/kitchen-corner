using Godot;
using System;
using Godot.Collections;
using Array = System.Array;

public partial class Bowl : Plate
{
	private Node3D _anchor;
	public override void _Ready()
	{
		_anchor = GetNode<Node3D>("RigidBody3D/Anchor");
		GetNode<MeshInstance3D>("RigidBody3D/" + PlateState.Clean).Visible = false;
		SetState(_state);
		base._Ready();
	}

	public void AddSoup(Array<Ingredient> ingredients)
	{
		foreach (var ingredient in ingredients)
		{
			Foods.Add(ingredient);
		}
		SetState(PlateState.HasFood);
	}
}

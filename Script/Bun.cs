using Godot;
using System;
using System.Diagnostics;

public partial class Bun : Ingredient
{
	private MeshInstance3D _upperBun;

	public override void _Ready()
	{
		base._Ready();
		_upperBun = GetNode<MeshInstance3D>("RigidBody3D/Raw/UpperBun");
	}

	public override void AddLevel(float height)
	{
		_upperBun.Position += new Vector3(0, height, 0);
	}
}

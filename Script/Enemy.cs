using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class Enemy : MeshInstance3D
{
	#region Properties

	[Export] private AnimationPlayer _animation;

	#endregion

	public override void _Ready()
	{
		OrderEvent.OnOrderPlaced += OnOrderPlaced;
	}

	private void OnOrderPlaced(Plate plate)
	{
		_animation.Play("Jump");
	}
}

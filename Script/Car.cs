using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Car : PathFollow3D
{
	#region Properties

	[Export] private Array<Mesh> _meshes = new Array<Mesh>();
	[Export] private double _speed = 1.0;

#endregion

	public override void _Ready()
	{
		GetNode<MeshInstance3D>("MeshInstance3D").Mesh = _meshes.PickRandom();
	}

	public override void _Process(double delta)
	{
		if (ProgressRatio + (float)(delta * _speed) >= 1)
			QueueFree();
		ProgressRatio += (float)(delta * _speed);
	}
}

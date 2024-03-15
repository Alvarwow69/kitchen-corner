using Godot;
using System;

public partial class PlayerModel : Node3D
{
	[Export] private AnimationPlayer _animation;
	[Export] private bool _defaultActivate = false;

	public override void _Ready()
	{
		if (_defaultActivate)
			Scale = new Vector3(1.0f, 1.0f ,1.0f);
	}

	public void Enable()
	{
		_animation.Play("Disable");
	}

	public void Disable()
	{
		_animation.PlayBackwards("Disable");
	}
}

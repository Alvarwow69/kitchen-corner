using Godot;
using System;
using System.Diagnostics;

public partial class VisualTarget : Target
{
	#region Properties

	[Export] private AnimationPlayer _animationPlayer;
	[Export] private double _delay = 0.5;

	private bool _activate;
	private double _currentTimer;

	#endregion

	#region Methodes

	public override void EnableTarget()
	{
		_activate = true;
	}

	private void DoEnable()
	{
		_activate = false;
		_animationPlayer.Play("VisualTarget");
		Visible = true;
		foreach (var child in GetChildren())
		{
			(child as Tuto_details)?.EnableDetail();
		}
	}

	public override void _Process(double delta)
	{
		if (!_activate)
			return;
		_currentTimer += delta;
		if (_currentTimer >= _delay)
			DoEnable();
	}

	public override void DisableTarget()
	{
		_animationPlayer.Stop();
		Visible = false;
		foreach (var child in GetChildren())
		{
			(child as Tuto_details)?.DisableDetail();
		}
	}

	#endregion
	
}

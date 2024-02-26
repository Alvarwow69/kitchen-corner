using Godot;
using System;

public partial class TimedTuto : Target
{
	[Export] private double _timer = 2;

	private bool _enabled = false;
	private double _currentTime = 0.0;

	public override void EnableTarget()
	{
		_enabled = true;
		Visible = true;
		_currentTime = 0.0;
	}

	public override void DisableTarget()
	{
		_enabled = false;
		Visible = false;
	}

	public override void _Process(double delta)
	{
		if (!_enabled)
			return;
		_currentTime += delta;
		if (_currentTime >= _timer)
			TargetEvent.PerformTargetReached();
	}
}

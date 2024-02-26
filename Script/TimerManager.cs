using Godot;
using System;
using System.Diagnostics;

public partial class TimerManager : Node
{
	[Export] private bool _enabled = true;
	[Export] private double _time = 300.0;
	[Export] private Label _text;
	
	private double _timer; 
	
	public override void _Ready()
	{
		_timer = _time;
	}

	public override void _Process(double delta)
	{
		if (!_enabled)
			return;
		_timer -= delta;
		_text.Text = (int)_timer / 60 + ":" + ((int)_timer % 60 < 10 ? "0" : "") + (int)_timer % 60;
		if (_timer <= 0)
			GetTree().Quit(); //TODO change later
	}
}

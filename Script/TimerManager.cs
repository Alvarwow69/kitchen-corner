using Godot;
using System;
using System.Diagnostics;
using KitchenCorner.Script.Event;

public partial class TimerManager : Node
{
	[Export] private double _time = 300.0;
	[Export] private Label _text;
	
	private double _timer; 
	
	public override void _Ready()
	{
		_timer = _time;
		_text.Text = (int)_timer / 60 + ":" + ((int)_timer % 60 < 10 ? "0" : "") + (int)_timer % 60;
	}

	public override void _Process(double delta)
	{
		if (GameManager.GetGameState() != GameManager.GameState.InGame)
			return;
		_timer -= delta;
		_text.Text = (int)_timer / 60 + ":" + ((int)_timer % 60 < 10 ? "0" : "") + (int)_timer % 60;
		if (_timer <= 0)
			TimeEvent.PerformOnTimeUp();
	}
}

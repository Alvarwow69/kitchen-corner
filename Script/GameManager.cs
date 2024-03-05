using Godot;
using System;
using KitchenCorner.Script.Event;

public partial class GameManager : Node3D
{
	public enum GameState
	{
		Starting,
		InGame,
		EndGame,
	}

	#region Properties

	[Export] private GameState _defaultState = GameState.Starting;
	[Export(PropertyHint.File, "*.tscn")] private string _timeUpScene;
	[Export] private double _timeBeforeChange = 3.0f;
	[Export] private double _timeBeforeStart = 3.0f;
	[Export] private Label _uiCountDown;

	private double _timer = 0.0;
	private static GameState _gameState;

	#endregion

	public override void _Ready()
	{
		TimeEvent.OnTimeUp += OnTimeUp;
		TargetEvent.OnTutorialFinished += OnTimeUp;
		_gameState = _defaultState;
		GameEvent.PerformanceOnGameStateChange(_gameState);
	}

	public override void _Process(double delta)
	{
		if (_gameState != GameState.InGame)
			_timer += delta;
		else
			_timer = 0;
		if (_gameState == GameState.EndGame && _timer >= _timeBeforeChange)
			GetTree().ChangeSceneToFile(_timeUpScene);
		if (_gameState == GameState.Starting)
		{
			_uiCountDown.Text = "" + (int)(_timeBeforeStart - _timer + 1) % 60;
			if (_timer >= _timeBeforeStart)
			{
				_uiCountDown.Visible = false;
				_gameState = GameState.InGame;
				GameEvent.PerformanceOnGameStateChange(_gameState);
			}

		}
	}

	private void OnTimeUp()
	{
		_gameState = GameState.EndGame;
		GameEvent.PerformanceOnGameStateChange(_gameState);
	}

	public static GameState GetGameState()
	{
		return _gameState;
	}
}

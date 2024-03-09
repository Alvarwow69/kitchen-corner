using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot.Collections;
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
	[Export] private ScoreManager _scoreManager;
	[Export] private Array<Node3D> _spawnPoints = new Array<Node3D>();

	private double _timer = 0.0;
	private static GameState _gameState;
	private Godot.Collections.Dictionary<Player, double> _disabledPlayer = new Godot.Collections.Dictionary<Player, double>();


	#endregion

	public override void _Ready()
	{
		TimeEvent.OnTimeUp += OnTimeUp;
		TargetEvent.OnTutorialFinished += OnTimeUp;
		PlayerEvent.OnPlayerHitByCar += OnPlayerHitByCar;
		for (int i = 0; i < 2; i++)
			SpawnPlayers(i);
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
			ChangeScene();
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
		foreach (var player in _disabledPlayer)
		{
			_disabledPlayer[player.Key] += delta;
			if (player.Value >= 2)
			{
				ReSpawnPlayer(player.Key);
				_disabledPlayer.Remove(player.Key);
			}
		}
	}

	private void OnTimeUp()
	{
		_gameState = GameState.EndGame;
		GameEvent.PerformanceOnGameStateChange(_gameState);
	}

	private void ChangeScene()
	{
		var scoreInfo = GetNode<info_score>("/root/InfoScore");
		scoreInfo.UpdateScore(GetParent().Name, _scoreManager.GetScore());
		GetTree().ChangeSceneToFile(_timeUpScene);
	}

	public static GameState GetGameState()
	{
		return _gameState;
	}

	private void OnPlayerHitByCar(Player player)
	{
		if (player.HasInteractable())
			player.GetInteractable().Drop(player);
		player.DisablePlayer();
		CollectionExtensions.TryAdd(_disabledPlayer, player, 0);
		Debug.Print("Player added!");
	}

	private void SpawnPlayers(int index)
	{
		var player = GetNode<Player>("/root/SC_Dungeon/Player" + index);
		var sPoint = _spawnPoints[index];

		player.PlayerNumber = index;
		player.GlobalPosition = sPoint.GlobalPosition;
		player.GlobalRotation = sPoint.GlobalRotation;
		player.EnablePlayer();
		Debug.Print("Player" + index + " spawned.");
	}

	private void ReSpawnPlayer(Player player)
	{
		var sPoint = _spawnPoints[player.PlayerNumber];
		player.GlobalPosition = sPoint.GlobalPosition;
		player.GlobalRotation = sPoint.GlobalRotation;
		player.EnablePlayer();
		Debug.Print("Player respawned!");
	}
}

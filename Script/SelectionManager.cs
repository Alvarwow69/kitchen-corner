using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot.Collections;
using KitchenCorner.Script.Event;
using KitchenCorner.Script.Save;

public partial class SelectionManager : Node3D
{
	public enum SelectionState
	{
		Computing,
		Pause,
		Waiting,
	}

	#region Properties

	[Export] private SelectionState _defaultState = SelectionState.Computing;
	[Export] private Array<NodeLevelSelector> _listLevel;
	[Export] private PauseMenu _pauseMenu;

	private double _timer = 0.0;
	private static SelectionState _gameState;
	private SaveLevel _levels;

	#endregion

	public override void _Ready()
	{
		_gameState = _defaultState;
		_levels = GetNode<SaveLevel>("/root/SaveLevel");
		foreach (var levelSelector in _listLevel)
			_levels.AddLevel(levelSelector.Name, levelSelector.Activated, levelSelector.Score);
		_levels.Save();
	}

	public override void _Process(double delta)
	{
		if (_gameState == SelectionState.Computing)
		{
			foreach (var levelSelector in _listLevel)
				if (_levels.Content[levelSelector.Name].Activated)
					levelSelector.Activate();
			_gameState = SelectionState.Waiting;
			return;
		}
		if (Input.IsActionJustPressed("player0_pause"))
			TogglePause();
	}

	public static SelectionState GetGameState()
	{
		return _gameState;
	}

	public static void Reset()
	{
		_gameState = SelectionState.Computing;
	}

	public void TogglePause()
	{
		_gameState = _gameState == SelectionState.Pause ? SelectionState.Waiting : SelectionState.Pause;
		if (_gameState == SelectionState.Pause)
			_pauseMenu.Enable();
		else
			_pauseMenu.Disable();
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot.Collections;
using KitchenCorner.Script.Event;

public partial class SelectionManager : Node3D
{
	public enum SelectionState
	{
		Computing,
		Waiting,
	}

	#region Properties

	[Export] private SelectionState _defaultState = SelectionState.Computing;
	[Export] private Array<NodeLevelSelector> _listLevel;

	private double _timer = 0.0;
	private static SelectionState _gameState;
	private info_score _infoScore;

	#endregion

	public override void _Ready()
	{
		_gameState = _defaultState;
		_infoScore = GetNode<info_score>("/root/InfoScore");
		foreach (var levelSelector in _listLevel)
			_infoScore.AddLevel(levelSelector.Name, levelSelector.Score, levelSelector.Activated);
	}

	public override void _Process(double delta)
	{
		if (_gameState != SelectionState.Computing)
			return;
		foreach (var levelSelector in _listLevel)
			if (_infoScore.IsActivate(levelSelector.Name))
				levelSelector.Activate();
		_gameState = SelectionState.Waiting;
	}

	public static SelectionState GetGameState()
	{
		return _gameState;
	}
}

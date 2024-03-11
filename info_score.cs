using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class info_score : Node
{
	#region Properties

	private Godot.Collections.Dictionary<string, int> _scores = new Godot.Collections.Dictionary<string, int>();
	private Godot.Collections.Dictionary<string, bool> _activated = new Godot.Collections.Dictionary<string, bool>();

	#endregion

	#region Methodes

	public void UpdateScore(string levelName, int newScore)
	{
		CollectionExtensions.TryAdd(_scores, levelName, newScore);
		if (_scores[levelName] < newScore)
			_scores[levelName] = newScore;
	}

	public int GetScore(string levelName)
	{
		if (!_scores.ContainsKey(levelName))
			return -1;
		return _scores[levelName];
	}

	public void AddLevel(string levelName, int score = -1, bool activated = false)
	{
		_scores.TryAdd(levelName, score);
		_activated.TryAdd(levelName, activated);
	}

	public void ActivateLevel(string levelName)
	{
		_activated[levelName] = true;
	}

	public bool IsActivate(string levelName)
	{
		return _activated[levelName];
	}

	#endregion
}

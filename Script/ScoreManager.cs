using Godot;
using System;

public partial class ScoreManager : Control
{
	[Export] private Label _text;
	private int _score = 0;

	public override void _Ready()
	{
		_text.Text = "Score: " + _score;
	}

	public int GetScore()
	{
		return _score;
	}

	public void AddScore(int bonus)
	{
		_score += bonus;
		_text.Text = "Score: " + _score;
	}
	
	public void RemoveScore(int malus)
	{
		_score -= malus;
		if (_score < 0)
			_score = 0;
		_text.Text = "Score: " + _score;
	}
}

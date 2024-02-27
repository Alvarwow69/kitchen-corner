using Godot;
using System;

public partial class TutoDialogue : Tuto_details
{
	[Export] private double _animationSpeed;
	[Export] private Label _label;
	[Export(PropertyHint.MultilineText)] private string _text;

	private int _numberChar = 0;
	private double _currentTimer = 0;

	public override void EnableDetail()
	{
		Visible = true;
		_numberChar = 0;
		_currentTimer = 0;
		_label.VisibleCharacters = 0;
		_label.Text = _text;
	}

	public override void DisableDetail()
	{
		Visible = false;
	}

	public override void _Process(double delta)
	{
		if (!Visible)
			return;
		_currentTimer += delta;
		if (_currentTimer >= _animationSpeed)
		{
			_numberChar += 1;
			_label.VisibleCharacters = _numberChar;
			_currentTimer = 0;
		}
	}
}

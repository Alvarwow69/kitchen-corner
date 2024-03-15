using Godot;
using System;

public partial class PlayerItem : Button
{
	[Export] private Button _eraseButton;
	[Export] private Button _mainButton;
	[Export] private string _buttonText;
	[Export] private bool _defaultState = false;
	[Export] private bool _canDisable = true;
	[Export] private PlayerModel _playerModel;

	public override void _Ready()
	{
		if (_defaultState)
			EnablePlayer();
	}

	public void EnablePlayer()
	{
		if (_canDisable)
			_eraseButton.Visible = true;
		_mainButton.Text = _buttonText;
		_playerModel.Enable();
	}

	public void DisablePlayer()
	{
		_eraseButton.Visible = false;
		_mainButton.Text = "+";
		_playerModel.Disable();
	}
}

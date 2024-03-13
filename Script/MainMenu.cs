using Godot;
using System;

public partial class MainMenu : Control
{
	[Export(PropertyHint.File, "*.tscn")] private string _nextScene;
	[Export] private AnimationPlayer _animationCamera;
	[Export] private Control _menu;
	[Export] private Control _back;

	private bool _isInCredits = false;

	public void OnPressPlay()
	{
		GetTree().ChangeSceneToFile(_nextScene);
	}

	public void OnPressOption()
	{

	}

	public void OnPressCredits()
	{
		_isInCredits = !_isInCredits;
		if (_isInCredits)
		{
			_animationCamera.Play("ChangeCredits");
			_menu.Visible = false;
			_back.Visible = true;
		}
		else
		{
			_animationCamera.PlayBackwards("ChangeCredits");
			_menu.Visible = true;
			_back.Visible = false;
		}
	}

	public void OnPressExit()
	{
		GetTree().Quit();
	}
}

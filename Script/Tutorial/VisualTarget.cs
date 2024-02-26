using Godot;
using System;
using System.Diagnostics;

public partial class VisualTarget : Target
{
	#region Properties

	[Export] private AnimationPlayer _animationPlayer;

	#endregion

	#region Methodes

	public override void EnableTarget()
	{
		_animationPlayer.Play("VisualTarget");
		Visible = true;
		foreach (var child in GetChildren())
		{
			(child as Tuto_details)?.EnableDetail();
		}
	}

	public override void DisableTarget()
	{
		_animationPlayer.Stop();
		Visible = false;
		foreach (var child in GetChildren())
		{
			(child as Tuto_details)?.DisableDetail();
		}
	}

	#endregion
	
}

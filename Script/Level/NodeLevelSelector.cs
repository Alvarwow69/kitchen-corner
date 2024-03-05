using Godot;
using System;

public partial class NodeLevelSelector : Interactable
{
	#region Properties

	[Export(PropertyHint.File, "*.tscn")] private string _newLevel;
	[Export] public int Score = -1;
	[Export] public bool Activated = false;

	#endregion

	public void Activate()
	{
		Activated = true;
	}

	public override void PerformAction(Player player)
	{
		if (!Activated)
			return;
		GetTree().ChangeSceneToFile(_newLevel);
	}
}

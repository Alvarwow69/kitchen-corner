using Godot;
using System;

public partial class NodeLevelSelector : Interactable
{
	#region Properties

	[Export(PropertyHint.File, "*.tscn")] private string _newLevel;

	#endregion

	public override void PerformAction(Player player)
	{
		GetTree().ChangeSceneToFile(_newLevel);
	}
}

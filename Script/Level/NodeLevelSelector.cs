using Godot;
using System;
using KitchenCorner.Script.Save;

public partial class NodeLevelSelector : Interactable
{
	#region Properties

	[Export(PropertyHint.File, "*.tscn")] private string _newLevel;
	[Export] public int Score = -1;
	[Export] public bool Activated = false;
	[Export] private GpuParticles3D _particles;
	[Export] private Label _title;
	[Export] private string _levelName;
	[Export] private StarManager _starManager;
	[Export] private Sprite3D _sprite3D;

	#endregion

	public override void _Ready()
	{
		_particles.Emitting = false;
		_title.Text = _levelName;
		Score = GetNode<SaveLevel>("/root/SaveLevel").Content[_levelName].Score;
	}

	public void Activate()
	{
		Activated = true;
		_particles.Emitting = true;
		_starManager.UpdateStar(Score);
		_sprite3D.Visible = true;
	}

	public override void PerformAction(Player player)
	{
		if (!Activated)
			return;
		GetTree().ChangeSceneToFile(_newLevel);
		SelectionManager.Reset();
	}
}

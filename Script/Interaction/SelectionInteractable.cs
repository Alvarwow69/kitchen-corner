using System.Diagnostics;
using Godot;

public partial class SelectionInteractable : Interactable
{

	[Export] private MeshInstance3D _mesh = null;
	private ShaderMaterial _shader;

	public override void _Ready()
	{
		_mesh = _mesh == null ? GetNode<MeshInstance3D>("StaticBody3D/Mesh") : _mesh;
		_shader = _mesh.GetSurfaceOverrideMaterial(0) as ShaderMaterial;
	}

	public override void HoverEnter(Player player)
	{
		_InteractableState = InteractableState.SELECT;
		_shader.SetShaderParameter("selected", true);
	}

	public override void HoverExit(Player player)
	{
		_InteractableState = InteractableState.IDLE;
		_shader.SetShaderParameter("selected", false);
	}
}

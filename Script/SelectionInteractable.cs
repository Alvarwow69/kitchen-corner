using Godot;

public partial class SelectionInteractable : Interactable
{

	private ShaderMaterial _shader;

	public override void _Ready()
	{
		_shader = GetNode<MeshInstance3D>("StaticBody3D/Mesh").GetSurfaceOverrideMaterial(0) as ShaderMaterial;
	}

	public override void HoverEnter(Player player)
	{
		_state = State.SELECT;
		_shader.SetShaderParameter("selected", true);
	}

	public override void HoverExit(Player player)
	{
		_state = State.IDLE;
		_shader.SetShaderParameter("selected", false);
	}
}

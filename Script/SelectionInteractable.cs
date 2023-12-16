using Godot;

public partial class SelectionInteractable : Interactable
{

	private ShaderMaterial _shader;

	public override void _Ready()
	{
		_shader = GetNode<MeshInstance3D>("Mesh").GetSurfaceOverrideMaterial(0) as ShaderMaterial;
	}

	public void Select()
	{
		_state = State.SELECT;
		_shader.SetShaderParameter("selected", true);
	}

	public void Reset()
	{
		_state = State.IDLE;
		_shader.SetShaderParameter("selected", false);
	}
}

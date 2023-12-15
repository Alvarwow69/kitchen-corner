using Godot;
using System;
using System.Diagnostics;

public partial class Interactable : StaticBody3D
{
	private enum State
	{
		IDLE,
		SELECT,
		ACTION
	}

	private ShaderMaterial _shader;
	private State _state;

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

	public virtual void PerformAction()
	{
		Debug.Print("Action Performed.");
	}
}

using Godot;
using System;
using System.Diagnostics;

public partial class Pad : Target
{
    [Export] private CollisionShape3D _collision;

    private void PlayerEnter(Node3D node)
    {
        DisableTarget();
        TargetEvent.PerformTargetReached();
    }

    public override void EnableTarget()
    {
        _collision.SetDeferred("disabled", false);
        Visible = true;
        foreach (var child in GetChildren())
        {
            (child as StepDetails)?.EnableDetail();
        }
    }
    
    public override void DisableTarget()
    {
        _collision.SetDeferred("disabled", true);
        Visible = false;
        foreach (var child in GetChildren())
        {
            (child as StepDetails)?.DisableDetail();
        }
    }
}

using Godot;
using System;
using System.Diagnostics;

public partial class PadTarget : Target
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
            (child as Tuto_details)?.EnableDetail();
        }
    }
    
    public override void DisableTarget()
    {
        _collision.SetDeferred("disabled", true);
        Visible = false;
        foreach (var child in GetChildren())
        {
            (child as Tuto_details)?.DisableDetail();
        }
    }
}

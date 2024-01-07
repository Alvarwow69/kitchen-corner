using Godot;
using System;
using System.Diagnostics;

public partial class Cuttingboard : CounterInteractable
{
    private bool _cutting = false;
    private double _cutTimer = 0.0;
    private Vector3 _knifePos;
    private Vector3 _knifeRot;
    private Node3D _knife;

    [Export]
    private AnimationPlayer _animation;

    public override void _Ready()
    {
        base._Ready();
        _knife = GetNode<Node3D>("StaticBody3D/Knife");
        _knifePos = _knife.GlobalPosition;
        _knifeRot = _knife.GlobalRotation;
    }

    public override void ProcessAction(Player player)
    {
        if (!_cutting && _interactable != null && (_interactable as Ingredient)?.GetState() == Ingredient.FoodState.Raw)
        {
            _cutting = true;
            _animation.Play("Cuttingboard");
        }
    }

    protected override void RemoveInteractable(Player player)
    {
        Reset();
        base.RemoveInteractable(player);
    }

    private void Reset()
    {
        _cutting = false;
        _cutTimer = 0;
        _animation.Pause();
        _knife.GlobalPosition = _knifePos;
        _knife.GlobalRotation = _knifeRot;
    }

    public override void EndProcessAction(Player player)
    {
        if (_interactable != null)
            _cutting = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!_cutting)
            return;
        _cutTimer += delta;
        if (_cutTimer < 5)
            return;
        (_interactable as Ingredient)?.SetState(Ingredient.FoodState.Sliced);
        Reset();
    }
}
using Godot;
using System;
using System.Diagnostics;

public partial class Cuttingboard : CounterInteractable
{
    private bool _cutting = false;
    private Vector3 _knifePos;
    private Vector3 _knifeRot;
    private Node3D _knife;

    [Export]
    private AnimationPlayer _animation;
    [Export] private ProgressBar _progressBar;

    public override void _Ready()
    {
        base._Ready();
        _knife = GetNode<Node3D>("StaticBody3D/Knife");
        _knifePos = _knife.GlobalPosition;
        _knifeRot = _knife.GlobalRotation;
        _progressBar.Visible = false;
        _progressBar.Value = 0;
    }

    public override void ProcessAction(Player player)
    {
        if (_interactable is not SliceIngredients)
            return;
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
        _progressBar.Visible = false;
    }

    private void Reset()
    {
        _cutting = false;
        _animation.Pause();
        _knife.GlobalPosition = _knifePos;
        _knife.GlobalRotation = _knifeRot;
        _progressBar.Visible = false;
    }

    public override void EndProcessAction(Player player)
    {
        if (_interactable != null)
            Reset();
    }

    protected override void PlaceInteractable(Player player)
    {
        if (player.GetInteractable() is Container && (player.GetInteractable() as Container).GetIngredient(0) is SliceIngredients)
        {
            if (_interactable != null || !player.HasInteractable())
                return;
            _interactable = player.RemoveFromInteractable();
            _interactable.Freeze();
            _interactable.Reparent(_anchor);
            _interactable.GlobalPosition = _anchor.GlobalPosition;
        } else
            base.PlaceInteractable(player);
        if ((_interactable as SliceIngredients).GetProgress() > 0 && (_interactable as SliceIngredients).GetProgress() < 100)
        {
            _progressBar.Visible = true;
            _progressBar.Value = (_interactable as SliceIngredients).GetProgress();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!_cutting)
            return;
        (_interactable as SliceIngredients)?.UpdateSliceTime(delta);
        _progressBar.Value = (_interactable as SliceIngredients).GetProgress();
        if (_progressBar.Value >= 0)
            _progressBar.Visible = true;
        if (_progressBar.Value >= 100)
        {
            Reset();
        }
    }
}
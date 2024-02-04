using Godot;
using System;
using Godot.Collections;

public partial class Pot : Cooker
{
    private double _timer = 0.0;
    private float _targetTimer = 5.0f;
    private bool _cooked = false;
    [Export] private MeshInstance3D _stew;
    [Export] private PackedScene _stewScene;

    public override void AddFood(Ingredient ingredient)
    {
        if (ingredient is not CookIngredient || Foods.Count >= 3 || _cooked)
            return;
        base.AddFood(ingredient);
    }

    public override bool IsCompatible(Ingredient food)
    {
        return true;
    }

    protected override void Cooking(double time)
    {
        if (!Cook)
            return;
        _timer += time;
        if (_timer >= _targetTimer)
            UpdateVisual();
        ProgressBar.Value = _timer / _targetTimer * 100;
        if (ProgressBar.Value >= 0)
            ProgressBar.Visible = true;
        if (ProgressBar.Value >= 100)
            ProgressBar.Visible = false;
    }

    private void UpdateVisual()
    {
        _cooked = true;
        _stew.Visible = true;
    }

    public void Reset()
    {
        _cooked = false;
        _stew.Visible = false;
        _timer = 0;
        foreach (var ingredient in Foods)
        {
            ingredient.QueueFree();
        }
        Foods.Clear();
    }

    public override bool CanGetFood()
    {
        return !_cooked;
    }

    public Array<Ingredient> GetFoods()
    {
        return Foods;
    }
}

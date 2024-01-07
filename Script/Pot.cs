using Godot;
using System;

public partial class Pot : Cooker
{
    private double _timer = 0.0;
    private float _targetTimer = 5.0f;
    private bool _cooked = false;
    [Export] private MeshInstance3D _stew;

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
    }

    private void UpdateVisual()
    {
        _cooked = true;
        _stew.Visible = true;
    }

    private void Reset()
    {
        _cooked = false;
        _stew.Visible = false;
    }
}

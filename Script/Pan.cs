using Godot;
using System;

public partial class Pan : Cooker
{
    public override void AddFood(Ingredient ingredient)
    {
        if (ingredient is not CookIngredient || Foods.Count >= 1)
            return;
        base.AddFood(ingredient);
    }

    protected override void Cooking(double time)
    {
        if (Cook)
            (Foods[0] as CookIngredient)?.UpdateCookTime(time);
    }
}
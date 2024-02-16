using Godot;
using System;

public partial class Pan : Cooker
{
    public override void AddFood(Ingredient ingredient)
    {
        if (ingredient is not CookIngredient || Foods.Count >= 1)
            return;
        base.AddFood(ingredient);
        if (GetParent().GetParent().GetParent() is Stove)
            StartCooking();
    }

    protected override void Cooking(double time)
    {
        if (!Cook)
            return;
        (Foods[0] as CookIngredient)?.UpdateCookTime(time);
        ProgressBar.Value = (Foods[0] as CookIngredient).GetProgress();
        if (ProgressBar.Value >= 0)
            ProgressBar.Visible = true;
        if (ProgressBar.Value >= 100)
            ProgressBar.Visible = false;
    }

    public override bool CanGetFood()
    {
        return Foods.Count == 0;
    }
}
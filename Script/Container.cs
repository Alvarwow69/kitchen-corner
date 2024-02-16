using Godot;
using System;

public abstract partial class Container : Ingredient
{
    public abstract bool CanGetFood();

    public Ingredient GetIngredient(int index)
    {
        return Foods[index];
    }

    public virtual Ingredient RemoveIngredient()
    {
        var ingredient = Foods[0];
        Foods.Remove(ingredient);
        return ingredient;
    }
}

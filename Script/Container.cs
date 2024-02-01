using Godot;
using System;

public partial class Container : Ingredient
{
    public Ingredient RemoveIngredient()
    {
        var ingredient = Foods[0];
        Foods.Remove(ingredient);
        return ingredient;
    }
}

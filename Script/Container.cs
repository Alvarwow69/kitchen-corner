using Godot;
using System;
using Godot.Collections;
using Array = System.Array;

public abstract partial class Container : Ingredient
{
    public abstract bool CanGetFood();

    public Ingredient GetIngredient(int index)
    {
        if (index >= Foods.Count)
            return null;
        return Foods[index];
    }

    public virtual Ingredient RemoveIngredient()
    {
        var ingredient = Foods[0];
        Foods.Remove(ingredient);
        return ingredient;
    }
}

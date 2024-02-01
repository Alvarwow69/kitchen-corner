using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;

public partial class Ingredient : RigidInteractable
{
	public enum FoodState
	{
		Raw,
		Sliced,
		Cooked,
		Burned
	}

	[Export] protected FoodState State = FoodState.Raw;
	[Export] protected String Name = "Food";
	[Export] protected Array<string> Compatibility;
	[Export] protected int Weight = 0;
	[Export] protected float Height = 0.1f;
	protected Array<Ingredient> Foods { get; } = new Array<Ingredient>();


	public override void _Ready()
	{
		base._Ready();
		GetNode<Node3D>("RigidBody3D/" + FoodState.Raw).Visible = false;
		SetState(State);
	}

	public virtual bool IsCompatible(Ingredient food)
	{
		return Compatibility.Contains(food.GetNameState());
	}

	public string GetNameState()
	{
		return Name + "_" + State;
	}

	public FoodState GetState()
	{
		return State;
	}
	
	public void SetState(FoodState newState)
	{
		GetNode<Node3D>("RigidBody3D/" + State).Visible = false;
		State = newState;
		GetNode<Node3D>("RigidBody3D/" + State).Visible = true;
	}

	public virtual void AddFood(Ingredient ingredient)
	{
		foreach (var element in Foods)
			if (!element.IsCompatible(ingredient) || !ingredient.IsCompatible(element) )
				return;
		ingredient.Reparent(GetNode("RigidBody3D"));
		ingredient.Freeze();
		ingredient.GlobalPosition = GlobalPosition;
		ingredient.GlobalRotation = GlobalRotation;
		Foods.Add(ingredient);
		UpdateVisual(this, ingredient);
		foreach (var element in Foods)
		{
			if (element.Name == ingredient.Name)
				continue;
			UpdateVisual(element, ingredient);
		}
	}

	private void UpdateVisual(Ingredient element, Ingredient ingredient)
	{
		if (element.Weight < ingredient.Weight || element.Name == "Bun")
			ingredient.AddLevel(element.Height);
		else
			element.AddLevel(ingredient.Height);
		if (element.Name == "Bun")
			element.AddLevel(ingredient.Height);
	}

	public virtual void AddLevel(float height)
	{
		GetNode<Node3D>("RigidBody3D/" + State).Position += new Vector3(0, height, 0);
	}

	public Array<Ingredient> GetIngredients()
	{
		return Foods;
	}
}

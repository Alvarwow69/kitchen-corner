using Godot;
using KitchenCorner.Script.Event;

public partial class Plate : Container
{
    public enum PlateState
    {
        Clean,
        Dirty,
        HasFood,
    }

    [Export] protected PlateState _state { get; set; } = PlateState.Clean;
    private Node3D _anchor;

    public override void _Ready()
    {
        base._Ready();
        _anchor = GetNode<Node3D>("RigidBody3D/Anchor");
        GetNode<MeshInstance3D>("RigidBody3D/" + PlateState.Clean).Visible = false;
        SetState(_state);
    }

    public void SetState(PlateState newState)
    {
        GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = false;
        _state = newState;
        GetNode<MeshInstance3D>("RigidBody3D/" + _state).Visible = true;
    }

    public bool isClean()
    {
        return _state == PlateState.Clean;
    }

    public override bool AddFood(Ingredient ingredient)
    {
        if (_state == PlateState.Dirty || ingredient is Container)
            return false;
        if (Player == null && ingredient.Player != null)
        {
            if (ingredient.Player.GetInteractable() is not Container)
                ingredient.Player.RemoveInteractable();
            base.PerformAction(ingredient.Player);
            ingredient.Reparent(GetNode("RigidBody3D"));
            ingredient.GlobalPosition = GlobalPosition;
            ingredient.GlobalRotation = GlobalRotation;
            Foods.Add(ingredient);
            foreach (var element in ingredient.GetIngredients())
            {
                element.Reparent(GetNode("RigidBody3D"));
                element.GlobalPosition = GlobalPosition;
                element.GlobalRotation = GlobalRotation;
                Foods.Add(element);
            }
        }
        else
        {
            if (!base.AddFood(ingredient))
                return false;
        }
        PlateEvent.PerformFoodAddedPlateEvent(this, ingredient);
        return true;
    }

    public override bool CanGetFood()
    {
        return true;
    }
}
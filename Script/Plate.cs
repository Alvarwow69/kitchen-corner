using Godot;

public partial class Plate : Ingredient
{
    public enum PlateState
    {
        Clean,
        Dirty
    }

    [Export] private PlateState _state = PlateState.Clean;
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

    public override void AddFood(Ingredient ingredient)
    {
        if (_state == PlateState.Dirty || ingredient is Plate)
            return;
        if (Player == null)
        {
            ingredient.GetPlayer().RemoveInteractable();
            base.PerformAction(ingredient.GetPlayer());
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
            base.AddFood(ingredient);
        }
    }
}
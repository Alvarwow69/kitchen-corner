using Godot;
using System;

public partial class CommandManager : Node
{
    public enum CommandType
    {
		Burger,
        Ham,
        Stew
    }
    
    #region Properties

    [Export] private double _timeBetweenCommand = 20;
    [Export] private int _maxCommand = 4;
    [Export] private PackedScene _commandScene;
    [Export] private CommandType _commandType = CommandType.Burger;

    private int _currentCommand = 0;
    private double _timer = 0;

    #endregion

    #region Methodes

    public override void _Ready()
    {
        CreateCommand();
    }

    public override void _Process(double delta)
    {
        if (_currentCommand >= _maxCommand)
            return;
        _timer += delta;
        if (_timer >= _timeBetweenCommand || _currentCommand == 0)
            CreateCommand();
    }

    private void CreateCommand()
    {
        var newCommand = GD.Load<PackedScene>(_commandScene.ResourcePath).Instantiate();
        AddChild(newCommand);
        _currentCommand += 1;
        _timer = 0;
    }

    public void RemoveCommand(Command command)
    {
        _currentCommand -= 1;
        command.QueueFree();
    }

    #endregion
}
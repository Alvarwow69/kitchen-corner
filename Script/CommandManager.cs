using Godot;
using System;
using System.Diagnostics;
using Godot.Collections;
using KitchenCorner.Script.Event;

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
    [Export] private Array<PackedScene> _commandScene;
    [Export] private CommandType _commandType = CommandType.Burger;

    private int _currentCommand = 0;
    private double _timer = 0;

    #endregion

    #region Methodes

    public override void _Ready()
    {
        GameEvent.onGameStateChange += OnGameStarted;
    }

    private void OnGameStarted(GameManager.GameState newGameState)
    {
        if (newGameState == GameManager.GameState.InGame)
            CreateCommand();
    }

    public override void _Process(double delta)
    {
        if (GameManager.GetGameState() != GameManager.GameState.InGame || _currentCommand >= _maxCommand)
            return;
        _timer += delta;
        if (_timer >= _timeBetweenCommand || (_currentCommand == 0 && _timer >= _timeBetweenCommand / 2))
            CreateCommand();
    }

    private void CreateCommand()
    {
        var newCommand = GD.Load<PackedScene>(_commandScene.PickRandom().ResourcePath).Instantiate();
        AddChild(newCommand);
        _currentCommand += 1;
        _timer = 0;
    }

    public void RemoveCommand(Command command)
    {
        _currentCommand -= 1;
        if (command.RemainTime() <= 0)
            GetNode<ScoreManager>("../ScoreManager").RemoveScore(10);
        command.QueueFree();
    }

    public bool CheckCommands(Array<Ingredient> list)
    {
        var commandList = GetChildren();
        foreach (Command command in commandList)
            if (command.IsValidCommand(list))
            {
                Debug.Print("Command valid");
                GetNode<ScoreManager>("../ScoreManager").AddScore(100);
                command.QueueFree();
                _currentCommand -= 1;
                return true;
            }
        Debug.Print("Command Invalid");
        return false;
    }

    #endregion
}
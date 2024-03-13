using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Godot;
using FileAccess = Godot.FileAccess;

namespace KitchenCorner.Script.Save;

public class LevelData
{
    public string Name;
    public int Score;
    public bool Activated;

    public LevelData(string name, int score, bool activated)
    {
        Name = name;
        Score = score;
        Activated = activated;
    }

    public string Save()
    {
        return $"{Name}:{Score}:{Activated}";
    }
}

public partial class SaveLevel : Node
{
    private string _filePath = "user://savegame.save";
    public Dictionary<string, LevelData> Content { get; set; } = new Dictionary<string, LevelData>();

    public override void _Ready()
    {
        if (!FileAccess.FileExists(_filePath))
        {
            Debug.Print("Save file doesn't exist, empty file created.");
            return;
        }
        FileAccess saveGame = FileAccess.Open(_filePath, FileAccess.ModeFlags.Read);
        while (saveGame.GetPosition() < saveGame.GetLength())
        {
            var line = saveGame.GetLine().Split(":");
            Content.TryAdd(line[0], new LevelData(line[0], Int32.Parse(line[1]), line[2] == "True"));
        }
    }

    public void AddLevel(string levelName, bool activated = false, int score = -1)
    {
        Content.TryAdd(levelName, new LevelData(levelName, score, activated));
    }

    public bool UpdateScore(string levelName, int newScore)
    {
        if (!Content.ContainsKey(levelName))
            return false;
        Content[levelName].Score = newScore;
        return true;
    }

    public bool UpdateActivation(string levelName, bool activation)
    {
        if (!Content.ContainsKey(levelName))
            return false;
        Content[levelName].Activated = activation;
        return true;
    }

    public void Save()
    {
        FileAccess saveGame = FileAccess.Open(_filePath, FileAccess.ModeFlags.Write);
        foreach (var levelData in Content)
            saveGame.StoreLine(levelData.Value.Save());
    }
}
using Godot;
using System;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public Position2D p1Pos;
    public Position2D p2Pos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        p1Pos = (Position2D)GetNode("P1Pos");
        p2Pos = (Position2D)GetNode("P2Pos");
        var playerScene = (PackedScene)ResourceLoader.Load("res://Player.tscn");

        var p1 = (Player)playerScene.Instance();
        p1.Name = GetTree().GetNetworkUniqueId().ToString();
        p1.SetNetworkMaster(GetTree().GetNetworkUniqueId());
        Console.WriteLine($"Spawning player 1 with id {p1.Name}");
        p1.GlobalPosition = p1Pos.GlobalPosition;
        AddChild(p1);

        var p2 = (Player)playerScene.Instance();
        p2.Name = Globals.p2Id.ToString();
        p2.SetNetworkMaster(Globals.p2Id);
        Console.WriteLine($"Spawning player 1 with id {p2.Name}");
        p2.GlobalPosition = p2Pos.GlobalPosition;
        AddChild(p2);

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

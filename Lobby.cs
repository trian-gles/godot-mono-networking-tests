using Godot;
using System;

public class Lobby : Control
{

    public override void _Ready()
    {
        GetTree().Connect("network_peer_connected", this, nameof(_PlayerConnected));
    }

    public void _onHostBtnPressed() 
    {
        var net = new NetworkedMultiplayerENet();
        net.CreateServer(8001, 2);
        GetTree().NetworkPeer = net;
        Console.WriteLine("Hosting...");
        Console.WriteLine("Your id is " + GetTree().GetNetworkUniqueId().ToString());
        GetNode<Label>("Label").Text = "Waiting for other player to join...";
    }

    public void _onJoinBtnPressed() 
    {
        var net = new NetworkedMultiplayerENet();
        net.CreateClient("127.0.0.1", 8001);
        GetTree().NetworkPeer = net;
        Console.WriteLine("Joing game...");
        Console.WriteLine("Your id is " + GetTree().GetNetworkUniqueId().ToString());
    }

    public void _PlayerConnected(int id) 
    {
        Console.WriteLine($"Player with id {id.ToString()} has connected");
        Globals.otherId = id;
        var gameScene = (PackedScene)ResourceLoader.Load("res://Game.tscn");
        var game = (Game) gameScene.Instance();
        GetTree().Root.AddChild(game);
        if (GetTree().IsNetworkServer())
        {
            game.config(true);
        }
        else 
        {
            game.config(false);
        }
        Hide();
    }
}

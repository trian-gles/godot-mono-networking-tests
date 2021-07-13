using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int gravity = 800;

    [Export]
    public int jumpForce = 400;

    [Export]
    public int speed = 200;

    private Vector2 vel = new Vector2();
    public override void _Ready()
    {
        
    }

    [Remote]
    private void _setPosition(Vector2 pos) 
    {
        GlobalPosition = pos;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!IsNetworkMaster()) 
        {
            return;
        }

        if (Input.IsActionPressed("left"))
        {
            vel.x = speed * -1;
        }

        else if (Input.IsActionPressed("right"))
        {
            vel.x = speed;
        }

        else 
        {
            vel.x = 0;
        }

        if (Input.IsActionJustPressed("jump")) 
        {
            vel.y = -jumpForce;
        }

        vel.y += gravity * delta;
        MoveAndSlide(vel, Vector2.Up);
        RpcUnreliable(nameof(_setPosition), GlobalPosition);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

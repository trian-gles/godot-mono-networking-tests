using Godot;
using System;

public class PlayerUnreliableVel : KinematicBody2D
{
    [Export]
    public int gravity = 800;

    [Export]
    public int jumpForce = 400;

    [Export]
    public int speed = 200;

    [Puppet]
    public Vector2 PuppetVelocity { get; set; }

    private Vector2 vel = new Vector2();
    public override void _Ready()
    {
        
    }

    [Remote]
    private void _setPosition(Vector2 pos) 
    {
        GlobalPosition = pos;
    }

    public void GetInput() 
    {
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

        vel.y += Convert.ToSingle(gravity * 0.0166666666667);

        Rset(nameof(PuppetVelocity), vel);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (IsNetworkMaster())
        {
            GetInput();
        }
        else 
        
        {
            vel = PuppetVelocity;
        }

        
        MoveAndSlide(vel, Vector2.Up);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

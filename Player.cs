using Godot;
using System;
using System.ComponentModel;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	// Mouse Sensitivity.
	public const float Sensitivity = 0.002f;
	public float CameraPitch { get; set; } = 0.0f;
	public float CameraYaw { get; set; } = 0.0f;
	
	[ExportGroup("Nodes")]
	[Export] private Camera3D _camera;
	[Export] private Node3D _pivot;



	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseModeEnum.Captured);
	}
	public override void _PhysicsProcess(double delta)
	{	



		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		_pivot.Rotation = new Vector3(CameraPitch, 0, 0);
		Rotation = new Vector3(0, CameraYaw, 0);


	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			CameraYaw -= mouseMotion.Relative.X * Sensitivity;
			CameraPitch -= mouseMotion.Relative.Y * Sensitivity;
			CameraPitch = Mathf.Clamp(CameraPitch, -Mathf.Pi / 2, Mathf.Pi / 2);
		}
		if (@event.IsActionPressed("ui_cancel"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Visible);
		}
		if (@event.IsActionPressed("ui_accept"))
		{
			Input.SetMouseMode(Input.MouseModeEnum.Captured);
		}
	}
	
}

using Godot;
using System;

public partial class Tower : StaticBody3D
{
	public int damage { get; set; }
	public float range { get; set; }
	public float fireRate { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

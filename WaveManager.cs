using Godot;
using System;
using System.Collections.Generic;

public partial class WaveManager : Node
{
	struct EnemyData {
		public int hpValue;
		public float speed;
	}
	private List<EnemyData> _enemyTypes = new() {
		new EnemyData {
			hpValue = 5,
			speed = 1
		},
		new EnemyData {
			hpValue = 5,
			speed = 1
		},
		new EnemyData {
			hpValue = 5,
			speed = 1
		}
	};
	
	private List<(int, int)[]> _waves = new() {
		new (int, int)[] {
			(0, 5),
			(1, 3),
			(2, 2)
		}
	};
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
};

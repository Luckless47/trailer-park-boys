using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
	
	[Export]
	private Path3D _spawnPath;
	[Export] 
	private PackedScene _enemyScene;
	[Export] 
	private Button _startWaveButton;

	
	[Signal]
	public delegate void WaveEndedEventHandler();


	private int _enemiesInWave = 0;

	public override void _Ready()
	{
		WaveEnded += OnWaveEnded;
		_startWaveButton.Pressed += () => {
			StartWave(0);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public async Task StartWave(int waveIndex) {
		_enemiesInWave = 0;
		GD.Print($"Starting wave {waveIndex}");	
		foreach (var (enemyType, count) in _waves[waveIndex]) {
			for (int i = 0; i < count; i++) {
				var enemyInstance = _enemyScene.Instantiate<Enemy>();
				// Set enemy properties based on _enemyTypes[enemyType]
				// For example:
				// enemyInstance.HP = _enemyTypes[enemyType].hpValue;
				// enemyInstance.Speed = _enemyTypes[enemyType].speed;
				var pathFollow = new PathFollow3D();
				_spawnPath.AddChild(pathFollow);
				pathFollow.AddChild(enemyInstance);
				
				_enemiesInWave++;
				followPath(pathFollow);

				await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
			}
		}
	}
	public void OnWaveEnded() {
		GD.Print("Wave Ended");
	}

	public void followPath(PathFollow3D pathFollow)
	{
		var tween = CreateTween();
		tween.TweenProperty(pathFollow, "progress_ratio", 1.0f, 10.0f).SetTrans(Tween.TransitionType.Linear).SetEase(Tween.EaseType.InOut);
		tween.TweenCallback(Callable.From(() => {
			pathFollow.QueueFree();
			_enemiesInWave--;
			if (_enemiesInWave <= 0) { EmitSignal(SignalName.WaveEnded); }
		}));
	}
}

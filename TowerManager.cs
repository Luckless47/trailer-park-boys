using Godot;
using System;
using System.Collections.Generic;

public partial class TowerManager : Node
{

	struct TowerData {
		public string towerName;
		public int damage;
		public float range;
		public float fireRate;
	}
	private List<TowerData> _towerTypes = new() {
		new TowerData {
			towerName = "Basic Tower",
			damage = 1,
			range = 100,
			fireRate = 1
		},
		new TowerData {
			towerName = "Strong Tower",
			damage = 2,
			range = 100,
			fireRate = 2
		}
	};
	[Export]
	private PackedScene _towerScene;

	[Signal]
	public delegate void TowerPlacedEventHandler(Vector3 position, int towerTypeIndex);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TowerPlaced += OnTowerPlaced;
	}

	public void OnTowerPlaced(Vector3 position, int towerTypeIndex)
	{
		var towerInstance = _towerScene.Instantiate<Tower>();
		towerInstance.Name = _towerTypes[towerTypeIndex].towerName;
		towerInstance.Set("damage", _towerTypes[towerTypeIndex].damage);
		towerInstance.Set("range", _towerTypes[towerTypeIndex].range);
		towerInstance.Set("fireRate", _towerTypes[towerTypeIndex].fireRate);
		towerInstance.GlobalPosition = position;
		AddChild(towerInstance);
	}
}


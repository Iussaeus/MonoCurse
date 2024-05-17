using Godot;

namespace PirateCurse.Scripts.Entities;

public partial class EnemyFactory : Node3D 
{

    [Export]
    public PackedScene EnemyPrefab; 
    [Export]
    public float SpawnRate = 2.0f; 

    private Timer _spawnTimer;
    private SubViewport _world;

    public override void _Ready()
    {
        _world = (SubViewport)GetParent();
        _spawnTimer = new Timer();
        _spawnTimer.Autostart = true;
        _spawnTimer.WaitTime = SpawnRate;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.Start();

        AddChild(_spawnTimer); 
    }

    private  void Spawn()
    {
        var enemy = (EnemyBody3D)EnemyPrefab.Instantiate();
        _world.AddChild(enemy);
    }

    private void OnSpawnTimerTimeout()
    {
        Spawn();
    }
}
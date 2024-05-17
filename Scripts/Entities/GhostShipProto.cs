using System;
using Godot;

namespace PirateCurse.Scripts.Entities;


public partial class GhostShipProto : EnemyBody3D
{
    [Export] public float SpawnInterval = 5.0f; // Time between self-clones (seconds)

    private Timer _spawnTimer;
    private bool _isParent = true;
    private Random _random;
    
    public override void _Ready()
    {
        base._Ready();
        _random = new Random();
        if (_isParent == false) return;
        _spawnTimer = new Timer();
        _spawnTimer.Autostart = true;
        _spawnTimer.WaitTime = SpawnInterval;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
        _spawnTimer.Start();

        AddChild(_spawnTimer); // Optional: Add the timer to the scene tree (for visualization)
    }

    private GhostShipProto Clone()
    {
        var enemyClone = (GhostShipProto)Duplicate();
        enemyClone.Health = Health; // Inherit health from the original enemy
        return enemyClone;
    }

    private void OnSpawnTimerTimeout()
    {
        var enemyClone = Clone();
        enemyClone._isParent = false;
        GetParent()?.AddChild(enemyClone); 
        enemyClone.Position += new Vector3(_random.Next(-5, 5), (float)Water.Call("GetHeight", GlobalPosition) + 5, _random.Next(-5, 5)); // Randomize clone position (optional)
    }
}
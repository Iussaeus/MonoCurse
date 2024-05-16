using Godot;
using PirateCurse.Scripts.Managers;

namespace PirateCurse.Scripts.Entities;

public partial class World : Node3D
{
	private Node3D _inputManager;
	public override void _Ready()
	{
		_inputManager = GetNode<InputManager>("/root/InputManager");
	}
	public override void _Process(double delta)
	{
	}
}
using Godot;
using System;

public class WorldEnvironment : Godot.WorldEnvironment
{
    public float GM = 0.0003985f;
	public float sizeOfEarth = 6.371f;
	
	[Export]	
	public float timeFactor = 1;
	
	[Export]	
	public int precision = 50;
}

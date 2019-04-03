using Godot;
using System;

public class WorldEnvironment : Godot.WorldEnvironment
{
    public float GM = 0.0003985f;
	public float SizeOfEarth = 6.371f;
	
	[Export]	
	public float TimeFactor = 1;
	
	[Export]	
	public int Precision = 50;
}
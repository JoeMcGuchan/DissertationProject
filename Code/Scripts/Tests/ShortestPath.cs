using Godot;
using System;

public class ShortestPath : Test
{
    // Uses Dijkstra's Algorithm to get a shortest path
	
	public Satellite[] SatPath;
	public int NumHops;
	public float Distance;

	public ShortestPath(String filePath)
	{
		CreateFile(filePath);
	}
	
	public override void Run()
	{
		WriteLine("Run number " + RunNumber++);
		
		
	}
}

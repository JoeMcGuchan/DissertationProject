using Godot;
using System;

public class TotalConnectivity : Test
{
    // Simple test, returns the total connectivity of the graph

	public void Init(String filePath)
	{
		CreateFile(filePath);
	}
	
	public override void Run()
	{
		WriteLine("Run number " + RunNumber++);
	}
}

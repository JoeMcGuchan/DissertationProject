using Godot;
using System;

public class TotalConnectivity : Test
{
    // Simple test, returns the total connectivity of the graph

	public TotalConnectivity(String filePath)
	{
		Init(filePath);
	}
	
	public override void Run(Constellation constellation)
	{
		WriteLine("Run number " + RunNumber++);
	}
}

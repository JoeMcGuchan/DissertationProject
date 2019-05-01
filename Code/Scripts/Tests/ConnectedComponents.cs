using Godot;
using System;
using System.Collections.Generic;

public class ConnectedComponents : Test
{
    // Simple test, returns the number of connected components
	Constellation constellation;

	public void Init(String filePath, Constellation constellationNew)
	{
		CreateFile(filePath);
		constellation = constellationNew;
		
		WriteLine("RunNumber,NumOfComponents");
	}
	
	public override void Run()
	{
		int NumOfComponents = (
			new ConnectedComponentsAlgorithm(
				constellation.GetAllVertsList().ToArray()
			)
		).Run();

		WriteLine(RunNumber+","+NumOfComponents);
	}
}

using Godot;
using System;
using System.Collections.Generic;

public class ConnectedComponents : Test
{
    // Simple test, returns the number of connected components
	
	string FilePath;
	
	public ConnectedComponents(String filePath)
	{
		FilePath = filePath;
	}

	public override void Init(Constellation constellationNew)
	{
		CreateFile(FilePath);
		TargetConstellation = constellationNew;
		
		WriteLine("RunNumber,NumOfComponents");
	}
	
	public override void Run()
	{
		int NumOfComponents = (
			new ConnectedComponentsAlgorithm(
				TargetConstellation.GetAllVertsList().ToArray()
			)
		).Run();

		WriteLine(RunNumber+","+NumOfComponents);
	}
}

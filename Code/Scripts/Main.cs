using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment worldEnvironment;
	
	public Constellation constellation;
	
	Test test1;

    public override void _Ready()
    {
        worldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		ConstellationDescriptionDatabase constellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		ConstellationDescription constellationDescription = constellationDescriptionDatabase.GetConstellation(2);
		
		constellation = constellationDescription.Create(worldEnvironment);
		
		test1 = new ConnectedComponents("TestResults\\ConnectedComponents");
		
		AddChild(constellation);
	}
	
	private void RunTest()
	{
		test1.Run(constellation);
	}
}


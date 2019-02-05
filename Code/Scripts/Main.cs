using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment worldEnvironment;
	
	public Constellation constellation;

    public override void _Ready()
    {
        worldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		ConstellationDescriptionDatabase constellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		ConstellationDescription constellationDescription = constellationDescriptionDatabase.GetConstellation(1);
		
		constellation = constellationDescription.Create(worldEnvironment);
		
		AddChild(constellation);
	}
}
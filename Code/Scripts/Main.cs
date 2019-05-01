using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment WorldEnvironment;
	Node UI;
	Spatial Player;
	
	public Constellation ThisConstellation;
	public ConstellationDescription ThisConstellationDescription;
	
	Test test1;

    public override void _Ready()
    {
        WorldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		Player = (Spatial) FindNode("Camera");
		
		ConstellationDescriptionDatabase ConstellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		ThisConstellationDescription = ConstellationDescriptionDatabase.GetConstellation(0);
		
		Load(ThisConstellationDescription);
		
		test1 = new ConnectedComponentsRemovingSatellites(
			"./TestResults/ConnectedComponentsRemovingSatellites.csv",
			ThisConstellation,
			33,
			3,
			10,
			5,
			100,
			this
		);
	}
	
	public override void _Process(float delta)
	{
		test1.Run();
		
	}
	
	private void RunTest()
	{
		//test1.Run();
	}
	
	private void Load(ConstellationDescription ConstellationDescriptionNew)
	{
		ThisConstellationDescription = ConstellationDescriptionNew;
		
		ThisConstellation = ThisConstellationDescription.Create(WorldEnvironment);
		
		AddChild(ThisConstellation);
		
		ThisConstellation.ApplyLinking();
		
		ThisConstellation.ApplyColouringMethod();
	}
	
	public void Refresh()
	{
		ThisConstellation.QueueFree();
		
		Load(ThisConstellationDescription);
	}
}

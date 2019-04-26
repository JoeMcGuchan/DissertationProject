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
		
		//UI.Call("Init",ThisConstellationDescription,WorldEnvironment);
		
		//UI.Connect("ConstellationChanged", this, nameof(Refresh));
		
		ThisConstellation.AddBaseStation(0,0);
		
		ThisConstellation.AddBaseStation(180,0);
		
		test1 = new RepeatedShortestPathTest("TestResults\\RepeatedShortestPath.csv");
		
		((RepeatedShortestPathTest) test1).SetConstellation(ThisConstellation,ThisConstellation.BaseStations[0],ThisConstellation.BaseStations[1],60,1);
	}
	
	public override void _Process(float delta)
	{
		test1.Run();
	}
	
	private void RunTest()
	{
		test1.Run();
	}
	
	private void Load(ConstellationDescription ConstellationDescriptionNew)
	{
		ThisConstellationDescription = ConstellationDescriptionNew;
		
		if (!(ThisConstellation is null))
		{
			ThisConstellation.QueueFree();
		}
		
		ThisConstellation = ThisConstellationDescription.Create(WorldEnvironment);
		
		AddChild(ThisConstellation);
		
		ThisConstellation.ApplyLinking();
		
		ThisConstellation.ApplyColouringMethod();
	}
	
	private void Refresh()
	{
		Load(ThisConstellationDescription);
	}
}

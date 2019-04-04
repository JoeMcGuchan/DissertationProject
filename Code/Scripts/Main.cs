using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment WorldEnvironment;
	Node UI;
	
	public Constellation ThisConstellation;
	public ConstellationDescription ThisConstellationDescription;
	
	Test test1;

    public override void _Ready()
    {
        WorldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		//UI = (Node) FindNode("UI");
		
		ConstellationDescriptionDatabase ConstellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		ThisConstellationDescription = ConstellationDescriptionDatabase.GetConstellation(0);
		
		Load(ThisConstellationDescription);
		
		//UI.Call("Init",ThisConstellationDescription,WorldEnvironment);
		
		//UI.Connect("ConstellationChanged", this, nameof(Refresh));
		
		test1 = new ClosestToPoint();
		((ClosestToPoint) test1).Init("TestResults\\ClosestToPoint",ThisConstellation,new Vector3(0,2,2));
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

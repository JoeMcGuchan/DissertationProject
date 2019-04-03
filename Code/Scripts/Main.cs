using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment WorldEnvironment;
	Node UI;
	
	public Constellation Constellation;
	public ConstellationDescription ThisConstellationDescription;
	
	Test test1;

    public override void _Ready()
    {
        WorldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		//UI = (Node) FindNode("UI");
		
		ConstellationDescriptionDatabase ConstellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		ThisConstellationDescription = ConstellationDescriptionDatabase.GetConstellation(2);
		
		Load(ThisConstellationDescription);
		
		//UI.Call("Init",ThisConstellationDescription,WorldEnvironment);
		
		//UI.Connect("ConstellationChanged", this, nameof(Refresh));
		
		test1 = new ClosestToPoint();
		((ClosestToPoint) test1).Init("TestResults\\ClosestToPoint",Constellation,new Vector3(0,2,2));
	}
	
	private void RunTest()
	{
		test1.Run();
	}
	
	private void Load(ConstellationDescription ConstellationDescriptionNew)
	{
		ThisConstellationDescription = ConstellationDescriptionNew;
		
		if (!(Constellation is null))
		{
			Constellation.QueueFree();
		}
		
		Constellation = ThisConstellationDescription.Create(WorldEnvironment);
		
		AddChild(Constellation);
	}
	
	private void Refresh()
	{
		Load(ThisConstellationDescription);
	}
}

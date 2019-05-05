using Godot;
using System;

public class Main : Spatial
{
	WorldEnvironment WorldEnvironment;
	Node UI;
	Spatial Player;
	
	public Constellation ThisConstellation;
	public ConstellationDescription ThisConstellationDescription;
	public ConstellationDescriptionDatabase ThisConstellationDescriptionDatabase;
	
	Transform PlayerInit;

    public override void _Ready()
    {
        WorldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		Player = (Spatial) FindNode("Camera");
		
		PlayerInit = Player.Transform;
		
		ThisConstellationDescriptionDatabase = new ConstellationDescriptionDatabase();
		
		LoadByNumber(0);
	}
	
	private void RunTest()
	{
		//test1.Run();
	}
	
	public void Refresh()
	{
		ThisConstellation.QueueFree();
		
		Load(ThisConstellationDescription);
	}
	
	private void LoadByNumber(int i)
	{
		ThisConstellationDescription = ThisConstellationDescriptionDatabase.GetConstellation(i);
		
		Load(ThisConstellationDescription);
	}
	
	public void ReloadByNumber(int i)
	{
		ThisConstellation.QueueFree();
		
		LoadByNumber(i);
		
		Player.Transform = PlayerInit;
	}
	
	private void Load(ConstellationDescription ConstellationDescriptionNew)
	{
		ThisConstellationDescription = ConstellationDescriptionNew;
		
		ResetConstellation();
		
		ThisConstellation.InitialiseTest();
	}
	
	public void RefreshConstellation()
	{
		Test theTest = ThisConstellation.ThisTest;
		
		ThisConstellation.QueueFree();
		
		ResetConstellation();
		
		ThisConstellation.ThisTest = theTest;
		ThisConstellation.ThisTest.TargetConstellation = ThisConstellation;
	}
	
	public void ResetConstellation()
	{
		ThisConstellation = ThisConstellationDescription.Create(WorldEnvironment);
		
		AddChild(ThisConstellation);
		
		ThisConstellation.ApplyLinking();
		
		ThisConstellation.ApplyColouringMethod();
	}
}
using Godot;
using System;
using System.Collections.Generic; 

public class Constellation : Spatial
{
	public List<BaseStation> BaseStations;
	
    // This represents a collection of orbital spheres
	public OrbitalSphere[] OrbitalSpheres;
	public int NumOfSpheres;
	public WorldEnvironment ThisWorldEnvironment;
	
	public LinkingMethod ThisLinkingMethod;

	public ColouringMethod ThisColouringMethod;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
    }
	
	public override void _Process(float delta)
	{
		ThisLinkingMethod.UpdateConstellation(this);
	}
	
	//Gives all the data to the Constellation, but doesn't
	//do the work of creating it just yet
	public void Init(
		OrbitalSphere[] orbitalSpheresNew, 
		int numOfSpheresNew,
		WorldEnvironment worldEnvironmentNew,
		LinkingMethod linkingMethodNew,
		ColouringMethod colouringMethodNew
	) {
		ThisWorldEnvironment = worldEnvironmentNew;
		NumOfSpheres = numOfSpheresNew;
		OrbitalSpheres = orbitalSpheresNew;
		
		ThisLinkingMethod = linkingMethodNew;
		ThisColouringMethod = colouringMethodNew;
		
		BaseStations = new List<BaseStation>();
	}
	
	public void AddBaseStation(float lng, float lat) {
		var baseStationScene = ResourceLoader.Load("res://Scenes//BaseStation.tscn") as PackedScene;
		BaseStation baseStation = baseStationScene.Instance() as BaseStation;
		
		baseStation.Init(lng,lat,ThisLinkingMethod,this,ThisWorldEnvironment);
		
		AddChild(baseStation);
		BaseStations.Add(baseStation);
	}
	
	//called when 
	public void ApplyColouringMethod()
	{
		ThisColouringMethod.ColourConstellation(this);
	}
	
	public void ApplyLinking()
	{
		ThisLinkingMethod.Initialise(this);
	}
}
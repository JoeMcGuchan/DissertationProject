using Godot;
using System;
using System.Collections.Generic; 
using System.Linq;

public class Constellation : Spatial
{
	public List<BaseStation> BaseStations;
	
    // This represents a collection of orbital spheres
	public OrbitalSphere[] OrbitalSpheres;
	public int NumOfSpheres;
	public WorldEnvironment ThisWorldEnvironment;
	
	public LinkingMethod ThisLinkingMethod;

	public ColouringMethod ThisColouringMethod;
	
	public float RotationOfEarth;
	
	public Test ThisTest;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
    }
	
	public override void _Process(float delta)
	{
		ThisLinkingMethod.UpdateConstellation(this);
		
		float timeStep = ThisWorldEnvironment.TimeFactor * delta;
		RotationOfEarth = (RotationOfEarth + timeStep * ThisWorldEnvironment.RotationSpeedOfEarth) % (2 * (float) Math.PI);
		
		if (ThisTest.RunContinually)
		{
			ThisTest.Run();	
		}
		
		((Spatial) GetParent().FindNode("Earth")).Rotation = new Vector3(0,RotationOfEarth + DegreeToRadian(ThisWorldEnvironment.EarthRotationOffset),0);
	}
	
	public void RunTest()
	{
		if (!ThisTest.RunContinually)
		{
			ThisTest.Run();
		}
	}
	
	//Gives all the data to the Constellation, but doesn't
	//do the work of creating it just yet
	public void Init(
		OrbitalSphere[] orbitalSpheresNew, 
		int numOfSpheresNew,
		WorldEnvironment worldEnvironmentNew,
		LinkingMethod linkingMethodNew,
		ColouringMethod colouringMethodNew,
		Test newTest
	) {
		ThisWorldEnvironment = worldEnvironmentNew;
		NumOfSpheres = numOfSpheresNew;
		OrbitalSpheres = orbitalSpheresNew;
		
		ThisLinkingMethod = linkingMethodNew;
		ThisColouringMethod = colouringMethodNew;
		
		BaseStations = new List<BaseStation>();
		
		ThisTest = newTest;
		
		RotationOfEarth = 0;
	}
	
	public BaseStation AddBaseStation(float lng, float lat) {
		var baseStationScene = ResourceLoader.Load("res://Scenes//BaseStation.tscn") as PackedScene;
		BaseStation baseStation = baseStationScene.Instance() as BaseStation;
		
		baseStation.Init(lng,lat,ThisLinkingMethod,this,ThisWorldEnvironment);
		
		AddChild(baseStation);
		BaseStations.Add(baseStation);
		
		return baseStation;
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
	
	public List<Vertex> GetAllVertsList()
	{
		List<Vertex> vList = GetAllSatsList().Cast<Vertex>().ToList();
		
		vList.AddRange(BaseStations);
		
		return vList;
	}
	
	public List<Satellite> GetAllSatsList()
	{
		List<Satellite> vList = new List<Satellite>();
		
		foreach (OrbitalSphere orbitalSphere in OrbitalSpheres)
		{
			Orbit[] orbits = orbitalSphere.Orbits;
			
			foreach (Orbit orbit in orbits)
			{
				Satellite[] satellites = orbit.Satellites;
				
				foreach (Satellite satellite in satellites)
				{
					vList.Add(satellite);
				}
			}
		}
		
		return vList;
	}
	
	public void SetMarkedAll(bool b)
	{
		foreach (Vertex v in GetAllVertsList()) {v.Marked = b;}
	}
	
	//disable p percent of all satellites
	public void DisableSatellitesProportion(double p)
	{
		Random rnd = new Random();
		
		foreach (Satellite s in GetAllSatsList())
		{
			if (rnd.NextDouble() < p)
			{
				DeleteSat(s);
			}
		}
	}
	
	public void DisableSatellitesNumber(int n)
	{
		Random rnd = new Random();
		
		List<Satellite> allSats = GetAllSatsList();
		
		for (int i = 0; i < n; i++)
		{
			int x = rnd.Next(allSats.Count);
			DeleteSat(allSats[x]);
			allSats.RemoveAt(x);
		}
	}
	
	public void DeleteSat(Satellite s)
	{
		ThisLinkingMethod.DeleteSat(s);
	}
	
	public void InitialiseTest()
	{
		ThisTest.Init(this);
	}
	
	private float DegreeToRadian(float angle)
	{
   		return (float) Math.PI * angle / 180.0f;
	}
}
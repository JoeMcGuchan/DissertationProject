using Godot;
using System;
using System.Collections.Generic;

public abstract class Test
{
	public System.IO.StreamWriter WriteOut;
	public int RunNumber;
	
	public void CreateFile(String path)
	{
		WriteOut = new System.IO.StreamWriter(path, true);
		RunNumber = 1;
	}
	
	public void WriteLine(String str)
	{
		WriteOut.WriteLine(str);
		Console.WriteLine(str);
		
	}
	
//	public Satellite[][][] GetAllSatsMatrix(Constellation constellation)
//	{
//
//	}
	
	public List<Vertex> GetAllVertsList(Constellation constellation)
	{
		List<Vertex> vList = new List<Vertex>();
		int numOfSpheres = constellation.NumOfSpheres;
		OrbitalSphere[] orbitalSpheres = constellation.OrbitalSpheres;
		
		for(int i = 0; i < numOfSpheres; i++)
		{
			OrbitalSphere orbitalSphere = orbitalSpheres[i];
			int numOfOrbits = orbitalSphere.NumOfOrbits;
			int satellitesPerOrbit = orbitalSphere.SatellitesPerOrbit;
			Orbit[] orbits = orbitalSphere.Orbits;
			
			for(int j = 0; j < numOfOrbits; j++)
			{
				Orbit orbit = orbits[j];
				Satellite[] satellites = orbit.Satellites;
				
				for(int k = 0; k < satellitesPerOrbit; k++)
				{
					vList.Add(satellites[k]);
				}
			}
		}
		
		vList.AddRange(constellation.BaseStations);
		
		return vList;
	}
	
	public abstract void Run();
	
	protected int mod(int x, int m) {
		return (x%m + m)%m;
	}	
}

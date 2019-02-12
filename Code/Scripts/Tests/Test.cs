using Godot;
using System;
using System.Collections.Generic;

public abstract class Test
{
	public System.IO.StreamWriter WriteOut;
	public int RunNumber;
	
	public void Init(String path)
	{
		WriteOut = new System.IO.StreamWriter(path, true);
		RunNumber = 1;
	}
	
	public void WriteLine(String str)
	{
		WriteOut.WriteLine(str);
		
	}
	
//	public Satellite[][][] GetAllSatsMatrix(Constellation constellation)
//	{
//
//	}
	
	public List<Satellite> GetAllSatsList(Constellation constellation)
	{
		List<Satellite> satList = new List<Satellite>();
		int numOfSpheres = constellation.numOfSpheres;
		OrbitalSphere[] orbitalSpheres = constellation.orbitalSpheres;
		
		for(int i = 0; i < numOfSpheres; i++)
		{
			OrbitalSphere orbitalSphere = orbitalSpheres[i];
			int numOfOrbits = orbitalSphere.numOfOrbits;
			int satellitesPerOrbit = orbitalSphere.satellitesPerOrbit;
			Orbit[] orbits = orbitalSphere.orbits;
			
			for(int j = 0; j < numOfOrbits; j++)
			{
				Orbit orbit = orbits[j];
				Satellite[] satellites = orbit.satellites;
				
				for(int k = 0; k < satellitesPerOrbit; k++)
				{
					satList.Add(satellites[k]);
				}
			}
		}
		
		return satList;
	}
	
	public abstract void Run(Constellation constellation);
}

using Godot;
using System;
using System.Collections.Generic;

public class OrbitalSphere : Spatial
{
	// Contains all the information needed to define
	// an orbital sphere

	public Constellation Constellation;
	public Orbit[] Orbits;
	public WorldEnvironment ThisWorldEnvironment;

	public float DistanceAboveCore;

	//This is the true anomaly of satellite 0
	//As all satellites in the same orbital sphere
	//are at the same altitude, we can consider this the
	//same for all orbits
	public float RotationOfOrbit;
	public float AngularVelocity;

	public int NumOfOrbits;
	public float Inclination;
	public int SatellitesPerOrbit;
	public int PhaseOffset;
	
	//number in constellation
	public int ID;

    public override void _Ready()
    {
        AngularVelocity = (float) Math.Sqrt(ThisWorldEnvironment.GM / Math.Pow(DistanceAboveCore, 3));
		RotationOfOrbit = 0;	
		
		UpdateSattelites();
    }

	public override void _Process(float delta) 
	{
		float timeStep = ThisWorldEnvironment.TimeFactor * delta;
		RotationOfOrbit = (RotationOfOrbit + timeStep * AngularVelocity) % (2 * (float) Math.PI);
	}
	
	public void UpdateSattelites()
	{
		for (int i = 0; i < NumOfOrbits; i++) 
		{
			Orbits[i].UpdateSattelites();
		}
	}

	public void Init(
		Orbit[] orbitsNew,
		int numOfOrbitsNew,
		float distanceAboveCoreNew,
		float inclinationNew,
		int sattelitesPerOrbitNew,
		int phaseOffsetNew,
		LinkingMethod linkingMethodNew,
		Constellation constellationNew,
		WorldEnvironment worldEnvironmentNew,
		int id
	) {
		Orbits = orbitsNew;
		NumOfOrbits = numOfOrbitsNew;
		DistanceAboveCore = distanceAboveCoreNew;
		Inclination = inclinationNew;
		SatellitesPerOrbit = sattelitesPerOrbitNew;
		PhaseOffset = phaseOffsetNew;
		//LinkingMethod = linkingMethodNew;
		Constellation = constellationNew;
		ThisWorldEnvironment = worldEnvironmentNew;
		ID = id;
	}
	
	//the c sharp mod function is crap for negative ints so I redefnined it
	int mod(int x, int m) {
    	return (x%m + m)%m;
	}
}
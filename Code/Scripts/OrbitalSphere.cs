using Godot;
using System;
using System.Collections.Generic;

public class OrbitalSphere : Spatial
{
	// Contains all the information needed to define
	// an orbital sphere

	public Constellation constellation;
	public Orbit[] orbits;
	public WorldEnvironment worldEnvironment;

	public float distanceAboveCore;

	//This is the true anomaly of satellite 0
	//As all satellites in the same orbital sphere
	//are at the same altitude, we can consider this the
	//same for all orbits
	public float rotation;
	public float angularVelocity;

	public int numOfOrbits;
	public float inclination;
	public int sattelitesPerOrbit;
	public float phaseOffset;
	
	float timeFactor;

    public override void _Ready()
    {
        angularVelocity = (float) Math.Sqrt(worldEnvironment.GM / Math.Pow(distanceAboveCore, 3));
		rotation = 0;
		
		timeFactor = worldEnvironment.timeFactor;
    }

	public override void _Process(float delta) 
	{
		float timeStep = timeFactor * delta;
		rotation = (rotation + timeStep * angularVelocity) % (2 * (float) Math.PI);

		for (int i = 0; i < numOfOrbits; i++) 
		{
			orbits[i].updateSattelites();
		}
	}

	public void Init(
		Orbit[] orbitsNew,
		int numOfOrbitsNew,
		float distanceAboveCoreNew,
		float inclinationNew,
		int sattelitesPerOrbitNew,
		float phaseOffsetNew,
		Constellation constellationNew,
		WorldEnvironment worldEnvironmentNew
	) {
		orbits = orbitsNew;
		numOfOrbits = numOfOrbitsNew;
		distanceAboveCore = distanceAboveCoreNew;
		inclination = inclinationNew;
		sattelitesPerOrbit = sattelitesPerOrbitNew;
		phaseOffset = phaseOffsetNew;
		constellation = constellationNew;
		worldEnvironment = worldEnvironmentNew;
	}
}

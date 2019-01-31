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
	public int linksPerSat;
	
	public LinkingMethod linkingMethod;
	
	float timeFactor;

    public override void _Ready()
    {
        angularVelocity = (float) Math.Sqrt(worldEnvironment.GM / Math.Pow(distanceAboveCore, 3));
		rotation = 0;
		
		timeFactor = worldEnvironment.timeFactor;
		
//		//AGAIN, THIS NEEDS TO BE BROUGHT INTO THE LINKING METHOD
//		Satellite thisSat;
//		Satellite fSat;
//		Satellite bSat;
//		Satellite rSat;
//		Satellite lSat;
//
//		for (int j = 0; j < numOfOrbits; j++)
//		{
//			for (int k = 0; k < sattelitesPerOrbit; k++)
//			{
//				thisSat = orbits[j].satellites[k];
//				thisSatPos = thisSat.Translation;
//				fSat = orbits[j].satellites[mod(k+1,sattelitesPerOrbit)];
//				bSat = orbits[j].satellites[mod(k-1,sattelitesPerOrbit)];
//				lSat = orbits[mod(j+1,numOfOrbits)].satellites[mod(k+1,sattelitesPerOrbit)];
//				rSat = orbits[mod(j-1,numOfOrbits)].satellites[mod(k-1,sattelitesPerOrbit)];
//				thisSat.setLinks(
//					new Sattelite[] {fSat, bSat, lSat, rSat, thisSat},
//					new float[] {
//						thisSatPos.DistanceTo(fSat.Translation),
//						thisSatPos.DistanceTo(bSat.Translation),
//						thisSatPos.DistanceTo(lSat.Translation),
//						thisSatPos.DistanceTo(rSat.Translation),
//						0f
//						}
//				);
//			}
//		}	
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
		int linksPerSatNew,
		Constellation constellationNew,
		WorldEnvironment worldEnvironmentNew
	) {
		orbits = orbitsNew;
		numOfOrbits = numOfOrbitsNew;
		distanceAboveCore = distanceAboveCoreNew;
		inclination = inclinationNew;
		sattelitesPerOrbit = sattelitesPerOrbitNew;
		phaseOffset = phaseOffsetNew;
		linksPerSat = linksPerSatNew;
		constellation = constellationNew;
		worldEnvironment = worldEnvironmentNew;
	}
	
	//the c sharp mod function is crap for negative ints so I redefnined it
	int mod(int x, int m) {
    	return (x%m + m)%m;
	}
}

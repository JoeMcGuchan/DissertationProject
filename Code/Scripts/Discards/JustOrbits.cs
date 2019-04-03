using Godot;
using System;

public class JustOrbits : LinkingMethod
{
	int NumOfOrbits;
	int SatellitesPerOrbit;
	
	float MaxDist;
	
	public JustOrbits(float maxDistNew) 
	{
		MaxDist = maxDistNew;
	}
	
	public override void Initialise(OrbitalSphere sphere)
	{
		NumOfOrbits = sphere.numOfOrbits;
		SatellitesPerOrbit = sphere.satellitesPerOrbit;
		for (int j = 0; j < NumOfOrbits; j++)
		{
			for (int k = 0; k < SatellitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;

				thisSat.CreateLinks(2);

				int fSatPosy = mod(k+1,SatellitesPerOrbit);
				int bSatPosy = mod(k-1,SatellitesPerOrbit);

				Satellite fSat = sphere.orbits[j].satellites[fSatPosy];
				Satellite bSat = sphere.orbits[j].satellites[bSatPosy];
				
				float fDist = thisSatPos.DistanceTo(fSat.Translation);
				float bDist = thisSatPos.DistanceTo(bSat.Translation);
				
				thisSat.SetLinks(
					new Satellite[] {fSat, bSat},
					new float[] {fDist, bDist}
				);
				
				//we can assume that if we are too far from the sattelites in front and behind, we will
				//be too far away forever
				if (fDist > MaxDist) {thisSat.ClearLink(0);}
				if (bDist > MaxDist) {thisSat.ClearLink(1);}
			}
		}	
	}
	
	public override void Update(OrbitalSphere sphere)
	{
	}
}
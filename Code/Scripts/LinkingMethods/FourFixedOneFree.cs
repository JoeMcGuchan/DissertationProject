using Godot;
using System;

public class FourFixedOneFree : LinkingMethod
{
	// for integer vectors giving the relative positions of the links
	//[[x, y],...]
	int[][] FixedLinks;
	
	int NumOfOrbits;
	int SatellitesPerOrbit;
	int PhaseOffset;
	
	float MaxDist;
	
	public FourFixedOneFree(int[][] fixedLinksNew, float MaxDistNew) 
	{
		FixedLinks = fixedLinksNew;
		MaxDist = MaxDistNew;
	}
	
	public override void Initialise(OrbitalSphere sphere)
	{
		NumOfOrbits = sphere.numOfOrbits;
		SatellitesPerOrbit = sphere.satellitesPerOrbit;
		PhaseOffset = sphere.phaseOffset;
		for (int j = 0; j < NumOfOrbits; j++)
		{
			for (int k = 0; k < SatellitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;

				thisSat.CreateLinks(5);

				int fSatPosx = mod(j+FixedLinks[0][0],NumOfOrbits);
				int rSatPosx = mod(j+FixedLinks[1][0],NumOfOrbits);
				int bSatPosx = mod(j+FixedLinks[2][0],NumOfOrbits);
				int lSatPosx = mod(j+FixedLinks[3][0],NumOfOrbits);

				int fSatPosy = mod(k+FixedLinks[0][1],SatellitesPerOrbit);
				int rSatPosy = mod(k+FixedLinks[1][1],SatellitesPerOrbit);
				int bSatPosy = mod(k+FixedLinks[2][1],SatellitesPerOrbit);
				int lSatPosy = mod(k+FixedLinks[3][1],SatellitesPerOrbit);
				
				//apply translation to account for phase offset
				if (j == 0) {lSatPosy = mod(lSatPosy-PhaseOffset,SatellitesPerOrbit);}
				if (j == NumOfOrbits-1) {rSatPosy = mod(rSatPosy+PhaseOffset,SatellitesPerOrbit);}

				Satellite fSat = sphere.orbits[fSatPosx].satellites[fSatPosy];
				Satellite rSat = sphere.orbits[rSatPosx].satellites[rSatPosy];
				Satellite bSat = sphere.orbits[bSatPosx].satellites[bSatPosy];
				Satellite lSat = sphere.orbits[lSatPosx].satellites[lSatPosy];

				float fDist = thisSatPos.DistanceTo(fSat.Translation);
				float rDist = thisSatPos.DistanceTo(rSat.Translation);
				float bDist = thisSatPos.DistanceTo(bSat.Translation);
				float lDist = thisSatPos.DistanceTo(lSat.Translation);
				
				thisSat.SetLinks(
					new Satellite[] {fSat, bSat, lSat, rSat, thisSat},
					new float[] {fDist, bDist, lDist, rDist, 0f}
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
		//update the two cross links
		for (int j = 0; j < NumOfOrbits; j++)
		{
			for (int k = 0; k < SatellitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;
				Link[] links = thisSat.Links;

				float dist1 = thisSatPos.DistanceTo(links[2].Sat.Translation);
				float dist2 = thisSatPos.DistanceTo(links[3].Sat.Translation);

				if (dist1 < MaxDist) {thisSat.UpdateLink(2, dist1);}
				else if (thisSat.Links[2].Active) {thisSat.ClearLink(2);}
				
				if (dist2 < MaxDist) {thisSat.UpdateLink(3, dist1);}
				else if (thisSat.Links[3].Active) {thisSat.ClearLink(3);}
				
				Satellite closestSat = FindNearestSatteliteInOtherSphere(thisSat);
				float dist3 = thisSatPos.DistanceTo(closestSat.Translation);
				if (dist3 < MaxDist)
				{
					thisSat.SetLink(4, closestSat, thisSatPos.DistanceTo(closestSat.Translation));
				}
				else {thisSat.ClearLink(4);}
			}
		}	
	}
}
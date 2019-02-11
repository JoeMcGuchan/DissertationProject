using Godot;
using System;

public class FourFixedOneFree : LinkingMethod
{
	// for integer vectors giving the relative positions of the links
	//[[x, y],...]
	int[][] fixedLinks;
	
	int numOfOrbits;
	int satellitesPerOrbit;
	
	public FourFixedOneFree(int[][] fixedLinksNew) 
	{
		fixedLinks = fixedLinksNew;
	}
	
	public override void Initialise(OrbitalSphere sphere)
	{
		numOfOrbits = sphere.numOfOrbits;
		satellitesPerOrbit = sphere.satellitesPerOrbit;
		for (int j = 0; j < numOfOrbits; j++)
		{
			for (int k = 0; k < satellitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;

				thisSat.CreateLinks(5);

				int fSatPosx = mod(j+fixedLinks[0][0],numOfOrbits);
				int rSatPosx = mod(j+fixedLinks[1][0],numOfOrbits);
				int bSatPosx = mod(j+fixedLinks[2][0],numOfOrbits);
				int lSatPosx = mod(j+fixedLinks[3][0],numOfOrbits);

				int fSatPosy = mod(k+fixedLinks[0][1],satellitesPerOrbit);
				int rSatPosy = mod(k+fixedLinks[1][1],satellitesPerOrbit);
				int bSatPosy = mod(k+fixedLinks[2][1],satellitesPerOrbit);
				int lSatPosy = mod(k+fixedLinks[3][1],satellitesPerOrbit);

				Satellite fSat = sphere.orbits[fSatPosx].satellites[fSatPosy];
				Satellite rSat = sphere.orbits[rSatPosx].satellites[rSatPosy];
				Satellite bSat = sphere.orbits[bSatPosx].satellites[bSatPosy];
				Satellite lSat = sphere.orbits[lSatPosx].satellites[lSatPosy];

				thisSat.SetLinks(
					new Satellite[] {fSat, bSat, lSat, rSat, thisSat},
					new float[] 
					{
						thisSatPos.DistanceTo(fSat.Translation),
						thisSatPos.DistanceTo(bSat.Translation),
						thisSatPos.DistanceTo(lSat.Translation),
						thisSatPos.DistanceTo(rSat.Translation),
						0f
					}
				);
			}
		}	
	}
	
	public override void Update(OrbitalSphere sphere)
	{
		//update the two cross links
		for (int j = 0; j < numOfOrbits; j++)
		{
			for (int k = 0; k < satellitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;
				Link[] links = thisSat.Links;

				float dist1 = thisSatPos.DistanceTo(links[2].Sat.Translation);
				float dist2 = thisSatPos.DistanceTo(links[3].Sat.Translation);

				thisSat.UpdateLink(2, dist1);
				thisSat.UpdateLink(3, dist2);
				
				Satellite closestSat = FindNearestSatteliteInOtherSphere(thisSat);
				thisSat.SetLink(4, closestSat, thisSatPos.DistanceTo(closestSat.Translation));
			}
		}	
	}
}
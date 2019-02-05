using Godot;
using System;

public class FourFixedOneFree : LinkingMethod
{
	// for integer vectors giving the relative positions of the links
	//[[x, y],...]
	int[][] fixedLinks;
	
	public FourFixedOneFree(int[][] fixedLinksNew) 
	{
		fixedLinks = fixedLinksNew;
	}
	
	public override void Initialise(OrbitalSphere sphere)
	{
		int numOfOrbits = sphere.numOfOrbits;
		int sattelitesPerOrbit = sphere.sattelitesPerOrbit;
		for (int j = 0; j < numOfOrbits; j++)
		{
			for (int k = 0; k < sattelitesPerOrbit; k++)
			{
				Satellite thisSat = sphere.orbits[j].satellites[k];
				Vector3 thisSatPos = thisSat.Translation;

				thisSat.CreateLinks(5);

				int fSatPosx = mod(j+fixedLinks[0][0],numOfOrbits);
				int rSatPosx = mod(j+fixedLinks[1][0],numOfOrbits);
				int bSatPosx = mod(j+fixedLinks[2][0],numOfOrbits);
				int lSatPosx = mod(j+fixedLinks[3][0],numOfOrbits);

				int fSatPosy = mod(k+fixedLinks[0][1],sattelitesPerOrbit);
				int rSatPosy = mod(k+fixedLinks[1][1],sattelitesPerOrbit);
				int bSatPosy = mod(k+fixedLinks[2][1],sattelitesPerOrbit);
				int lSatPosy = mod(k+fixedLinks[3][1],sattelitesPerOrbit);

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
		
	}
}
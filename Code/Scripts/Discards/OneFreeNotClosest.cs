using Godot;
using System;

public class OneFreeNotClosest : LinkingMethod
{
	int numOfOrbits;
	int satellitesPerOrbit;
	
	public OneFreeNotClosest() 
	{

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

				thisSat.CreateLinks(1);

				thisSat.SetLinks(
					new Satellite[] {thisSat},
					new float[] {0f}
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
				
				Satellite closestSat = FindNearestSatteliteInOtherSphere(thisSat);
				thisSat.SetLink(0, closestSat, thisSatPos.DistanceTo(closestSat.Translation));
			}
		}
	}
}

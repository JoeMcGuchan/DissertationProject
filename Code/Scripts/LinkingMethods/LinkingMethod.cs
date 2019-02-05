using Godot;
using System;

public abstract class LinkingMethod : Godot.Object
{
    // This abstract class describes the way that a sattelite forms
	// links with it's nieghtbors
	
	// each orbitalsphere is given a linking method individually (as
	// we might want different spheres to have differentmethods).
	
	//this method very simply searches through all the other spheres
	//(and the opposing side of this sphere) and find a closest
	//sattelite to connect with
	public Satellite findNearestSatteliteInOtherSphere(Satellite thisSat) {		
		Satellite closeSattelite = thisSat;
		
		OrbitalSphere orbitalSphere = thisSat.orbit.orbitalSphere;
		Constellation constellation = orbitalSphere.constellation;
		
		int[] id = thisSat.id;
		
		int numOfOrbits = orbitalSphere.numOfOrbits;
		int satsPerOrbit = orbitalSphere.sattelitesPerOrbit;
		int[] newid = new int[] 
		{
			id[0],
			(id[1] + (numOfOrbits / 2)) % numOfOrbits,
			(id[2] + (satsPerOrbit / 2)) % satsPerOrbit
		};
		
		return closeSattelite;
		
		//TODO
	}
	
	//the priniple of this is thus, when finding the closest sattelite
	//I don't have to search every sat on the sphere, as "closeness" is
	//a smooth function with two peaks, this will find one of those
	//peaks
	public Satellite findNearestSatteliteInSphere(
		Satellite[][] sphere, 
		Vector3 pos, 
		int[] startingPoint,
		int width,
		int height
	) {
		int j = startingPoint[0];
		int k = startingPoint[1];
		
		Vector3 testPos = sphere[j][k].Translation;
		float testDistance = testPos.DistanceTo(pos);
		
		//first we will move widthwise
		int jNext = (j + 1) % width;
		Vector3 nextPos = sphere[jNext][k].Translation;
		float nextDistance = nextPos.DistanceTo(pos);
		
		//see if distance is deacreasing or increasing, and use this to determine how to move
		bool movingFowards = (nextDistance < testDistance);
		
		//if we're not moving forwards, we need to change our nextpos
		if (!movingFowards) 
		{
			jNext = mod(j - 1, width);
			nextPos = sphere[jNext][k].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		//Now we move in our chosen direction until we stop getting closer
		while (nextDistance < testDistance) 
		{
			j = jNext;
			testPos = nextPos;
			testDistance = nextDistance;
			
			if (movingFowards)
			{
				jNext = (j + 1) % width;
			} else {
				jNext = mod(j - 1, width);
			}
			
			nextPos = sphere[jNext][k].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		// now repeat for height
		int kNext = (k + 1) % height;
		nextPos = sphere[j][kNext].Translation;
		nextDistance = nextPos.DistanceTo(pos);
		
		movingFowards = (nextDistance < testDistance);
		
		//if we're not moving forwards, we need to change our nextpos
		if (!movingFowards) 
		{
			kNext = mod(k - 1, height);
			nextPos = sphere[j][kNext].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		//Now we move in our chosen direction until we stop getting closer
		while (nextDistance < testDistance) 
		{
			k = kNext;
			testPos = nextPos;
			testDistance = nextDistance;
			
			if (movingFowards)
			{
				kNext = (k + 1) % height;
			} else {
				kNext = mod(k - 1, height);
			}
			
			nextPos = sphere[j][kNext].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		return sphere[j][k];
	}
	
	//the c sharp mod function is crap for negative ints so I redefnined it
	int mod(int x, int m) {
    	return (x%m + m)%m;
	}
	
	//this method is used by an orbital sphere to initialise the positions of all it's orbits
	public 
}
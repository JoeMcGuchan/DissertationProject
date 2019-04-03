	//this method very simply searches through all the other spheres
	//(and the opposing side of this sphere) and find a closest
	//sattelite to connect with
	public Satellite FindNearestSatteliteInOtherSphere(Satellite testSat)
	{
		Vector3 pos = testSat.Translation;
		
		Constellation constellation = testSat.Orbit.orbitalSphere.constellation;
		
		Satellite closestSat = testSat;
		float closestSatDistance = float.MaxValue;
		
		int[] id = testSat.ID;
		int thisSphereNumber = id[0];
		
		int numOfSpheres = constellation.numOfSpheres;
		OrbitalSphere[] orbitalSpheres = constellation.orbitalSpheres;
		
		for (int i = 0; i < numOfSpheres; i++)
		{
			if (i == thisSphereNumber)
			{
				//get the one other point in same sphere
				OrbitalSphere orbitalSphere = orbitalSpheres[i];
				int numOfOrbits = orbitalSphere.numOfOrbits;
				
				int[] startingPoint = new int[] 
				{
					(id[1] + (numOfOrbits / 2)) % numOfOrbits,
					id[2]
				};
				
				Satellite thisSat = FindNearestSatelliteInSphere(
					orbitalSphere, 
					pos, 
					startingPoint	
				);
				float thisDist = pos.DistanceTo(thisSat.Translation);
				
				if (thisDist < closestSatDistance)
				{
					closestSatDistance = thisDist;
					closestSat = thisSat;
				}
			}
			else 
			{				//get the one other point in same sphere
				OrbitalSphere orbitalSphere = orbitalSpheres[i];
				int numOfOrbits = orbitalSphere.numOfOrbits;
				
				int[] startingPoint1 = new int[] 
				{
					0,
					0
				};
				Satellite sat1 = FindNearestSatelliteInSphere(
					orbitalSphere, 
					pos, 
					startingPoint1	
				);
				float dist1 = pos.DistanceTo(sat1.Translation);
				
				if (dist1 < closestSatDistance)
				{
					closestSatDistance = dist1;
					closestSat = sat1;
				}
				
				int[] startingPoint2 = new int[] 
				{
					numOfOrbits / 2,
					0
				};
				Satellite sat2 = FindNearestSatelliteInSphere(
					orbitalSphere, 
					pos, 
					startingPoint2	
				);
				float dist2 = pos.DistanceTo(sat1.Translation);
				
				if (dist2 < closestSatDistance)
				{
					closestSatDistance = dist2;
					closestSat = sat2;
				}
			}
		}
		
		return closestSat;
	}
	
	public Satellite FindNearestSatelliteInSphere(
		OrbitalSphere sphere, 
		Vector3 pos, 
		int[] startingPoint	
	) {
		// x = ORBIT NUMBER
		// y = SATTELITE NUMBER
		int x = startingPoint[0];
		int y = startingPoint[1];
		
		int numOfOrbits = sphere.numOfOrbits;
		int satellitesPerOrbit = sphere.satellitesPerOrbit;
		
		Orbit[] orbits = sphere.orbits;
		
		Orbit thisOrb = orbits[x];
		Satellite[] theseSats = thisOrb.satellites;
		Satellite thisSat = theseSats[y];
		float thisDist = pos.DistanceTo(thisSat.Translation);
		
		int yup = mod(y + 1, satellitesPerOrbit);
		Satellite upSat = theseSats[yup];
		float upDist = pos.DistanceTo(upSat.Translation);
		
		int ydown = mod(y - 1, satellitesPerOrbit);
		Satellite downSat = theseSats[ydown];
		float downDist = pos.DistanceTo(downSat.Translation);
		
		int xleft = mod(x + 1, numOfOrbits);
		Orbit leftOrb = orbits[xleft];
		Satellite[] leftSats = leftOrb.satellites;
		Satellite leftSat = leftSats[y];
		float leftDist = pos.DistanceTo(leftSat.Translation);
		
		int xright = mod(x - 1, numOfOrbits);
		Orbit rightOrb = orbits[xright];
		Satellite[] rightSats = rightOrb.satellites;
		Satellite rightSat = rightSats[y];
		float rightDist = pos.DistanceTo(rightSat.Translation);
	
		bool looping = true;
		
		while (looping)
		{
			if (upDist < thisDist)
			{
				//this becomes new ydown
				ydown = y;
				downSat = thisSat;
				downDist = thisDist;
				
				//yup becomes new centre
				y = yup;
				thisSat = upSat;
				thisDist = upDist;
				
				//compute new yup
				yup = mod(y + 1, satellitesPerOrbit);
				upSat = theseSats[yup];
				upDist = pos.DistanceTo(upSat.Translation);
				
				//compute new left and right
				leftSat = leftSats[y];
				leftDist = pos.DistanceTo(leftSat.Translation);
				
				rightSat = rightSats[y];
				rightDist = pos.DistanceTo(rightSat.Translation);
			}
			else if (downDist < thisDist)
			{
				//this becomes new yup
				yup = y;
				upSat = thisSat;
				upDist = thisDist;
				
				//ydown becomes new centre
				y = ydown;
				thisSat = downSat;
				thisDist = downDist;
				
				//compute new ydown
				ydown = mod(y - 1, satellitesPerOrbit);
				downSat = theseSats[ydown];
				downDist = pos.DistanceTo(downSat.Translation);
				
				//compute new left and right
				leftSat = leftSats[y];
				leftDist = pos.DistanceTo(leftSat.Translation);
				
				rightSat = rightSats[y];
				rightDist = pos.DistanceTo(rightSat.Translation);
			}
			else if (leftDist < thisDist)
			{
				//this becomes the new right
				xright = x;
				rightOrb = thisOrb;
				rightSats = theseSats;
				rightSat = thisSat;
				rightDist = thisDist;
				
				//left becomes new this
				x = xleft;
				thisOrb = leftOrb;
				theseSats = leftSats;
				thisSat = leftSat;
				thisDist = leftDist;
				
				//compute new left
				xleft = mod(x + 1, numOfOrbits);
				leftOrb = orbits[xleft];
				leftSats = leftOrb.satellites;
				leftSat = leftSats[y];
				leftDist = pos.DistanceTo(leftSat.Translation);
				
				//compute new up and down
				upSat = theseSats[yup];
				upDist = pos.DistanceTo(upSat.Translation);
				
				downSat = theseSats[ydown];
				downDist = pos.DistanceTo(downSat.Translation);
			}
			else if (rightDist < thisDist)
			{
				//this becomes the new left
				xleft = x;
				leftOrb = thisOrb;
				leftSats = theseSats;
				leftSat = thisSat;
				leftDist = thisDist;
				
				//right becomes new this
				x = xright;
				thisOrb = rightOrb;
				theseSats = rightSats;
				thisSat = rightSat;
				thisDist = rightDist;
				
				//compute new right
				xright = mod(x - 1, numOfOrbits);
				rightOrb = orbits[xleft];
				rightSats = rightOrb.satellites;
				rightSat = rightSats[y];
				rightDist = pos.DistanceTo(rightSat.Translation);
				
				//compute new up and down
				upSat = theseSats[yup];
				upDist = pos.DistanceTo(upSat.Translation);
				
				downSat = theseSats[ydown];
				downDist = pos.DistanceTo(downSat.Translation);
			}
			else {looping = false;}
		}
	
		return thisSat;
	}
	
	//finds the nearest satellite not already connected to it
	public Satellite FindNearestUnconnectedSat(
		OrbitalSphere sphere, 
		Satellite satellite
	) {
		//this time we have to loop through all satellites as there's no effective way to 
		//TODO
		return satellite;
	}
	
	//a slightly simpler (read: worse) algorithm
	public Satellite FindNearestSatelliteInSphereSimple(
		OrbitalSphere sphere, 
		Vector3 pos, 
		int[] startingPoint
	) {
		int j = startingPoint[0];
		int k = startingPoint[1];
		
		int numOfOrbits = sphere.numOfOrbits;
		int satellitesPerOrbit = sphere.satellitesPerOrbit;
		
		Orbit[] orbits = sphere.orbits;
		
		Vector3 testPos = orbits[j].satellites[k].Translation;		
		float testDistance = testPos.DistanceTo(pos);
		
		//first we will move widthwise
		int jNext = (j + 1) % numOfOrbits;
		Vector3 nextPos = orbits[jNext].satellites[k].Translation;
		float nextDistance = nextPos.DistanceTo(pos);
		
		//see if distance is deacreasing or increasing, and use this to determine how to move
		bool movingFowards = (nextDistance < testDistance);
		
		//if we're not moving forwards, we need to change our nextpos
		if (!movingFowards) 
		{
			jNext = mod(j - 1, numOfOrbits);
			nextPos = orbits[jNext].satellites[k].Translation;
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
				jNext = (j + 1) % numOfOrbits;
			} else {
				jNext = mod(j - 1, numOfOrbits);
			}
			
			nextPos = orbits[jNext].satellites[k].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		// now repeat for height
		Orbit orbit = orbits[j];
		Satellite[] satellites = orbit.satellites;
		
		int kNext = (k + 1) % satellitesPerOrbit;
		nextPos = satellites[kNext].Translation;
		nextDistance = nextPos.DistanceTo(pos);
		
		movingFowards = (nextDistance < testDistance);
		
		//if we're not moving forwards, we need to change our nextpos
		if (!movingFowards) 
		{
			kNext = mod(k - 1, satellitesPerOrbit);
			nextPos = satellites[kNext].Translation;
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
				kNext = (k + 1) % satellitesPerOrbit;
			} else {
				kNext = mod(k - 1, satellitesPerOrbit);
			}
			
			nextPos = satellites[kNext].Translation;
			nextDistance = nextPos.DistanceTo(pos);
		}
		
		return satellites[k];
	}
	
//	//so this one is a little more tricky to describe, bascially, how do you choose a nearest satellite
//	//when all options are taken?
//	public Satellite FindNearestSatelliteToExcluding(
//	) {
//
//	}
using Godot;
using System;
using System.Collections.Generic; 

public class Orbit : Spatial
{
	// Contains all the information needed to define
	// an orbit
	
	public OrbitalSphere orbitalSphere;
	public Satellite[] satellites;
	//note: this is rechnically redundant, but I've included it here 
	//for efficency
	int precision;
	int numOfSatellites;
	public WorldEnvironment worldEnvironment;
	
	public float longditudonalOffset;
	
	public Transform[] orbitPoints;

    public override void _Ready()
    {
		precision = worldEnvironment.precision;
		numOfSatellites = orbitalSphere.satellitesPerOrbit;
		
		ComputeOrbitPoints();
    }
	
	public void Init(
		Satellite[] satellitesNew, 
		OrbitalSphere orbitalSphereNew, 
		WorldEnvironment worldEnvironemntNew,
		float longditudonalOffsetNew
	) {
		worldEnvironment = worldEnvironemntNew;

		satellites = satellitesNew;
		orbitalSphere = orbitalSphereNew;
		longditudonalOffset = longditudonalOffsetNew;
	}
	
	public void ComputeOrbitPoints() 
	{

		float distanceAboveCore = orbitalSphere.distanceAboveCore;
		float inclination = orbitalSphere.inclination;
		float phaseOffset = orbitalSphere.phaseOffset;
		orbitPoints = new Transform[precision];
		for (int i = 0; i < precision; i++) 
		{
			float trueAnomaly = (float) Math.PI * 2 * (i + phaseOffset) / precision;
			
//			// we start with a point in the x direction
//			var newPos = new Vector3(distanceAboveCore, 0, 0);
//
//			// first we treat our equatorial plane as our orbital plane
//			newPos = newPos.Rotated(new Vector3(0,1,0),trueAnomaly);
//
//			// then we apply our inclination
//			newPos = newPos.Rotated(new Vector3(1,0,0),inclination);
//
//			// finally we apply our offset
//			newPos = newPos.Rotated(new Vector3(0,1,0),longditudonalOffset);
//
			// we start with our neutral basis
			Basis basis = new Basis(new Vector3(1,0,0), new Vector3(0,1,0), new Vector3(0,0,1));
			
			// first we treat our equatorial plane as our orbital plane
			basis = basis.Rotated(new Vector3(0,1,0),trueAnomaly);
	
			// then we apply our inclination
			basis = basis.Rotated(new Vector3(1,0,0),inclination);
	
			// finally we apply our offset
			basis = basis.Rotated(new Vector3(0,1,0),longditudonalOffset);
			
			// now x should be pointing in direction of sattelite, so we transform like thus
			Vector3 pos = basis.Xform(new Vector3(distanceAboveCore, 0, 0));
		
			orbitPoints[i] = new Transform
			(
				basis,
				pos
			);
		}
	}
	
	public void updateSattelites() 
	{
		float trueAnomaly = orbitalSphere.rotation;
		
		//cast to float to get modulo to work
		float precisionf = (float) precision;
		
		//translate true anomaly into displacement in the array
		float arrayPos = trueAnomaly * precision / (2 * (float) Math.PI);
		float arrayGap = precisionf / (float) numOfSatellites;		
		
		for (int i = 0; i < numOfSatellites; i++) 
		{

			int arrayValue = (int) Math.Floor(arrayPos);
			float arrayDisplacement = arrayPos % 1.0f;
			
			int val1 = arrayValue;
			int val2 = (arrayValue + 1) % precision;
			
			Transform t1 = orbitPoints[val1];
			Transform t2 = orbitPoints[val2];
			
			satellites[i].Transform = new Transform(
				t1.basis.x.LinearInterpolate(t2.basis.x,arrayDisplacement),
				t1.basis.y.LinearInterpolate(t2.basis.y,arrayDisplacement),
				t1.basis.z.LinearInterpolate(t2.basis.z,arrayDisplacement),
				t1.origin.LinearInterpolate(t2.origin,arrayDisplacement)
			);
	
			arrayPos = (arrayPos + arrayGap) % precisionf;
		}
	}
}
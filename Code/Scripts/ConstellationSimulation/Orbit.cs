using Godot;
using System;
using System.Collections.Generic; 

public class Orbit : Spatial
{
	// Contains all the information needed to define
	// an orbit
	
	public OrbitalSphere OrbitalSphere;
	public Satellite[] Satellites;
	
	public float PhaseOffset;
	public float LongditudonalOffset;
	
	public WorldEnvironment ThisWorldEnvironment;
	
	public Transform[] OrbitPoints;
	
	public int ID;

    public override void _Ready()
    {
		ComputeOrbitPoints();

    }
	
	public override void _Process(float delta)
	{
		UpdateSattelites();
		OrbitalSphere.Constellation.ThisLinkingMethod.UpdateOrbit(this);
	}
	
	public void Init(
		Satellite[] satellitesNew, 
		OrbitalSphere orbitalSphereNew, 
		WorldEnvironment worldEnvironemntNew,
		float longditudonalOffsetNew,
		float phaseOffsetNew,
		int id
	) {
		ThisWorldEnvironment = worldEnvironemntNew;

		Satellites = satellitesNew;
		OrbitalSphere = orbitalSphereNew;
		LongditudonalOffset = longditudonalOffsetNew;
		PhaseOffset = phaseOffsetNew;
		ID = id;
	}
	
	public void ComputeOrbitPoints() 
	{
		int precision = ThisWorldEnvironment.Precision;
		float DistanceAboveCore = OrbitalSphere.DistanceAboveCore;
		float Inclination = OrbitalSphere.Inclination;
		OrbitPoints = new Transform[precision];
		for (int i = 0; i < precision; i++) 
		{
			float trueAnomaly = (float) Math.PI * 2 * (i + PhaseOffset) / precision;
			
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
			basis = basis.Rotated(new Vector3(1,0,0),Inclination);
	
			// finally we apply our offset
			basis = basis.Rotated(new Vector3(0,1,0),LongditudonalOffset);
			
			// now x should be pointing in direction of sattelite, so we transform like thus
			Vector3 pos = basis.Xform(new Vector3(DistanceAboveCore, 0, 0));
		
			OrbitPoints[i] = new Transform
			(
				basis,
				pos
			);
		}
	}
	
	public void UpdateSattelites() 
	{
		float trueAnomaly = OrbitalSphere.RotationOfOrbit;
		int satellitesPerSphere = OrbitalSphere.SatellitesPerOrbit;
		
		//translate true anomaly into displacement in the array
		float arrayPos = trueAnomaly * ThisWorldEnvironment.Precision / (2 * (float) Math.PI);
		float arrayGap = ((float) ThisWorldEnvironment.Precision) / (float) satellitesPerSphere;
		
		for (int i = 0; i < satellitesPerSphere; i++) 
		{

			int arrayValue = (int) Math.Floor(arrayPos);
			float arrayDisplacement = arrayPos % 1.0f;
			
			int val1 = arrayValue;
			int val2 = (arrayValue + 1) % ThisWorldEnvironment.Precision;
			
			Transform t1 = OrbitPoints[val1];
			Transform t2 = OrbitPoints[val2];
			
			Satellites[i].Transform = new Transform(
				t1.basis.x.LinearInterpolate(t2.basis.x,arrayDisplacement),
				t1.basis.y.LinearInterpolate(t2.basis.y,arrayDisplacement),
				t1.basis.z.LinearInterpolate(t2.basis.z,arrayDisplacement),
				t1.origin.LinearInterpolate(t2.origin,arrayDisplacement)
			);
	
			arrayPos = (arrayPos + arrayGap) %  ((float) ThisWorldEnvironment.Precision);
		}
	}
}
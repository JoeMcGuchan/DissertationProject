using Godot;
using System;

//this class contains all the data needed to describe a constellation
//hoever it doesn't actually construct the consteallation

public class ConstellationDescription
{
	public int NumOfSpheres;
	
	public int[] OrbitalPlanesPerSphere; 
	public int[] SattelitesPerPlanePerSphere;
	public int[] Altitudes;
	public float[] Inclinations;
	public int[] PhaseOffsets;
	public float[] TimeOffsets;
	
	public LinkingMethod ThisLinkingMethod;
	public ColouringMethod ThisColouringMethod;

	public ConstellationDescription(
		int numOfSpheresNew,
		int[] orbitalPlanesPerSphereNew, 
		int[] sattelitesPerPlanePerSphereNew,
		int[] altitudesNew,
		float[] inclinationsNew,
		int[] phaseOffsetsNew,
		float[] timeOffsetsNew,
		LinkingMethod linkingMethodsNew,
		ColouringMethod colouringMethodNew
	) {
		NumOfSpheres = numOfSpheresNew;
		
		OrbitalPlanesPerSphere = orbitalPlanesPerSphereNew; 
		SattelitesPerPlanePerSphere = sattelitesPerPlanePerSphereNew;
		Altitudes = altitudesNew;
		Inclinations = inclinationsNew;
		PhaseOffsets = phaseOffsetsNew;
		TimeOffsets = timeOffsetsNew;
		
		ThisLinkingMethod = linkingMethodsNew;
		ThisColouringMethod = colouringMethodNew;
	}

	// creates a constellation from the inputs
	// orbital planes[], sattelites per plane[], altitude[], inlination[], numOfSphere
	public Constellation Create(WorldEnvironment worldEnvironment) 
	{
		var constellationScene = ResourceLoader.Load("res://Scenes//Constellation.tscn") as PackedScene;
		var orbitalSphereScene = ResourceLoader.Load("res://Scenes//OrbitalSphere.tscn") as PackedScene;
		var orbitScene = ResourceLoader.Load("res://Scenes//Orbit.tscn") as PackedScene;
		var satteliteScene = ResourceLoader.Load("res://Scenes//Satellite.tscn") as PackedScene;

		Constellation newConstellation = constellationScene.Instance() as Constellation;

		OrbitalSphere[] orbitalSpheresNew = new OrbitalSphere[NumOfSpheres];

		for (int i = 0; i < NumOfSpheres; i++) 
		{
			OrbitalSphere newOrbitalSphere = orbitalSphereScene.Instance() as OrbitalSphere;

			int orbitalPlanes = OrbitalPlanesPerSphere[i];
			int sattelitesPerPlane = SattelitesPerPlanePerSphere[i];
			float distanceAboveCore = Altitudes[i] / 1000 + worldEnvironment.SizeOfEarth;
			float inclination = Inclinations[i] * (float) Math.PI * 2 / 360;
			int phaseOffset = PhaseOffsets[i];

			Orbit[] orbits = new Orbit[orbitalPlanes];

			for (int j = 0; j < orbitalPlanes; j++) 
			{
				Orbit newOrbit = orbitScene.Instance() as Orbit;

				Satellite[] newSats = new Satellite[sattelitesPerPlane];

				for (int k = 0; k < sattelitesPerPlane; k++) 
				{
					Satellite newSat = satteliteScene.Instance() as Satellite;

					newSat.Init(k, newOrbit, worldEnvironment);

					newSats[k] = newSat;
					newOrbit.AddChild(newSat);
				}

				float longditudonalOffset = j * 2 * (float) Math.PI / orbitalPlanes;

				newOrbit.Init(
					newSats,
					newOrbitalSphere, 
					worldEnvironment,
					longditudonalOffset,
					((float) phaseOffset) * j / orbitalPlanes,
					j
				);

				orbits[j] = newOrbit;
				newOrbitalSphere.AddChild(newOrbit);
			}

			newOrbitalSphere.Init(
				orbits, 
				orbitalPlanes, 
				distanceAboveCore,
				inclination,
				sattelitesPerPlane,
				phaseOffset,
				ThisLinkingMethod,
				newConstellation,
				worldEnvironment,
				i
			);

			orbitalSpheresNew[i] = newOrbitalSphere;
			newConstellation.AddChild(newOrbitalSphere);
		}

		newConstellation.Init(
			orbitalSpheresNew, 
			NumOfSpheres,
			worldEnvironment,
			ThisLinkingMethod,
			ThisColouringMethod
		);

		return newConstellation;
	}
}
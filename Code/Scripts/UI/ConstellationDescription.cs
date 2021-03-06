using Godot;
using System;

//this class contains all the data needed to describe a constellation
//hoever it doesn't actually construct the consteallation

//simple template just to make my code cleaner
public class ConstellationDescriptionTemplate
{
	public int NumOfSpheres;
	
	public int[] OrbitalPlanesPerSphere; 
	public int[] SattelitesPerPlanePerSphere;
	public int[] Altitudes;
	public float[] Inclinations;
	public int[] PhaseOffsets;
	public float[] TimeOffsets;
	
	public ConstellationDescriptionTemplate(
		int numOfSpheresNew,
		int[] orbitalPlanesPerSphereNew, 
		int[] sattelitesPerPlanePerSphereNew,
		int[] altitudesNew,
		float[] inclinationsNew,
		int[] phaseOffsetsNew,
		float[] timeOffsetsNew
	) {
		NumOfSpheres = numOfSpheresNew;
		
		OrbitalPlanesPerSphere = orbitalPlanesPerSphereNew; 
		SattelitesPerPlanePerSphere = sattelitesPerPlanePerSphereNew;
		Altitudes = altitudesNew;
		Inclinations = inclinationsNew;
		PhaseOffsets = phaseOffsetsNew;
		TimeOffsets = timeOffsetsNew;
	}
}

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
	
	public Test ThisTest;

	public ConstellationDescription(
		int numOfSpheresNew,
		int[] orbitalPlanesPerSphereNew, 
		int[] sattelitesPerPlanePerSphereNew,
		int[] altitudesNew,
		float[] inclinationsNew,
		int[] phaseOffsetsNew,
		float[] timeOffsetsNew,
		LinkingMethod linkingMethodsNew,
		ColouringMethod colouringMethodNew,
		Test newTest
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
		
		ThisTest = newTest;
	}
	
	public ConstellationDescription(
		ConstellationDescriptionTemplate constellationDescriptionTemplate,
		LinkingMethod linkingMethodsNew,
		ColouringMethod colouringMethodNew,
		Test newTest
	) {
		NumOfSpheres = constellationDescriptionTemplate.NumOfSpheres;
		
		OrbitalPlanesPerSphere = constellationDescriptionTemplate.OrbitalPlanesPerSphere; 
		SattelitesPerPlanePerSphere = constellationDescriptionTemplate.SattelitesPerPlanePerSphere;
		Altitudes = constellationDescriptionTemplate.Altitudes;
		Inclinations = constellationDescriptionTemplate.Inclinations;
		PhaseOffsets = constellationDescriptionTemplate.PhaseOffsets;
		TimeOffsets = constellationDescriptionTemplate.TimeOffsets;
		
		ThisLinkingMethod = linkingMethodsNew;
		ThisColouringMethod = colouringMethodNew;
		
		ThisTest = newTest;
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
			float distanceAboveCore = ((float) Altitudes[i]) / 1000f + worldEnvironment.SizeOfEarth;
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

					newSat.Init(
						k, 
						newOrbit, 
						(2 * (float) Math.PI * k) / sattelitesPerPlane,
						worldEnvironment
					);

					newSats[k] = newSat;
					newOrbit.AddChild(newSat);
				}

				float longditudonalOffset = j * 2 * (float) Math.PI / orbitalPlanes;

				newOrbit.Init(
					newSats,
					newOrbitalSphere, 
					worldEnvironment,
					longditudonalOffset,
					((float) phaseOffset) * j / (float) orbitalPlanes,
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
			ThisColouringMethod,
			ThisTest
		);

		return newConstellation;
	}
}
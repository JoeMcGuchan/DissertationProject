using Godot;
using System;

//this class contains all the data needed to describe a constellation
//hoever it doesn't actually construct the consteallation

public class ConstellationDescription : Godot.Object
{
//	public int[] orbitalPlaness; 
//	public int[] sattelitesPerPlanes;
//	public int[] altitudes;
//	public float[] inclinations;
//	public float[] phaseOffsets;
//	public LinkingMethod[] linkingMethods;
//	public int numOfSpheres;
//
//	public ConstellationDescription(
//		int[] orbitalPlanessNew, 
//		int[] sattelitesPerPlanesNew,
//		int[] altitudesNew,
//		float[] inclinationsNew,
//		float[] phaseOffsetsNew,
//		LinkingMethod[] linkingMethodsNew,
//		int numOfSpheresNew
//	) {
//		orbitalPlaness = orbitalPlanessNew; 
//		sattelitesPerPlanes = sattelitesPerPlanesNew;
//		altitudes = altitudesNew;
//		inclinations = inclinationsNew;
//		phaseOffsets = phaseOffsetsNew;
//		linkingMethods = linkingMethodsNew;
//		numOfSpheres = numOfSpheresNew;
//	}
//
//		// creates a constellation from the inputs
//	// orbital planes[], sattelites per plane[], altitude[], inlination[], numOfSphere
//	public Constellation Create(WorldEnvironment worldEnvironment) 
//	{
//		var constellationScene = ResourceLoader.Load("res://Scenes//Constellation.tscn") as PackedScene;
//		var orbitalSphereScene = ResourceLoader.Load("res://Scenes//OrbitalSphere.tscn") as PackedScene;
//		var orbitScene = ResourceLoader.Load("res://Scenes//Orbit.tscn") as PackedScene;
//		var satteliteScene = ResourceLoader.Load("res://Scenes//Satellite.tscn") as PackedScene;
//
//		Constellation newConstellation = constellationScene.Instance() as Constellation;
//
//		OrbitalSphere[] orbitalSpheresNew = new OrbitalSphere[numOfSpheres];
//
//		for (int i = 0; i < numOfSpheres; i++) 
//		{
//			OrbitalSphere newOrbitalSphere = orbitalSphereScene.Instance() as OrbitalSphere;
//
//			int orbitalPlanes = orbitalPlaness[i];
//			int sattelitesPerPlane = sattelitesPerPlanes[i];
//			float distanceAboveCore = altitudes[i] / 1000 + worldEnvironment.sizeOfEarth;
//			float inclination = inclinations[i] * (float) Math.PI * 2 / 360;
//			float phaseOffset = phaseOffsets[i];
//			LinkingMethod linkingMethod = linkingMethods[i];
//
//			Orbit[] orbits = new Orbit[orbitalPlanes];
//
//			for (int j = 0; j < orbitalPlanes; j++) 
//			{
//				Orbit newOrbit = orbitScene.Instance() as Orbit;
//
//				Satellite[] newSats = new Satellite[sattelitesPerPlane];
//
//				for (int k = 0; k < sattelitesPerPlane; k++) 
//				{
//					Satellite newSat = satteliteScene.Instance() as Satellite;
//
//					newSat.Init(i, j, k, newOrbit);
//
//					newSats[k] = newSat;
//					newOrbit.AddChild(newSat);
//				}
//
//				float longditudonalOffset = j * 2 * (float) Math.PI / orbitalPlanes;
//
//				newOrbit.Init(
//					newSats,
//					newOrbitalSphere, 
//					worldEnvironment,
//					longditudonalOffset
//				);
//
//				orbits[j] = newOrbit;
//				newOrbitalSphere.AddChild(newOrbit);
//			}
//
//			newOrbitalSphere.Init(
//				orbits, 
//				orbitalPlanes, 
//				distanceAboveCore,
//				inclination,
//				sattelitesPerPlane,
//				phaseOffset,
//				linkingMethod,
//				newConstellation,
//				worldEnvironment
//			);
//
//			orbitalSpheresNew[i] = newOrbitalSphere;
//			newConstellation.AddChild(newOrbitalSphere);
//		}
//
//		newConstellation.Init(
//			orbitalSpheresNew, 
//			numOfSpheres,
//			worldEnvironment
//		);
//
//		return newConstellation;
//	}
}
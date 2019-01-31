using Godot;
using System;

public class Constellations
{
//	TODO  phase offsets not working

	public const Constellation ORBIT = CreateFromArrays
	(
		/*orbitalPlaness*/ new int[] {1}, 
		/*sattelitesPerPlanes*/ new int[] {50},
		/*altitudes*/ new int[] {1150},
		/*inclinations*/ new float[] {53},
		/*phaseOffsets*/ new float[] {0},
		/*numOfSpheres*/ 1w
	);

	public const Constellation SPHERE = CreateFromArrays
	(
		/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
		/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
		/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
		/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
		/*phaseOffsets*/ new float[] {0, 0, 0, 0, 0},
		/*numOfSpheres*/ 5
	);

	public const Constellation STARLINK = CreateFromArrays
	(
		/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
		/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
		/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
		/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
		/*phaseOffsets*/ new float[] {0, 0, 0, 0, 0},
		/*numOfSpheres*/ 5
	);

	public const Constellation TWO_ORIBTIS = CreateFromArrays
	(
		/*orbitalPlaness*/ new int[] {2}, 
		/*sattelitesPerPlanes*/ new int[] {8},
		/*altitudes*/ new int[] {1150},
		/*inclinations*/ new float[] {80},
		/*phaseOffsets*/ new float[] {0},
		/*numOfSpheres*/ 1	
	);
}

public class Main : Spatial
{
	WorldEnvironment worldEnvironment;
	
	Satellite[][][] allSatellites;

    public override void _Ready()
    {		
        worldEnvironment = (WorldEnvironment) FindNode("WorldEnvironment");
		
		AddChild(newConstellation);
    }

	// creates a constellation from the inputs
	// orbital planes[], sattelites per plane[], altitude[], inlination[], numOfSphere
	public Constellation CreateFromArrays(
		int[] orbitalPlaness, 
		int[] sattelitesPerPlanes,
		int[] altitudes,
		float[] inclinations,
		float[] phaseOffsets,
		int numOfSpheres
	) {
			
		var constellationScene = ResourceLoader.Load("res://Scenes//Constellation.tscn") as PackedScene;
		var orbitalSphereScene = ResourceLoader.Load("res://Scenes//OrbitalSphere.tscn") as PackedScene;
		var orbitScene = ResourceLoader.Load("res://Scenes//Orbit.tscn") as PackedScene;
		var satteliteScene = ResourceLoader.Load("res://Scenes//Satellite.tscn") as PackedScene;
		
		Constellation newConstellation = constellationScene.Instance() as Constellation;
		
		
		
		OrbitalSphere[] orbitalSpheresNew = new OrbitalSphere[numOfSpheres];
		
		for (int i = 0; i < numOfSpheres; i++) 
		{
			OrbitalSphere newOrbitalSphere = orbitalSphereScene.Instance() as OrbitalSphere;
			
			int orbitalPlanes = orbitalPlaness[i];
			int sattelitesPerPlane = sattelitesPerPlanes[i];
			float distanceAboveCore = altitudes[i] / 1000 + worldEnvironment.sizeOfEarth;
			float inclination = inclinations[i] * (float) Math.PI * 2 / 360;
			float phaseOffset = phaseOffsets[i];
			
			Orbit[] orbits = new Orbit[orbitalPlanes];
			
			for (int j = 0; j < orbitalPlanes; j++) 
			{
				Orbit newOrbit = orbitScene.Instance() as Orbit;
				
				Satellite[] newSats = new Satellite[sattelitesPerPlane];
				
				for (int k = 0; k < sattelitesPerPlane; k++) 
				{
					Satellite newSat = satteliteScene.Instance() as Satellite;
					
					newSat.Init(i, j, k, newOrbit);
					
					newSats[k] = newSat;
					newOrbit.AddChild(newSat);
				}
				
				float longditudonalOffset = j * 2 * (float) Math.PI / orbitalPlanes;
				
				newOrbit.Init(
					newSats,
					newOrbitalSphere, 
					worldEnvironment,
					longditudonalOffset
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
				5, //TODO: UPDATE THIS
				newConstellation,
				worldEnvironment
			);
			
			orbitalSpheresNew[i] = newOrbitalSphere;
			newConstellation.AddChild(newOrbitalSphere);
		}
		
		newConstellation.Init(
			orbitalSpheresNew, 
			numOfSpheres,
			worldEnvironment
		);
		
		return newConstellation;
	}
}

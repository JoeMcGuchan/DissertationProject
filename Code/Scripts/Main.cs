using Godot;
using System;

public class Constellations
{
	//	TODO  phase offsets not working

	public int[][] updownleftright = new int[][] {
		new int[] {1,0},
		new int[] {-1,0},
		new int[] {0,1},
		new int[] {0,-1}
	}

	public ConstellationDesciption orbitDesc = ConstellationDescription
	(
		/*orbitalPlaness*/ new int[] {1}, 
		/*sattelitesPerPlanes*/ new int[] {50},
		/*altitudes*/ new int[] {1150},
		/*inclinations*/ new float[] {53},
		/*phaseOffsets*/ new float[] {0},
		/*linkingMethods*/ new LinkingMethod[] {new FourFixedOneFree()},
		/*numOfSpheres*/ 1
	);

	public ConstellationDesciption sphereDesc = ConstellationDescription
	(
		/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
		/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
		/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
		/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
		/*phaseOffsets*/ new float[] {0, 0, 0, 0, 0},
		/*linkingMethods*/ new LinkingMethod[] 
		{
			new FourFixedOneFree(),
			new FourFixedOneFree(),
			new FourFixedOneFree(),
			new FourFixedOneFree(),
			new FourFixedOneFree()
		},
		/*numOfSpheres*/ 5
	);

	public ConstellationDesciption starlinkDesc = ConstellationDescription
	(
		/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
		/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
		/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
		/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
		/*phaseOffsets*/ new float[] {0, 0, 0, 0, 0},
		/*linkingMethods*/ new LinkingMethod[] {new FourFixedOneFree()},
		/*numOfSpheres*/ 5
	);

	public ConstellationDesciption twoOrbitsDesc = new ConstellationDescription
	(
		/*orbitalPlaness*/ new int[] {2}, 
		/*sattelitesPerPlanes*/ new int[] {8},
		/*altitudes*/ new int[] {1150},
		/*inclinations*/ new float[] {80},
		/*phaseOffsets*/ new float[] {0},
		/*linkingMethods*/ new LinkingMethod[] {new FourFixedOneFree()},
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
		
		AddChild(Constellations.ConstellationDesciption.create(worldEnvironment));
    }

}

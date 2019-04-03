using Godot;
using System;

public class ConstellationDescriptionDatabase
{
	//	TODO  phase offsets not working
	ConstellationDescription[] constellationDescriptions;

	public ConstellationDescriptionDatabase() {
		int[][] updownleftright = new int[][] {
			new int[] {0,1},
			new int[] {1,0},
			new int[] {0,-1},
			new int[] {1,0}
		};

		LinkingMethod simpleLinking = new NoLinks();
	
		LinkingMethod noLinking = new NoLinks();
		LinkingMethod fourFixed = new NoLinks();
		LinkingMethod justOrbits = new NoLinks();

		constellationDescriptions = new ConstellationDescription[]
		{
			/*SINGLE ORBIT*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {1}, 
				/*sattelitesPerPlanes*/ new int[] {50},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {0},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethods*/ new LinkingMethod[] {simpleLinking},
				/*numOfSpheres*/ 1
			),
			/*STARLINK*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
				/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
				/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
				/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
				/*phaseOffsets*/ new int[] {0, 0, 0, 0, 0},
				/*timeOffset*/ new float[] {0f, 0f, 0f, 0f, 0f},
				/*linkingMethods*/ new LinkingMethod[] 
				{
					noLinking,
					noLinking,
					noLinking,
					noLinking,
					noLinking
				},
				/*numOfSpheres*/ 5
			),
			/*BROKEN ORBITS*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {30},
				/*sattelitesPerPlanes*/ new int[] {31},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {70},
				/*phaseOffsets*/ new int[] {15},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethods*/ new LinkingMethod[] {fourFixed},
				/*numOfSpheres*/ 1
			),
			/*CLOSEST LINKS AT WORK*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {48}, 
				/*sattelitesPerPlanes*/ new int[] {50},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {60},
				/*phaseOffsets*/ new int[] {0},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethods*/ new LinkingMethod[] {new NoLinks()},
				/*numOfSpheres*/ 1
			),
			/*GEOSTATIONARY VS SPHERE*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {1, 32}, 
				/*sattelitesPerPlanes*/ new int[] {100, 50},
				/*altitudes*/ new int[] {42164, 1150},
				/*inclinations*/ new float[] {0, 53},
				/*phaseOffsets*/ new int[] {0, 0},
				/*timeOffset*/ new float[] {0f, 0f},
				/*linkingMethods*/ new LinkingMethod[] {noLinking, noLinking},
				/*numOfSpheres*/ 2
			),
			/*FIRST SPHERE*/
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {32}, 
				/*sattelitesPerPlanes*/ new int[] {50},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethods*/ new LinkingMethod[] {simpleLinking},
				/*numOfSpheres*/ 1
			),
		};
	}

	public ConstellationDescription GetConstellation(int i) {
		return constellationDescriptions[i];
	}
}
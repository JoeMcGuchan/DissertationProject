using Godot;
using System;

public class ConstellationDescriptionDatabase
{
	//	TODO  phase offsets not working
	ConstellationDescription[] constellationDescriptions;

	public ConstellationDescriptionDatabase() {
		int[][] updownleftright = new int[][] {
			new int[] {1,0},
			new int[] {-1,0},
			new int[] {0,1},
			new int[] {0,-1}
		};

		LinkingMethod simpleLinking = new FourFixedOneFree(updownleftright);

		constellationDescriptions = new ConstellationDescription[]
		{
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {1}, 
				/*sattelitesPerPlanes*/ new int[] {50},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new float[] {0},
				/*linkingMethods*/ new LinkingMethod[] {simpleLinking},
				/*numOfSpheres*/ 1
			),
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {32, 32, 8, 5, 6}, 
				/*sattelitesPerPlanes*/ new int[] {50, 50, 50, 75, 75},
				/*altitudes*/ new int[] {1150, 1110, 1130, 1275, 1325},
				/*inclinations*/ new float[] {53, 53.8f, 74, 80, 80},
				/*phaseOffsets*/ new float[] {0, 0, 0, 0, 0},
				/*linkingMethods*/ new LinkingMethod[] 
				{
					simpleLinking,
					simpleLinking,
					simpleLinking,
					simpleLinking,
					simpleLinking
				},
				/*numOfSpheres*/ 5
			),
			new ConstellationDescription
			(
				/*orbitalPlaness*/ new int[] {2}, 
				/*sattelitesPerPlanes*/ new int[] {8},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {80},
				/*phaseOffsets*/ new float[] {0},
				/*linkingMethods*/ new LinkingMethod[] {simpleLinking},
				/*numOfSpheres*/ 1	
			)
		};
	}

	public ConstellationDescription GetConstellation(int i) {
		return constellationDescriptions[i];
	}
}
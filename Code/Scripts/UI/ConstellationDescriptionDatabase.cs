using Godot;
using System;

public class ConstellationDescriptionDatabase
{
	//	TODO  phase offsets not working
	ConstellationDescription[] constellationDescriptions;

	public ConstellationDescriptionDatabase() {
		int[][] updownleftright = new int[][] {
			new int[] {1,-1},
			new int[] {1,0},
		};

		LinkingMethod noLinking = new NoLinks();
		
		LinkingMethod simpleLinking = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {updownleftright}
		);
		
		ColouringMethod highlightMarked = new HighlightMarked();

		constellationDescriptions = new ConstellationDescription[]
		{
			/*FIRST SPHERE*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {32}, 
				/*sattelitesPerPlanes*/ new int[] {50},
				/*altitudes*/ new int[] {1150},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked
			),
		};
	}

	public ConstellationDescription GetConstellation(int i) {
		return constellationDescriptions[i];
	}
}
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
		
		int[][] alternateDir = new int[][] {
			new int[] {0,-1},
			new int[] {1,0},
		};

		LinkingMethod noLinking = new NoLinks();
		
		LinkingMethod simpleLinking = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {updownleftright}
		);
		
		LinkingMethod alternateLinking = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {alternateDir}
		);
		
		ColouringMethod highlightMarked = new HighlightMarked();

		constellationDescriptions = new ConstellationDescription[]
		{
			/*NO TEST*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new NoTest()
			),
			/*FOLLOWING SATELLITE*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new NearbySats("TestResults/NearbySats.csv")
			),
			/*CONNECTED COMPONENTS*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponents.csv",
					24,
					100,
					1,
					1,
					66)
			),
			/*CONNECTED COMPONENTS TWO*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ alternateLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponentsVariant.csv",
					24,
					100,
					1,
					1,
					66)
			),
			/*CONNECTED COMPONENTS LOCALISED*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponentsLocalised.csv",
					24,
					100,
					1,
					1,
					22)
			),
			/*SHORTEST PATH LONDON NEW YORK*/
			new ConstellationDescription
			(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f},
				/*linkingMethod*/ simpleLinking,
				/*colouringMathod*/ highlightMarked,
				/*test*/ new RepeatedShortestPath(
					"TestResults/ShortestPathDeletions.csv",
					/*num of experiments*/3,
					/*num of repeats each experiment*/3,
					/*num of samples each repetition*/1,
					/*time between samples*/2,
					/*num to delete*/500,
					/*lng1*/ -51.5f,
					/*lat1*/ 0.12f,
					/*lng2*/ -40.7f,
					/*lat2*/ -74.0f
				)
			)
			
		};
	}

	public ConstellationDescription GetConstellation(int i) {
		return constellationDescriptions[i];
	}
}
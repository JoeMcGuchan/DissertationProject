using Godot;
using System;

public class ConstellationDescriptionDatabase
{
	//	TODO  phase offsets not working
	ConstellationDescription[] constellationDescriptions;

	public ConstellationDescriptionDatabase() {
		
		//Forward offset, Side Offset
		int[][] updownleftright = new int[][] {
			new int[] {1,-1},
			new int[] {1,0},
		};
		
		int[][] alternateDir = new int[][] {
			new int[] {0,-1},
			new int[] {1,0},
		};
		
		int[][] alternateDir3 = new int[][] {
			new int[] {2,-1},
			new int[] {1,2}
		};
		
		int[][] onlyForwardDir = new int[][] {
			new int[] {1,0}
		};

		LinkingMethod noLinking = new NoLinks();
		
		LinkingMethod linkingMethod1 = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {updownleftright}
		);
		
		LinkingMethod linkingMethod2 = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {alternateDir}
		);
		
		LinkingMethod linkingMethod3 = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {2},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {alternateDir3}
		);
		
		LinkingMethod onlyForward = new SomeFixedSomeFree(
			/*int[] numOfFreeLinks*/ new int[] {0},
			/*int[] numOfFixedLinksHalved*/ new int[] {1},
			/*int[][][] fixedLinkOffsets*/ new int[][][] {onlyForwardDir}
		);
		
		ColouringMethod highlightMarked = new HighlightMarked();
		ColouringMethod orbitGradient24 = new OrbitGradient(24);
		
		ConstellationDescriptionTemplate starLink = new ConstellationDescriptionTemplate(
				1,
				/*orbitalPlaness*/ new int[] {24}, 
				/*sattelitesPerPlanes*/ new int[] {66},
				/*altitudes*/ new int[] {550},
				/*inclinations*/ new float[] {53},
				/*phaseOffsets*/ new int[] {9},
				/*timeOffset*/ new float[] {0f}
		);

		constellationDescriptions = new ConstellationDescription[]
		{
			/*ORBIT GRADIENT*/
			new ConstellationDescription(starLink, onlyForward, orbitGradient24, new NoTest()),
			/*FOLLOWING SATELLITE*/
			new ConstellationDescription(starLink, onlyForward, highlightMarked, new NearbySats("TestResults/NearbySats.csv")),
			/*LM1*/
			new ConstellationDescription(starLink, linkingMethod1, highlightMarked, new NoTest()),
			/*LM2*/
			new ConstellationDescription(starLink, linkingMethod2, highlightMarked, new NoTest()),
			/*LM3*/
			new ConstellationDescription(starLink, linkingMethod3, highlightMarked, new NoTest()),
			/*CONNECTED COMPONENTS LM1*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod1,
				highlightMarked,
				new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponentsLM1.csv",
					25,
					100,
					1,
					0,
					66
				)
			),
			/*CONNECTED COMPONENTS LM2*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod2,
				highlightMarked,
				new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponentsLM2.csv",
					25,
					100,
					1,
					0,
					66
				)
			),
			/*CONNECTED COMPONENTS LM3*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod3,
				highlightMarked,
				new ConnectedComponentsRemovingSatellites(
					"TestResults/ConnestedComponentsLM3.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 100,
					/*num of samples each repetition*/ 1,
					/*time between samples*/ 0,
					/*num to delete*/ 66
				)
			),
			/*SHORTEST PATH LONDON NEW YORK 1*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod1,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToNewYork1.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH LONDON NEW YORK 2*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod2,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToNewYork2.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH LONDON NEW YORK 3*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod3,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToNewYork3.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH BEIJING NEW YORK 1*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod1,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/BeijingToNewYork1.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 116.4f,
					/*lat1*/ 39.9f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH BEIJING NEW YORK 2*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod2,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/BeijingToNewYork2.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 116.4f,
					/*lat1*/ 39.9f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH BEIJING NEW YORK 3*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod3,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/BeijingToNewYork3.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 116.4f,
					/*lat1*/ 39.9f,
					/*lng2*/ -74f,
					/*lat2*/ 40.7f
				)
			),
			/*SHORTEST PATH LONDON JOHANNASBURG 1*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod1,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToJohannesburg1.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ 28f,
					/*lat2*/ -26.2f
				)
			),
			/*SHORTEST PATH LONDON JOHANNASBURG 2*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod2,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToJohannesburg2.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ 28f,
					/*lat2*/ -26.2f
				)
			),
			/*SHORTEST PATH LONDON JOHANNASBURG 3*/
			new ConstellationDescription
			(
				starLink,
				linkingMethod3,
				highlightMarked,
				new RepeatedShortestPath(
					"TestResults/LondonToJohannesburg3.csv",
					/*num of experiments*/ 25,
					/*num of repeats each experiment*/ 5,
					/*num of samples each repetition*/ 60,
					/*time between samples*/ 0,
					/*num to delete*/ 66,
					/*lng1*/ 0.13f,
					/*lat1*/ 51.5f,
					/*lng2*/ 28f,
					/*lat2*/ -26.2f
				)
			),
			/*NOTHING*/
			new ConstellationDescription(starLink, noLinking, highlightMarked, new QuitTheGame()),
		};
	}

	public ConstellationDescription GetConstellation(int i) {
		return constellationDescriptions[i];
	}
}
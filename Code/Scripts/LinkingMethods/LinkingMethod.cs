using Godot;
using System;

public abstract class LinkingMethod : Node
{
    // This abstract class describes the way that a sattelite forms
	// links with it's neighbors
	

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
	
	//the priniple of this is thus, when finding the closest sattelite
	//I don't have to search every sat on the sphere, as "closeness" is
	//a smooth function with a single peak wrt the sphere
	public float findNearestSatteliteInSphere(
		Sattelite[][] sphere, 
		Vector3 pos, 
		Sattelite* returnSat, 
		int[] startingPoint
	) {
		int j = startingPoint[0];
		int k = startingPoint[1];
		
		Vector3 testPos = sattelite[j][k].Translation;
		
	}
	
	public int 
}

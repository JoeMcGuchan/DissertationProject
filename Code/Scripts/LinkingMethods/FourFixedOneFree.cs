using Godot;
using System;

public class FourFixedOneFree : LinkingMethod
{
	// for integer vectors giving the relative positions of the links
	//[[x, y],...]
	int[][] fixedLinks;
	
	public LinkingMethod(int[][] fixedLinksNew) {
		fixedLinks = fixedLinksNew;
	}
	
	public override 
}

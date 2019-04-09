using Godot;
using System;

public class Link : Spatial
{
	public bool Active;
	public ImmediateGeometry Line;
	//link is the child of Sat1
	
	public Vertex V1;
	public Vertex V2;
	public float Dist;
	
	//used for algoritms
	public bool Marked = false;
	
	public void Init(Vertex v1, Vertex v2)
	{
		V1 = v1;
		V2 = v2;
		
		V1.AddLink(this);
		V1.AddChild(this);
		
		V2.AddLink(this);
		
		Active = false;
		Dist = V1.Translation.DistanceTo(V2.Translation);
		
		Line = new ImmediateGeometry();
		
		AddChild(Line);
		
		Update();
	}
	
	public void Update()
	{
		Dist = V1.Translation.DistanceTo(V2.Translation);
		Draw();
	}
	
	//draw line to sat2
	public void Draw()
	{
		Line.Clear();
		Line.Begin(Mesh.PrimitiveType.Lines);
		Line.AddVertex(new Vector3(0,0,0));
		Line.AddVertex(ToLocal(V2.Translation));
		Line.End();
	}
}

using Godot;
using System;

//Simple class to describe a path through the network

public class VertexPath
{
    public Vertex[] Verticies;
	public Link[] Links;

	float Dist;
	bool DistSet;

	public VertexPath(Vertex[] vs, Link[] ls)
	{
		Verticies = vs;
		Links = ls;

		DistSet = false;
	}

	//option to declare with Distance
	public VertexPath(Vertex[] vs, Link[] ls, float d)
	{
		Verticies = vs;
		Links = ls;

		DistSet = true;
		Dist = d;

		if (!IsValid())
		{
			throw new ArgumentException("Verticies don't match line");
		}
	}

	public bool IsValid()
	{
		if (Verticies.Length != Links.Length + 1) {return false;}

		bool valid = true;

		for (int i = 0; i < Links.Length; i++)
		{
			//check that the Verticies on either end of the link match the Vertexes on the list

			Vertex v1 = Links[i].V1;
			Vertex v2 = Links[i].V2;
			Vertex u1 = Verticies[i];
			Vertex u2 = Verticies[i+1];

			valid = valid && ((v1 == u1 && v2 == u2) || (v1 == u2 && v2 == u1));
		}

		return valid;
	}

	public void SetMarkedAll(bool b)
	{
		foreach (Vertex v in Verticies)
		{
			v.Marked = b;
		}

		foreach (Link l in Links)
		{
			l.Marked = b;
		}
	}

	public float GetDist()
	{
		if (DistSet) {return Dist;}
		else
		{
			Dist = 0f;

			foreach (Link l in Links)
			{
				Dist += l.Dist;
			}

			DistSet = true;
			return Dist;
		}
	}

	public int NumVerts()
	{
		return Verticies.Length;
	}

	public Vertex GetVertex(int i)
	{
		return Verticies[i];
	}
	
	public VertexPath SubPath(int i, int length)
	{
		//Vertex[] newVerts = new Vertex[length];
		//Link[] newLinks = new Links[length-1];
		
		//Array.ConstrainedCopy(Verticies,i,newVerts,0,length);
		//Array.ConstrainedCopy(Links,i,NewVerts,0,length-1);
		
		//return new VertexPath(newVerts, newLinks);
		return null;
	}
}

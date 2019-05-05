using Godot;
using System;
using System.Collections.Generic;

public abstract class Test
{
	public System.IO.StreamWriter WriteOut;
	public int RunNumber;
	
	public bool RunContinually;
	
	public Constellation TargetConstellation;
	
	public void CreateFile(String path)
	{
		WriteOut = new System.IO.StreamWriter(path, true);
		RunNumber = 1;
	}
	
	public void WriteLine(String str)
	{
		WriteOut.WriteLine(str);
		WriteOut.AutoFlush = true;
		Console.WriteLine(str);
	}
	
	public void Close()
	{
		WriteOut.Close();
	}
	
	public abstract void Run();
	
	public abstract void Init(Constellation c);
	
	protected int mod(int x, int m) {
		return (x%m + m)%m;
	}	
}

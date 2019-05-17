using Godot;
using System;

public class QuitTheGame : Test
{
	Main M;
	
	public QuitTheGame()
	{
		RunContinually = true;
	}
	
	public override void Run() 
	{
		M.GetTree().Quit();
	}
	
	public override void Init(Constellation c) {
		M = (Main) c.GetParent();
	}
}

using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	private static int level;

	public static int Level {
		get { return level; } 
		private set { level = value; }
	}

	public void StartGame (int level)
	{
		Level = level;
		Application.LoadLevel ("stage");	
	}
}

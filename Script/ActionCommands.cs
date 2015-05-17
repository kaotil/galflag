using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionCommands : MonoBehaviour
{

	[SerializeField]
	private string
		title = "title";
	private string color;
	private int command;
	private string text;
	private string[] colors = {"white", "red"};
	private int[] commands = {1, 2, 3, 4}; // 1: up, 2: not up, 3: down, 4: not down
//	private string[] white_flag_texts = new string[10];
//	private string[] red_flag_texts = new string[10];
	private System.Random rand = new System.Random ();
	private Dictionary<string, int> before_commands = new Dictionary<string, int> ();

	public string Title {
		get { return title; } 
		private set { title = value; }
	}

	public string Color {
		get { return color; } 
		private set { color = value; }
	}

	public int Command {
		get { return command; } 
		private set { command = value; }
	}

	public string Text {
		get { return text; } 
		private set { text = value; }
	}

	void Awake ()
	{
		before_commands ["white"] = 3;
		before_commands ["red"] = 3;
	}

	public void CreateCommand ()
	{
		Color = colors [rand.Next (0, 2)];
		Command = getCommand ();
		Text = getText ();
	}

	private int getCommand ()
	{
		switch (before_commands [Color]) {
		// now is up
		case (1):
		case (4):
			// next is down or not down
			command = commands [rand.Next (2, 4)];
			break;
		// now is down
		case (2):
		case (3):
			// next is up or not up
			command = commands [rand.Next (0, 2)];
			break;
		}
		before_commands [Color] = Command;
		return command;
	}

	private string getText ()
	{
		string str = "";

		switch (Command) {
		// up
		case (1):
			str = "上げて  ";
			break;
		// not up
		case (2):
			str = "上げないで";
			break;
		// down
		case (3):
			str = "下げて  ";
			break;
		// not down
		case (4):
			str = "下げないで";
			break;
		}

		return ((Color == "white") ? "白" : "赤") + str;
	}
}
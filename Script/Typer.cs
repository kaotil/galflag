using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class Typer : MonoBehaviour {
	
	private string text = "Replace";
	private Text textComp;
	public float startDelay = 2f;
	public float typeDelay = 0.01f;
	private bool finished = false;
	private string cmdText;

	public bool Finished {
		get { return finished; } 
		private set { finished = value; }
	}

	public string CmdText {
		get { return cmdText; } 
		set { cmdText = value; }
	}

	// Use this for initialization
	void Start () {
		textComp = GameObject.Find("CommandText").GetComponent<Text>();
		textComp = GetComponent<Text>();
//		StartCoroutine("TypeIn");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
//	public void StartTyping()
	public void StartTyping()
	{
Debug.Log ("StartTyping Start");
		StartCoroutine("TypeIn");
Debug.Log ("StartTyping End");
	}


	public IEnumerator TypeIn()
	{
Debug.Log ("TypeIn Start");
		float time = 0;
		while (time < startDelay)
		{
			time += Time.fixedDeltaTime;
			yield return new WaitForEndOfFrame();
		}
		
		for(int i = 1; i <= CmdText.Length; ++i)
		{
Debug.Log (i + " / " + text.Substring (0, i));
			textComp.text = CmdText.Substring (0, i);
			time = 0;
			while (time < typeDelay)
			{
				time += Time.fixedDeltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
		Finished = true;
Debug.Log ("TypeIn End");
	}
	
	public IEnumerator TypeOff()
	{
		for (int i = text.Length; i >= 0; --i)
		{
			textComp.text = text.Substring (0, i);
			yield return new WaitForSeconds(typeDelay);
		}
	}


}
/*
[RequireComponent(typeof(Text))]
public class Typer : MonoBehaviour {
	public int score = 0;

	private string text = "abc";
	private Text textComp;
	private float startDelay = 2f;
	private float typeDelay = 0.5f;
//	private string[] messages = new string[5];
//	private static Dictionary<string, string> messages = new Dictionary<string, string>();
	private ActionCommands ac;
	private FlagAction fa;

	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text>();
		ac = GameObject.Find("ActionCommands").GetComponent<ActionCommands>();
		fa = GameObject.Find("FlagAction").GetComponent<FlagAction>();
		score = PlayerPrefs.GetInt("score");

//		StartCoroutine("TypeIn2");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void StartTyping()
	{
Debug.Log ("StartTyping");

		StartCoroutine("TypeIn2");
	}

	public IEnumerator TypeIn2()
	{
		textComp.text = "TypeIn2";
		yield return null;
	}

	public IEnumerator TypeIn()
	{
		float time = 0;
		while (time < startDelay)
		{
			time += Time.fixedDeltaTime;
			yield return new WaitForEndOfFrame();
		}

		bool flag_check = true;
int cnt = 0;

		while (flag_check) 
		{
			// 動作指定
			ac.CreateCommand();

			// １文字づつ表示
			for (int i = 1; i < ac.Text.Length+1; ++i)
			{
//Debug.Log (ac.Text.Substring (0, i));
				textComp.text = ac.Text.Substring (0, i);
				time = 0;
				while (time < typeDelay)
				{
					time += Time.fixedDeltaTime;
					yield return new WaitForEndOfFrame();
				}
			}
			flag_check = fa.IsEnableFlag(ac.Color, ac.Command);
         	if (!flag_check) {
				textComp.text = "out!!!";
			} else {
				score += 10;
				PlayerPrefs.SetInt("score", score);
			}
//			if (cnt == 4) ok = false;
// 最大値になったらクリア
			++cnt;
		}
	}
}
*/
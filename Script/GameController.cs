using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public int score = 0;
	private Typer typerComp;
	private ActionCommands actComp;
	private FlagAction flagComp;

	// Use this for initialization
	void Start () {
		typerComp = GameObject.Find("Typer").GetComponent<Typer>();
		actComp = GameObject.Find("ActionCommands").GetComponent<ActionCommands>();
		flagComp = GameObject.Find("FlagAction").GetComponent<FlagAction>();
		score = PlayerPrefs.GetInt("score");

//		PlayGame ();

//		StartCoroutine("Coroutine1");
//		Coroutine3 ();
	}


	void PlayGame () {
Debug.Log ("PlayGame.");
		bool flag_check = true;

int cnt = 0;
		while (flag_check) {
			actComp.CreateCommand();
			typerComp.CmdText = actComp.Text;
			typerComp.StartTyping ();

//			if (typerComp.Finished) {
				flag_check = flagComp.IsEnableFlag(actComp.Color, actComp.Command);
				if (!flag_check) {
				//textComp.text = "out!!!";
Debug.Log ("Out!!!!!");
				} else {
					score += 10;
					PlayerPrefs.SetInt("score", score);
				}
//			}
			if (cnt > 2) {
//Debug.Log ("flag_check: false");
				flag_check = false;
			} else {
//Debug.Log ("flag_check: true");
				flag_check = true;
			}
			cnt++;
		}
// 最大値になったらクリア
	}

	void Update () {
	}

	private void Coroutine3() {
		Debug.Log("START-----");
		StartCoroutine("Coroutine1");
		Debug.Log("END-------");
	}

	public IEnumerator Coroutine1()
	{
		Debug.Log("Start-1");
		Debug.Log("--------");
		yield return StartCoroutine("Coroutine2");
		Debug.Log("--------");
		Debug.Log("Start-2");
		yield return null;
		Debug.Log("Start-3");
	}
	
	private IEnumerator Coroutine2()
	{
		Debug.Log("Nest-1");
		yield return null;
		
		Debug.Log("Nest-2");
		yield return null;
		
		Debug.Log("Nest-3");
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
	private ActionCommands actComp;
	private FlagAction flagComp;
	private Text textComp;
	private float startDelay = 2f;
	private float typeDelay;
	private int score = 0;
	private int level;
	public GUIStyle guiStyle;
	public GUIStyleState guiStyleState;

	// Use this for initialization
	void Start ()
	{
		guiStyle = new GUIStyle ();
		guiStyleState = new GUIStyleState ();

		actComp = GameObject.Find ("ActionCommands").GetComponent<ActionCommands> ();
		flagComp = GameObject.Find ("FlagAction").GetComponent<FlagAction> ();
		textComp = GameObject.Find ("CommandText").GetComponent<Text> ();

		setInit ();
		StartCoroutine ("play");
	}
	
	private IEnumerator play ()
	{
		float time = 0;
		while (time < startDelay) {
			time += Time.fixedDeltaTime;
			yield return new WaitForEndOfFrame ();
		}
		
		bool flag_check = true;
		int cnt = 0;
		
		while (flag_check) {
			// 動作指定
			actComp.CreateCommand ();
			
			// １文字づつ表示
			for (int i = 1; i < actComp.Text.Length+1; ++i) {
				//Debug.Log (ac.Text.Substring (0, i));
				textComp.text = actComp.Text.Substring (0, i);
				time = 0;
				while (time < typeDelay) {
					time += Time.fixedDeltaTime;
					yield return new WaitForEndOfFrame ();
				}
			}
			flag_check = flagComp.IsEnableFlag (actComp.Color, actComp.Command);
			if (!flag_check) {
				textComp.text = "out!!!";
				if (score > PlayerPrefs.GetInt ("highScore_lv" + level)) {
					PlayerPrefs.SetInt ("highScore_lv" + level, score);
				}
			} else {
				score += 10;
			}
			//			if (cnt == 4) ok = false;
			// 最大値になったらクリア
			++cnt;
		}
	}

	private void setInit ()
	{
		guiStyle.fontSize = 20;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyleState.textColor = Color.red;
		guiStyleState.background = Texture2D.whiteTexture;
		guiStyle.normal = guiStyleState;

		level = MenuController.Level;
		score = 0;

		switch (level) {
		// easy
		case 1:
			typeDelay = 1f;
			break;
		// normal
		case 2:
			typeDelay = 0.5f;
			break;
		// hard
		case 3:
			typeDelay = 0.1f;
			break;
		default:
			typeDelay = 0.5f;
			level = 2;
			break;
		}
	}

	void OnGUI ()
	{
		if (GUILayout.Button ("PlayerPrefs Delete!!!")) {
			PlayerPrefs.DeleteKey ("highScore_lv" + level);
			PlayerPrefs.DeleteKey ("score");
			PlayerPrefs.DeleteAll ();
		}
		GUILayout.Label ("Score: " + score, guiStyle);
		GUILayout.Label ("highScore: " + PlayerPrefs.GetInt ("highScore_lv" + level), guiStyle);
	}
}

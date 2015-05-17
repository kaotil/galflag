using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlagAction : MonoBehaviour
{
	public Dictionary<string, GameObject> WhiteFlags = new Dictionary<string, GameObject> ();
	public Dictionary<string, GameObject> RedFlags = new Dictionary<string, GameObject> ();

	// Use this for initialization
	void Start ()
	{
		WhiteFlags ["top"] = GameObject.Find ("white_top");
		WhiteFlags ["mdl"] = GameObject.Find ("white_middle");
		WhiteFlags ["btm"] = GameObject.Find ("white_bottom");
		RedFlags ["top"] = GameObject.Find ("red_top");
		RedFlags ["mdl"] = GameObject.Find ("red_middle");
		RedFlags ["btm"] = GameObject.Find ("red_bottom");

		// 旗初期表示
		enableWhiteFlag ("btm");
		enableRedFlag ("btm");
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Vector3 aTapPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Collider2D aCollider2d = Physics2D.OverlapPoint (aTapPoint);
			
			if (aCollider2d) {
				GameObject obj = aCollider2d.transform.gameObject;
				switch (obj.name) {
				case "area_white_top":
					enableWhiteFlag ("top");
					break;
				case "area_white_bottom":
					enableWhiteFlag ("btm");
					break;
				case "area_red_top":
					enableRedFlag ("top");
					break;
				case "area_red_bottom":
					enableRedFlag ("btm");
					break;
				default:
					break;
				}
			}
		}
// 命令と違う動きだったらアウト！
	}

	// 白旗表示画像変更
	void enableWhiteFlag (string type)
	{
		foreach (KeyValuePair<string,GameObject> flag in WhiteFlags) {
			WhiteFlags [flag.Key].GetComponent<Renderer> ().enabled = (type == flag.Key);
		}
	}

	// 赤旗表示画像変更
	void enableRedFlag (string type)
	{
		foreach (KeyValuePair<string,GameObject> flag in RedFlags) {
			RedFlags [flag.Key].GetComponent<Renderer> ().enabled = (type == flag.Key);
		}
	}

	// 旗判定
	public bool IsEnableFlag (string color, int command)
	{
		if (color == "white") {
			return WhiteFlags [setType (command)].GetComponent<Renderer> ().enabled;
		} else {
			return RedFlags [setType (command)].GetComponent<Renderer> ().enabled;
		}
	}

	// 命令の動作ごとに判別する旗の位置
	private string setType (int command)
	{
		string type = "";

		switch (command) {
		// up
		case (1):
		// not down
		case (4):
			type = "top";
			break;
		// not up
		case (2):
		// down
		case (3):
			type = "btm";
			break;
		}
		return type;
	}
}
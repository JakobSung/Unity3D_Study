using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DispScore(0);
	
	}

	private int _score;
	
	public Text ScoreText;

    public void DispScore(int v)
    {
        _score += v;
		ScoreText.text = "score <color=#ff0000>" + _score.ToString() + "</color>";
    }


    // Update is called once per frame
    void Update () {
	
	}
}

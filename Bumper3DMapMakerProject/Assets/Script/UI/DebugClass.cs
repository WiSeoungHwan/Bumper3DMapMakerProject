using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void ResetStageNumRestart(){
        PlayerPrefs.SetInt("StageLevel", 1);
        GameManager.Instance.stageNum = 0;
        GameManager.Instance.ReloadScene();
    }
	// Update is called once per frame
	void Update () {
		
	}
}

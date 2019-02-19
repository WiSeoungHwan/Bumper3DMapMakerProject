using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour {
    public UISprite buttonSprite;
    public GameObject[] buttons;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
	}
    public void SettingButtonDidTap(){
        if(GameManager.Instance.isUIOpen){
            for (int i = 0; i < buttons.Length; i++){
                buttons[i].SetActive(false);
            }
            GameManager.Instance.isUIOpen = false;
        }else{
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
            GameManager.Instance.isUIOpen = true;
        }
    }

	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.isUIOpen)
        {
            buttonSprite.spriteName = "Pause";
        }else{
            buttonSprite.spriteName = "Play";
        }

	}
}

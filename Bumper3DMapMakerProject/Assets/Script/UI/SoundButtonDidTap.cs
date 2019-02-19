using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonDidTap : MonoBehaviour {
    public UISprite buttonSprite;
    bool isSoundOff;

    private void Start()
    {
        PlayerPefsSetting();
    }

    public void SoundOnOffButtonDidTap()
    {
        if (isSoundOff)
        {
            isSoundOff = false;
            Debug.Log("SoundOn");
        }
        else
        {
            isSoundOff = true;
            Debug.Log("SoundOff");
        }
        SoundManager.Instance.OnOffSoundButton(isSoundOff);

    }
    void PlayerPefsSetting()
    {
        int onOffNum = 0; //0: false, 1: true;
        if (PlayerPrefs.HasKey("Sound"))
        {
            onOffNum = PlayerPrefs.GetInt("Sound");
            if (onOffNum == 0)
            {
                isSoundOff = true;
            }
            else if (onOffNum == 1)
            {
                isSoundOff = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1); // Default: On
            isSoundOff = false;
        }


    }
	// Update is called once per frame
	void Update () {
        if (isSoundOff)
        { 
            buttonSprite.spriteName = "SoundOff";
        }else{
            buttonSprite.spriteName = "SoundOn";
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {
    
    AudioSource itemSound;

    public void OnOffSoundButton(bool isSoundOff){
        if (!isSoundOff){// true: On, false: Off
            itemSound.volume = 1f;
            Debug.Log("soundOn");
        }else{
            itemSound.volume = 0f;
            Debug.Log("soundOff");
        }
    }
    public void ItemSoundPlay(){
        itemSound.Play();
    }
	// Use this for initialization
	void Start () {
        itemSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

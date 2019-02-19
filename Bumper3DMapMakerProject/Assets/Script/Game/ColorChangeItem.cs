using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeItem : MonoBehaviour {
    ParticleSystem particleSystem;
    MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
        particleSystem = GetComponent<ParticleSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")){
            meshRenderer.enabled = false;
            particleSystem.Play(true);
            SoundManager.Instance.ItemSoundPlay();
        }
    }
    // Update is called once per frame
    void FixedUpdate () {
        transform.Rotate(60 * Time.deltaTime, 60 * Time.deltaTime, 60 * Time.deltaTime);  
	}
}

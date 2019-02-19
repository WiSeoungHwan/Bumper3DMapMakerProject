using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    public float translateSpeed;
    public bool isGameOver;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GameManager.Instance.isEndPoint)
        {
            return;
        }
        if (!GameManager.Instance.isUIOpen){
            transform.Translate(0, 0, translateSpeed, Space.World);
        }
	}

}

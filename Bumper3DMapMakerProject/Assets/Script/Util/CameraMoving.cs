using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour {
    public float speed;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)){
            
            gameObject.transform.Translate(new Vector3(0, 0, 1f) * speed * Time.deltaTime, Space.World);
        }
        if(Input.GetKey(KeyCode.S)){
            gameObject.transform.Translate(new Vector3(0, 0, -1f) * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0){ // down
            gameObject.transform.Translate(new Vector3(0, -1f, 0) * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)// up
        {
            gameObject.transform.Translate(new Vector3(0, 1f, 0) * speed * Time.deltaTime, Space.World);

        }

	}
}

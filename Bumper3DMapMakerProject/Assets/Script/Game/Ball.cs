using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour {
    Vector3 beganPos;
    Rigidbody myRigidbody;
    Vector3 previousMovedPos;
    Vector3 direction = Vector3.zero;
    Vector3 velocity;
    bool isFirstTouch;
    bool isRightSide;
    bool isLeftSide;
    float speed = 50f;
    public float translateSpeed;
    public bool isColorChangeMode;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("GameOverObject")){
            if(!isColorChangeMode){
                GameManager.Instance.isGameOver = true;
            }
        }
        if(collision.gameObject.tag.Equals("NotGameOverObject")){
            if(isColorChangeMode){
                GameManager.Instance.isGameOver = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("ColorChangeItem")){
            if (isColorChangeMode){
                isColorChangeMode = false;
            }else{
                isColorChangeMode = true;
            }
            GameManager.Instance.isColorChanged = isColorChangeMode;
            Debug.Log("ColorChange!");
        }
        if(other.tag.Equals("EndPoint")){
            GameManager.Instance.isEndPoint = true;
            GameManager.Instance.AddStageNum();
            Debug.Log("EndPoint!");
            Debug.Log(GameManager.Instance.stageNum);
        }
    }

    void Start () {
        myRigidbody = GetComponent<Rigidbody>();
	}
    void FixedUpdate () {
        if (GameManager.Instance.isEndPoint){
            return;
        }
        if (GameManager.Instance.isUIOpen || GameManager.Instance.isGameOver){
            direction = Vector3.zero;
            myRigidbody.velocity = Vector3.zero;
            return;
        }

        if (transform.position.x >= 9.5f){
            direction = Vector3.zero;
            myRigidbody.velocity = Vector3.zero;
            isRightSide = true;
        }else{
            isRightSide = false;

        }
        if(transform.position.x <= -9.5f){
            direction = Vector3.zero;
            myRigidbody.velocity = Vector3.zero;
            isLeftSide = true;
        }else{
            isLeftSide = false;
        }

        if (Input.touchCount > 0){
            if (Input.GetTouch(0).phase == TouchPhase.Began){
                //Debug.Log("touch");
                isFirstTouch = true;
                beganPos = Input.GetTouch(0).position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved){
                Vector3 movedPos = Input.GetTouch(0).position;
                Vector3 movedDirection;
                if (isFirstTouch){
                    movedDirection = beganPos - movedPos;
                    isFirstTouch = false;
                }else{
                    movedDirection = previousMovedPos - movedPos;
                }if (movedDirection.x > 0 && isLeftSide)
                { // 왼쪽으로 드래그 한 상황
                    myRigidbody.velocity = Vector3.zero;
                    return;
                }
                if (movedDirection.x < 0 && isRightSide)
                { // 오른쪽으로 드래그 한 상황
                    myRigidbody.velocity = Vector3.zero;
                    return;
                }
                if (myRigidbody.velocity != Vector3.zero)
                {
                    velocity = myRigidbody.velocity;
                }
                this.direction = new Vector3(-movedDirection.x, 0, -movedDirection.y);
                myRigidbody.velocity = direction * speed * Time.deltaTime;
                previousMovedPos = movedPos;
                //Debug.Log("Moved");
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended){
                if (!isLeftSide && !isRightSide){
                    myRigidbody.AddForce(this.velocity * 0.5f, ForceMode.Impulse);
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Canceled){
                Debug.Log("Touch Canceled");
            }
        }
        transform.Translate(0, 0, translateSpeed);
	}
}

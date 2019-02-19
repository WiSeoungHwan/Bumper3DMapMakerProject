using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMapButton : MonoBehaviour {
    Stage stage;
    public void SaveMap(){
        stage.stageNumber = GameManager.Instance.stageNum;

    }
}

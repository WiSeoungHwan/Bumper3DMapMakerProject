using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : SingletonMonoBehaviour<UIManager> {
    public GameObject objectList;
    Object[] prefabs;
    public Button objectButton;
    public GameObject makeObject;
    public Toggle[] toggles;
    Vector3 makeObjectScale;
    Moving movingScript;
    //Vector3 newRectPos;
    bool isListOn;
    public void ObjectsButtonDidTap(){
        if(isListOn){
            objectList.SetActive(false);
            isListOn = false;
        }else{
            objectList.SetActive(true);
            isListOn = true;
        }


    }
    public int IsOnToggle(){
        int result = 1;
        for (int i = 0; i < toggles.Length; i++){
            if (toggles[i].isOn)
            {
                result = i;
            }
        }
        return result;
    }
    protected override void OnAwake()
    {
        base.OnAwake();
        movingScript = makeObject.GetComponent<Moving>();
        prefabs = Resources.LoadAll("Object");
        movingScript.prefab = prefabs[0] as GameObject;
    }
    protected override void OnStart()
    {
        base.OnStart();

        Debug.Log(prefabs.Length);
        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i] as GameObject;
            Button button = Instantiate(objectButton);
            button.name = prefab.tag;
            var rectPos = button.GetComponent<RectTransform>();
            var textObject = button.transform.Find("Text");
            var text = textObject.GetComponent<Text>();
            text.text = prefab.tag;
            Debug.Log(prefab.tag);
            button.onClick.AddListener(delegate
            {
                var prefabMesh = prefab.GetComponent<MeshFilter>();
                var makeObjectMeshFilter = makeObject.GetComponent<MeshFilter>();

                if (prefab.name == "ColorChangeItem")
                {
                    movingScript.isItem = true;
                    toggles[2].isOn = true;
                }else{
                    toggles[1].isOn = true;
                    movingScript.isItem = false;
                }
                movingScript.prefab = prefab;
                makeObjectMeshFilter.sharedMesh = prefabMesh.sharedMesh;
            });
            button.transform.SetParent(objectList.transform);
        }
    }
        
}


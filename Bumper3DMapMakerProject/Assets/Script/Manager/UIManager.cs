using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoBehaviour {
    public GameObject objectList;
    Object[] prefabs;
    public Button objectButton;
    //Vector3 newRectPos;

    public void ObjectsButtonDidTap(){
        objectList.SetActive(true);
    }
    public void ObjectsListCloseButtonDidTap(){
        objectList.SetActive(false);
    }

    private void Start()
    {
        prefabs = Resources.LoadAll("Object");
        Debug.Log(prefabs.Length);
        for (int i = 0; i < prefabs.Length; i++){
            
            GameObject prefab = prefabs[i] as GameObject;
            Button button = Instantiate(objectButton);
            button.name = prefab.tag;
            var rectPos = button.GetComponent<RectTransform>();
            var textObject = button.transform.FindChild("Text");
            var text = textObject.GetComponent<Text>();
            text.text = prefab.tag;
            Debug.Log(prefab.tag);
            button.onClick.AddListener(delegate
            {
                var someObject = Instantiate(prefab);

            });
            button.transform.SetParent(objectList.transform);


        }
    }
    void CreateObject(){
        
    }

}


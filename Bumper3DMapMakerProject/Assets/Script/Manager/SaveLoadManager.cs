using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public int stageNum;
    public Material[] materials;
    public GameObject stage;

    public void StageSave()
    {
        Stage stageInfo = new Stage();
        int objectCount = stage.transform.GetChildCount();
        stageInfo.blocks = new Block[objectCount];
        for (int i = 0; i < objectCount; i++)
        {
            Block blockInfo = new Block();
            Transform block = stage.transform.GetChild(i);
            if (block.GetComponent<NotGameOVerObject>() != null)
            { // NotGameOverObject를 가지고 있으면 
                blockInfo.objectNumber = 0;
            }
            if (block.GetComponent<GameOverObject>() != null)
            {
                blockInfo.objectNumber = 1;
            }
            if (block.GetComponent<ColorChangeItem>() != null)
            {
                blockInfo.objectNumber = 2;
            }
            blockInfo.shape = block.tag;
            blockInfo.pX = block.position.x;
            blockInfo.pY = block.position.y;
            blockInfo.pZ = block.position.z;
            blockInfo.sX = block.localScale.x;
            blockInfo.sY = block.localScale.y;
            blockInfo.sZ = block.localScale.z;
            stageInfo.blocks[i] = blockInfo;
        }
        stageInfo.stageNumber = stageNum;
        var filePath = Application.dataPath + "/Resources/Stage/stage" + stageNum + ".bytes";
        Debug.Log(filePath);
        BinaryData.BinarySerialize<Stage>(stageInfo, filePath);
        Debug.Log("SaveStage");
    }
    public void LoadStage()
    {
        stage.transform.DestroyChildren();
        string path = "Stage/stage" + stageNum;

        TextAsset binData = Resources.Load(path) as TextAsset;


        Debug.Log(binData.bytes);


        var loadStage = BinaryData.DeserializeObject<Stage>(binData.bytes);

        for (int i = 0; i < loadStage.blocks.Length; i++)
        {
            Block blocks = loadStage.blocks[i];
            Debug.Log(blocks.shape);
            GameObject prefab = Resources.Load<GameObject>("Object/" + blocks.shape);
            GameObject block = Instantiate(prefab);
            Vector3 position = new Vector3(blocks.pX, blocks.pY, blocks.pZ);
            Vector3 scale = new Vector3(blocks.sX, blocks.sY, blocks.sZ);
            block.transform.position = position;
            block.transform.localScale = scale;
            switch (blocks.objectNumber)
            {
                case 0:
                    //머티리얼 넣기 
                    block.GetComponent<MeshRenderer>().material = materials[1];
                    // 태그 넣기 
                    block.tag = "NotGameOverObject";
                    //스크립트 넣기 
                    block.AddComponent<NotGameOVerObject>();
                    break;
                case 1:
                    //머티리얼 넣기 
                    block.GetComponent<MeshRenderer>().material = materials[0];
                    // 태그 넣기 
                    block.tag = "GameOverObject";
                    //스크립트 넣기 
                    block.AddComponent<GameOverObject>();
                    break;
                case 2:
                    //머티리얼 넣기 
                    block.GetComponent<MeshRenderer>().material = materials[2];
                    // 태그 넣기 
                    block.tag = "ColorChangeItem";
                    //스크립트 넣기 
                    block.AddComponent<ColorChangeItem>();
                    break;
                default:
                    return;
            }
            block.transform.SetParent(stage.transform);
            block.SetActive(true);
        }
        Debug.Log("LoadStage");
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : SingletonMonoBehaviour<GameManager> {
    public Material[] materials;
    public GameObject gameoverPanel;
    public GameObject touchToStart;
    public MainCamera mainCamera;
    public Ball ball;
    public Ground ground;
    public UILabel stageNumLabel;
    public UILabel stageNumber;
    public bool isColorChanged;
    public bool isGameOver;
    public bool isUIOpen;
    public bool isEndPoint;
    private float translateSpeed;
    public int stageNum;
    string stagePath = "/stage";


    protected override void OnAwake()
    {
        base.OnAwake();
        PlayerPefsSetting();
        LoadStage();
       
    }

    protected override void OnStart()
    {
        base.OnStart();
        translateSpeed = 0f;
        touchToStart.SetActive(true);
        gameoverPanel.SetActive(false);
        isGameOver = false;
        isEndPoint = false;
    }
    void LoadStage()
    {
        string path = "Stage/stage" + stageNum;

        TextAsset binData = Resources.Load(path) as TextAsset;

        if (binData == null)
        {
            binData = Resources.Load("Stage/stage1") as TextAsset;
        }

        Debug.Log(binData.bytes);


        var stage = BinaryData.DeserializeObject<Stage>(binData.bytes);

        Debug.Log(stage.stageNumber);


        for (int i = 0; i < stage.blocks.Length; i++)
        {
            Block blocks = stage.blocks[i];
            GameObject prefab = Resources.Load<GameObject>("Object/" + blocks.shape);
            Debug.Log(blocks.shape);
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
            block.SetActive(true);

        }
    }
    void PlayerPefsSetting(){
        if(PlayerPrefs.HasKey("StageLevel")){
            stageNum = PlayerPrefs.GetInt("StageLevel");
        }else{
            PlayerPrefs.SetInt("StageLevel", stageNum);
        }
        Debug.Log(stageNum);
    }
    public void AddStageNum(){
        this.stageNum += 1;
        PlayerPrefs.SetInt("StageLevel", stageNum);
        SceneManager.LoadScene(0);
    }
    public void ReloadScene(){
        SceneManager.LoadScene(0);
    }
    void ChangeMaterial(){
        if(isColorChanged){
            ball.GetComponent<MeshRenderer>().material = materials[0];
        }else{
            ball.GetComponent<MeshRenderer>().material = materials[1];
        }
        return;
        
    }
    // Update is called once per frame
    void FixedUpdate () {
        ChangeMaterial();
        stageNumber.text = "Stage " + stageNum;
        if (isUIOpen){
            return;
        }
        if (isGameOver){
            gameoverPanel.SetActive(true);
            touchToStart.SetActive(false);
            translateSpeed = 0f;
            mainCamera.translateSpeed = translateSpeed;
            ball.translateSpeed = translateSpeed;
            stageNumLabel.text = stageNum.ToString();

            return;
        }else{
            gameoverPanel.SetActive(false);
        }
        if (Input.touchCount > 0){
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                translateSpeed = 0.4f;
                touchToStart.SetActive(false);
                ball.translateSpeed = translateSpeed;
                mainCamera.translateSpeed = translateSpeed;
            }
        }

	}
}

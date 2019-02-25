using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Material[] materials; // 0 :GameOver, 1: NotGameOver, 2: Item
    public float speed = 10f;
    public bool isItem;
    MeshRenderer meshRenderer;
    public GameObject prefab;
    MeshFilter meshFilter;
    public GameObject stage;
    public GameObject itemPrefab;

    // Use this for initialization
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[1];
    }
    IEnumerator MakeObject()
    {
        if(UIManager.Instance.IsOnToggle() == 2){
            GameObject itemObject = Instantiate(itemPrefab);
            itemObject.transform.position = gameObject.transform.position;
            itemObject.transform.SetParent(stage.transform);
            itemObject.SetActive(true);
            yield return null;
        }else{
            if (prefab != null)
            {
                if (!isItem)
                {
                    GameObject instanceObject = Instantiate(prefab);
                    switch (UIManager.Instance.IsOnToggle())
                    {
                        case 0:
                            instanceObject.AddComponent<GameOverObject>();
                            instanceObject.GetComponent<MeshRenderer>().sharedMaterial = materials[0];
                            Debug.Log("game");
                            break;
                        case 1:
                            instanceObject.AddComponent<NotGameOVerObject>();
                            instanceObject.GetComponent<MeshRenderer>().sharedMaterial = materials[1];
                            Debug.Log("not");
                            break;
                        default:
                            break;
                    }
                    instanceObject.transform.localScale = transform.localScale;
                    instanceObject.transform.position = gameObject.transform.position;
                    instanceObject.transform.SetParent(stage.transform);
                    instanceObject.SetActive(true);
                }
                else
                {
                    GameObject itemObject = Instantiate(itemPrefab);
                    itemObject.transform.position = gameObject.transform.position;
                    itemObject.transform.SetParent(stage.transform);
                    itemObject.SetActive(true);
                }
            }
        }

        Debug.Log("make");
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.D))
        {
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("ColorChangeItem"))
        {
            Debug.Log("TItem");
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("ColorChangeItem"))
        {
            Debug.Log("Item");
        }

    }
    // Update is called once per frame
    void Update()
    {
        switch (UIManager.Instance.IsOnToggle())
        {
            case 0:
                meshRenderer.material = materials[0];
                meshFilter.sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;
                break;
            case 1:
                meshRenderer.material = materials[1];
                meshFilter.sharedMesh = prefab.GetComponent<MeshFilter>().sharedMesh;
                break;
            case 2:
                meshRenderer.material = materials[2];
                break;
            default:
                break;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine("MakeObject");
            StopCoroutine("MakeObject");
        }
    }
}

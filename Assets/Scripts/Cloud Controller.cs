using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CloudController : MonoBehaviour
{
    // Start is called before the first frame update
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode dropKey;
    public float speed = 5f;

    public int handIndex = 0;
    public int nextIndex = 0;
    public GameObject[] handFruits;
    public GameObject[] nextFruits;

    public GameManager gameManager;
    public FruitManager fruitManager;
    void Start()
    {
        handIndex = Random.Range(0, 5);
        nextIndex = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 2.80)
        {
            transform.position = new Vector3(2.80f, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -2.50)
        {
            transform.position = new Vector3(-2.50f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(leftKey))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(rightKey))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
        if (Input.GetKeyDown(dropKey) && gameManager.isDroppable)
        {
            fruitManager.Spawn(handIndex, transform.position, true);
            handFruits[handIndex].SetActive(false);
            handIndex = nextIndex;
            nextIndex = Random.Range(0, 5);
            gameManager.isDroppable = false;
        }
        if (gameManager.isDroppable)
        {
            Display();
        }
    }

    void Display()
    {
        for (int i = 0; i < handFruits.Length; i++)
        {
            handFruits[i].SetActive(false);
            nextFruits[i].SetActive(false);
        }
        handFruits[handIndex].SetActive(true);
        nextFruits[nextIndex].SetActive(true);
    }
}

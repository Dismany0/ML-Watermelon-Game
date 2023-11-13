using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudControl1 : MonoBehaviour
{
    private int randint = 5;
    public int handIndex = 0;
    public int nextIndex = 1;
    public Vector3 startPos;
    public GameObject[] handFruits;
    public GameObject[] nextFruits;
    public GameManager1 gameManager;
    public FruitManager1 fruitManager;
    private AudioSource dropSound;

    private float walldist = 3.35f;

    public float cooldown = 0.25f;

    void Awake()
    {
        handIndex = Random.Range(0, randint);
        nextIndex = Random.Range(0, randint);
        startPos = transform.position;
        dropSound = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.isDroppable)
        {
            move(0);
            Display();
        }
        if (!gameManager.isGameActive)
        {

        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void move(int direction)
    {
        Fruit1 fruit = fruitManager.FruitArray[handIndex];
        float fruitRadius = fruit.GetComponent<CircleCollider2D>().radius * fruit.transform.localScale.x;


        if (gameManager.isGameActive)
        {
            transform.position += Vector3.right * direction * Time.deltaTime * 5f;
        }
        if (transform.position.x >= startPos.x + walldist - fruitRadius)
        {
            transform.position = new Vector3(startPos.x + walldist - fruitRadius, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= startPos.x - walldist + fruitRadius)
        {
            transform.position = new Vector3(startPos.x - walldist + fruitRadius, transform.position.y, transform.position.z);
        }
    }

    public void drop()
    {
        if (gameManager.isDroppable && gameManager.isGameActive && cooldown <= 0)
        {
            fruitManager.Spawn(handIndex, transform.position, true);
            handFruits[handIndex].SetActive(false);
            handIndex = nextIndex;
            nextIndex = Random.Range(0, randint);
            gameManager.isDroppable = false;
            cooldown = 0.25f;
            dropSound.pitch = UnityEngine.Random.Range(2f, 2.5f);
            dropSound.Play();
        }
        if (gameManager.fruitCount > 250)
        {
            gameManager.isGameActive = false;
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

    public void Restart()
    {
        handIndex = Random.Range(0, randint);
        nextIndex = Random.Range(0, randint);
        transform.position = startPos;
        Display();
        gameManager.Restart();
        fruitManager.Restart();
    }
}

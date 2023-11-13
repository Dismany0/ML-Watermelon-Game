using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloudControl : MonoBehaviour
{
    private int randint = 5;
    public int handIndex = 0;
    public int nextIndex = 1;
    public Vector3 startPos;
    public GameObject[] handFruits;
    public GameObject[] nextFruits;
    public GameManager gameManager;
    public FruitManager fruitManager;

    private float walldist = 3.35f;

    public CloudAgent cloudAgent;

    public float cooldown = 0.25f;
    public bool moving = false;
    public float pos = 0;


    void Awake()
    {
        handIndex = Random.Range(0, randint);
        nextIndex = Random.Range(0, randint);
        startPos = transform.position;
        cloudAgent = GetComponentInParent<CloudAgent>();
    }

    public void MoveTo(float position)
    {
        //Takes a position from -1 to 1 and moves the cloud to that position

        if (!moving)
        {

            
            Fruit fruit = fruitManager.FruitArray[handIndex];
            float fruitRadius = fruit.GetComponent<CircleCollider2D>().radius * fruit.transform.localScale.x;
            float minTargetPosition = startPos.x - walldist + fruitRadius;
            float maxTargetPosition = startPos.x + walldist - fruitRadius;
            float targetPosition = Mathf.Lerp(minTargetPosition, maxTargetPosition, position);
            // float targetPosition = Mathf.Clamp(position, minTargetPosition, maxTargetPosition);

            StartCoroutine(MoveToPos(targetPosition));
        }

    }

    private IEnumerator MoveToPos(float position)
    {
        moving = true;
        float time = Time.time;
        float start = transform.position.x;

        while (Time.time - time < 1f)
        {
            transform.position = new Vector3(Mathf.Lerp(start, position, Time.time - time), transform.position.y, transform.position.z);
            yield return null;
        }
        transform.position = new Vector3(position, transform.position.y, transform.position.z);
        drop();
        moving = false;
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
            cloudAgent.EndEpisode();
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public void move(int direction)
    {
        Fruit fruit = fruitManager.FruitArray[handIndex];
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
        if (gameManager.isDroppable && gameManager.isGameActive)
        {
            fruitManager.Spawn(handIndex, transform.position, true);
            handFruits[handIndex].SetActive(false);
            handIndex = nextIndex;
            nextIndex = Random.Range(0, randint);
            gameManager.isDroppable = false;
            cooldown = 0.25f;
        }
        if (gameManager.fruitCount > 250)
        {
            gameManager.isGameActive = false;
        }
        if (gameManager.score > 3000)
        {
            cloudAgent.AddReward(3f);
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

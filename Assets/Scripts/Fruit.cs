using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameManager gameManager;
    public FruitManager fruitManager;
    public int index;
    public bool firstCollision;

    public bool fused = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameObject.CompareTag(other.gameObject.tag) && (gameObject.GetInstanceID() < other.gameObject.GetInstanceID()) && index < 10)
        {
            if (!(fused || other.gameObject.GetComponent<Fruit>().fused))
            {
                Vector3 spawnPos = (gameObject.transform.position + other.gameObject.transform.position) / 2;

                //TAKE OUT PARTICLES FOR TRAINING
                float particlesize = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
                fruitManager.SpawnParticles(transform.position, particlesize);
                fruitManager.SpawnParticles(other.transform.position, particlesize);
                fused = true;
                other.gameObject.GetComponent<Fruit>().fused = true;

                Destroy(gameObject);
                Destroy(other.gameObject);

                fruitManager.Spawn(index + 1, spawnPos, false);
                gameManager.fruitCount -= 2;
                gameManager.addScore(gameManager.fruitScores[index]);
            }
        }
        if (!gameManager.isDroppable && firstCollision)
        {
            gameManager.isDroppable = true;
            firstCollision = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gameOver") && gameManager.isDroppable)
        {
            gameManager.isGameActive = false;
            StartCoroutine(fruitManager.DeathFireworks());
        }
    }
}

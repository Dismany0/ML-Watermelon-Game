using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameManager1 gameManager;
    public FruitManager1 fruitManager;
    public int index;
    public bool firstCollision;
    private AudioSource audioSource;

    public bool fused = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!firstCollision)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
            audioSource.Play();
        }
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
            if (!(fused || other.gameObject.GetComponent<Fruit1>().fused))
            {
                Vector3 spawnPos = (gameObject.transform.position + other.gameObject.transform.position) / 2;

                
                float particlesize = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
                fruitManager.SpawnParticles(transform.position, particlesize);
                fruitManager.SpawnParticles(other.transform.position, particlesize);
                fused = true;
                other.gameObject.GetComponent<Fruit1>().fused = true;

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
        if (other.gameObject.CompareTag("gameOver") && gameManager.isDroppable && gameManager.isGameActive)
        {
            gameManager.isGameActive = false;
            gameManager.cam.backgroundColor = Color.black;
            gameManager.GameOverText.gameObject.SetActive(true);
            Debug.Log("Game Over");
            fruitManager.SpawnParticles(transform.position, 5);
            StartCoroutine(fruitManager.DeathFireworks());
        }
    }
}

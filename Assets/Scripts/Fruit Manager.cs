using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public Fruit[] FruitArray;
    public GameManager gameManager;
    public int attemptCount = 0;

    public ParticleSystem particles;

    public ParticleSystem gameOverParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(int index, Vector3 Pos, bool firstCollision){
        if(index >= 0 && index < FruitArray.Length){
            Fruit newFruit = Instantiate(FruitArray[index], Pos, quaternion.identity, gameObject.transform);
            newFruit.index = index;
            newFruit.gameManager = gameManager;
            newFruit.fruitManager = this;
            newFruit.rb = newFruit.GetComponent<Rigidbody2D>();
            newFruit.firstCollision = firstCollision;
            gameManager.fruitCount++;
            if (!firstCollision){
                //Add a bit of random force upward to the fruit depending on the size of the fruit
                // newFruit.rb.AddForce((newFruit.index + 1) * UnityEngine.Random.Range(-1.5f, 1.5f) * Vector2.up * 0.5f, ForceMode2D.Impulse);
            }
        }
    }

    public void SpawnParticles(Vector3 Pos, float size){
        if (gameManager.isGameActive){
        ParticleSystem newParticles = Instantiate(particles, Pos, quaternion.identity, gameManager.transform);
        newParticles.transform.localScale = new Vector3(size, size, size);
        newParticles.Play();

    }else {
            ParticleSystem newParticles = Instantiate(gameOverParticles, Pos, quaternion.identity, gameManager.transform);
            newParticles.transform.localScale = new Vector3(size, size, size);
            AudioSource audioSource = newParticles.GetComponentInChildren<AudioSource>();
            audioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
            newParticles.Play();
        }
    }

    public IEnumerator DeathFireworks(){
        for (int i = 0; i < 10; i++){
            if(gameManager.isGameActive){
                break;
            }
            SpawnParticles(new Vector3(UnityEngine.Random.Range(-3.5f, 3.5f), UnityEngine.Random.Range(-3.5f, 3.5f), 0), 2f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Restart(){
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGameActive = false;
    public bool isDroppable = true;
    public int[] fruitScores = new int[] { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55 };
    public int score = 0;
    public int fruitCount = 0;
    public int attempt = 1;

    public int[] highScores = new int[3];
    public int[] attemptnum = new int[3];
    public float curSinValue = 0;

    public Camera cam;
    public float colourHue = 44f;
    public TextMeshPro scoreText;
    public TextMeshPro GameOverText;
    public TextMeshPro A1; public TextMeshPro A2; public TextMeshPro A3;
    public TextMeshPro S1; public TextMeshPro S2; public TextMeshPro S3;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            float value = math.sin(curSinValue) * 10;
            curSinValue += Time.deltaTime;
            cam.backgroundColor = Color.HSVToRGB((colourHue + value) % 360 / 360f, 0.6f, 0.6f);
        }
        //I want to make the background constantly pulse up and down using a sin function and the colour to add

    }
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScore();
        //Get HSV value of the current background color, add addHue to the hue, and set the background color to the new HSV value
        colourHue += math.max(scoreToAdd / 5f, 1);
        Debug.Log(colourHue);
        if (isGameActive)
            cam.backgroundColor = Color.HSVToRGB(colourHue % 360 / 360f, 0.6f, 0.6f);
    }

     public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (score > highScores[i])
            {
                for (int j = highScores.Length - 1; j > i; j--)
                {
                    highScores[j] = highScores[j - 1];
                    attemptnum[j] = attemptnum[j - 1];
                }
                highScores[i] = score;
                attemptnum[i] = attempt;
                break;
            }
        }
        A1.text = attemptnum[0].ToString();
        A2.text = attemptnum[1].ToString();
        A3.text = attemptnum[2].ToString();
        S1.text = highScores[0].ToString();
        S2.text = highScores[1].ToString();
        S3.text = highScores[2].ToString();
    }

    public void Restart()
    {
        UpdateHighScores();
        attempt++;
        score = 0;
        fruitCount = 0;
        isGameActive = true;
        isDroppable = true;
        UpdateScore();
        GameOverText.gameObject.SetActive(false);
        colourHue = UnityEngine.Random.Range(0, 360);
        cam.backgroundColor = Color.HSVToRGB(colourHue % 360 / 360f, 0.6f, 0.6f);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGameActive = true;
    public bool isDroppable = true;
    public int[] fruitScores = new int[] { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55 };

    public int score = 0;
    public int fruitCount = 0;

    public int attempt = -1;

    public CloudAgent cloudAgent;

    public int[] highScores = new int[3];
    public int[] attemptnum = new int[3];

    public TextMeshPro scoreText;
    public TextMeshPro attemptText;
    public TextMeshPro A1; public TextMeshPro A2; public TextMeshPro A3; public TextMeshPro A1Small;
    public TextMeshPro S1; public TextMeshPro S2; public TextMeshPro S3; public TextMeshPro S1Small;

    public GameObject neutralPanel;
    public GameObject winPanel;
    public SetManager setManager;
    void Start()
    {
        cloudAgent = GetComponentInParent<CloudAgent>();
        setManager = cloudAgent.GetComponentInParent<SetManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScore();
        cloudAgent.AddReward(scoreToAdd * 0.01f);
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateHighScores(){
        for (int i = 0; i < highScores.Length; i++){
            if (score > highScores[i]){
                for (int j = highScores.Length - 1; j > i; j--){
                    highScores[j] = highScores[j-1];
                    attemptnum[j] = attemptnum[j-1];
                }
                highScores[i] = score;
                attemptnum[i] = attempt;
                break;
            }
        }
        A1.text = attemptnum[0].ToString();
        A1Small.text = attemptnum[0].ToString();
        A2.text = attemptnum[1].ToString();
        A3.text = attemptnum[2].ToString();
        S1.text = highScores[0].ToString();
        S1Small.text = highScores[0].ToString();
        S2.text = highScores[1].ToString();
        S3.text = highScores[2].ToString();
        attemptText.text = (attempt + 1).ToString();
        setManager.updateBackgrounds();
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

    }
}

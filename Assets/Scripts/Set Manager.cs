using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager[] gameManagers;
    public int highestScore = 0;

    public CameraMove setCam;
    void Start()
    {
        gameManagers = GetComponentsInChildren<GameManager>();
        setCam = FindObjectOfType<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateBackgrounds(){
        GameManager highestScoreAgent = gameManagers[0];
        for (int i = 0; i < gameManagers.Length; i++){
            if (gameManagers[i].highScores[0] > highestScoreAgent.highScores[0]){
                highestScoreAgent = gameManagers[i];
            }
        }
        for (int i = 0; i < gameManagers.Length; i++){
            gameManagers[i].neutralPanel.SetActive(true);
            gameManagers[i].winPanel.SetActive(false);
        }
        highestScoreAgent.neutralPanel.SetActive(false);
        highestScoreAgent.winPanel.SetActive(true);
        highestScore = highestScoreAgent.highScores[0];
        setCam.MoveHighestScore();
    }
}

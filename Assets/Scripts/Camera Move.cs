using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public SetManager[] setManager;
    public bool moving;

    public bool auto = true;
    void Start()
    {
        setManager = FindObjectsOfType<SetManager>();
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void MoveHighestScore()
    {
        Vector3 highestScorePos = setManager[0].transform.position;
        int highestScore = setManager[0].highestScore;
        for (int i = 0; i < setManager.Length; i++)
        {
            if (setManager[i].highestScore > highestScore)
            {
                highestScore = setManager[i].highestScore;
                highestScorePos = setManager[i].transform.position;
            }
        }
        MoveTo(highestScorePos);
        
    }

    public void MoveTo(Vector3 pos)
    {
        if (!moving)
        {
            pos.z = -10;
            pos.y += 3.5f;
            // StartCoroutine(MoveToPos(pos));
        }

    }

    private IEnumerator MoveToPos(Vector3 pos)
    {
        moving = true;
        float time = Time.time;
        float start = transform.position.x;

        while (Time.time - time < 30f)
        {
            Debug.Log((Time.time - time) / 30f);
            transform.position = Vector3.Lerp(transform.position, pos, (Time.time - time) / 30f);
            yield return null;
        }
        transform.position = pos;
        moving = false;
    }
}

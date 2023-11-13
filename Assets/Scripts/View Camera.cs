using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ViewCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public bool action = false;
    public bool viewAll = false;
    public bool viewOne = false;
    public bool view4 = false;
    public float timeLeft = 0.0f;

    bool auto = true;
    private Vector3 offset = new Vector3(4.5f, 3, 0);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!action && auto)
        {
            chooseMode();
            action = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            auto = !auto;
        }
        if (!auto)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                timeLeft = 3f;
                StartCoroutine(ViewAll());
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                timeLeft = 3f;
                StartCoroutine(View4());
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                timeLeft = 3f;
                StartCoroutine(ViewOne());
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * 5f;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * 5f;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position += Vector3.up * 5f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += Vector3.down * 5f;
            }
        }


    }

    void chooseMode()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        viewAll = false;
        viewOne = false;
        view4 = false;
        timeLeft = UnityEngine.Random.Range(20.0f, 60.0f);
        if (rand == 0)
        {
            viewAll = true;
            StartCoroutine(ViewAll());
        }
        else if (rand == 1)
        {
            view4 = true;
            StartCoroutine(View4());
        }
        else
        {
            viewOne = true;
            StartCoroutine(ViewOne());
        }
        
    }

    private IEnumerator ViewAll()
    {
        StartCoroutine(MoveToPosition(offset + Vector3.right, 5f));
        var curZoom = Camera.main.orthographicSize;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 5f;
            Camera.main.orthographicSize = Mathf.Lerp(curZoom, 30, t);
            yield return null;
        }
        yield return new WaitForSeconds(timeLeft);
        action = false;
    }

    private IEnumerator ViewOne()
    {
        int numTimes = (int)(timeLeft / 10f);

        int xpos = UnityEngine.Random.Range(-1, 1);
        int ypos = UnityEngine.Random.Range(-1, 1);
        Vector3 pos = new Vector3(xpos * 37, ypos * 20, 0) + offset;
        var curZoom = Camera.main.orthographicSize;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 2.5f;
            Camera.main.orthographicSize = Mathf.Lerp(curZoom, 10, t);
            yield return null;
        }

        for (int i = 0; i < numTimes; i++)
        {
            xpos = UnityEngine.Random.Range(-1, 2);
            ypos = UnityEngine.Random.Range(-1, 2);
            pos = new Vector3(xpos * 37, ypos * 20, 0) + offset;
            StartCoroutine(MoveToPosition(pos, 2.5f));
            float timeToWait = UnityEngine.Random.Range(10f, 30f);
            yield return new WaitForSeconds(timeToWait);
        }
        action = false;
    }

    private IEnumerator View4()
    {
        int numTimes = (int)(timeLeft / 20f);

        int xpos = UnityEngine.Random.Range(0, 2);
        int ypos = UnityEngine.Random.Range(0, 2);
        Vector3 pos = new Vector3(-13 + xpos * 37, -7f + ypos * 20, 0);
        var curZoom = Camera.main.orthographicSize;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 2.5f;
            Camera.main.orthographicSize = Mathf.Lerp(curZoom, 20, t);
            yield return null;
        }

        for (int i = 0; i < numTimes; i++)
        {
            xpos = UnityEngine.Random.Range(0, 2);
            ypos = UnityEngine.Random.Range(0, 2);
            pos = new Vector3(-13 + xpos * 37, -7f + ypos * 20, 0);
            StartCoroutine(MoveToPosition(pos, 2.5f));
            float timeToWait = UnityEngine.Random.Range(10f, 30f);
            yield return new WaitForSeconds(timeToWait);
        }
        action = false;
    }


    private IEnumerator MoveToPosition(Vector3 target, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, target, t);
            yield return null;
        }
    }

}

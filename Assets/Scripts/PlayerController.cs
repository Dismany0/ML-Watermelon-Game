using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode dropKey;

    public CloudControl1 cloudControl;
    public SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftKey))
        {
            cloudControl.move(-1);
        }
        if (Input.GetKey(rightKey))
        {
            cloudControl.move(1);
        }
        if (Input.GetKeyDown(dropKey))
        {
            cloudControl.drop();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            cloudControl.Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.N)){
            soundManager.skip = true;
        }
    }
}

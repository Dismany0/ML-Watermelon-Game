using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Music;
    public AudioSource audioSource;
    public bool playing = false;
    public int index = 0;

    public bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomPerm(1);
    }
    void loadMusic(){
        audioSource.clip = Music[index];
        audioSource.Play();
        playing = true;
    }
    void randomPerm(int startIndex){
        for (int i = startIndex; i < Music.Length; i++){
            int rand = Random.Range(startIndex, Music.Length);
            AudioClip temp = Music[i];
            Music[i] = Music[rand];
            Music[rand] = temp;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!playing){
            loadMusic();
        }
        if (!audioSource.isPlaying){
            index++;
            if (index >= Music.Length){
                index = 0;
                randomPerm(0);
            }
            loadMusic();
        }
        if(playing && skip){
            audioSource.Stop();
            // playing = false;
            skip = false;
        }
    }

}

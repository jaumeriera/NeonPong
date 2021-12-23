using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip collision;
    public AudioClip goal;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    public void GoalSound(){
        GetComponent<AudioSource>().clip = goal;
        GetComponent<AudioSource>().Play();
    }
    
    public void HitSound(){ 
        GetComponent<AudioSource>().clip = collision;
        GetComponent<AudioSource>().Play();
    }
}

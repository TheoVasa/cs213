using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public AudioClip winAudioClip;
    public AudioClip looseAudioClip;
    private AudioSource winAudioSource;
    private AudioSource looseAudioSource; 
    private int points; 
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        winAudioSource = gameObject.AddComponent<AudioSource>();
        winAudioSource.clip = winAudioClip;

        looseAudioSource= gameObject.AddComponent<AudioSource>();
        looseAudioSource.clip = looseAudioClip;
    }
    public void IncreasePoints(int nbr)
    {
        winAudioSource.Play();
        points = points + nbr; 
    }
    public void DecreasePoints(int nbr)
    {
        looseAudioSource.Play();
        points = points - nbr; 
    }
    public int Points()
    {
        return points;
    }
}

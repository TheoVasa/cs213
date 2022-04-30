using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    private AudioSource GemSpawnSound;
    public AudioClip GemSpawnClip;
    private AudioSource GemDespawnSound;
    public AudioClip GemDespawnClip;

    public GameObject gemPrefab;
    private GameObject gem; 
    public float GemTimeSpawn;

    //bounds of the map
    private int MaxMapX = 25;
    private int MinMapX = 2;
    private int MaxMapZ = -2;
    private int MinMapZ = -18;

    // Start is called before the first frame update
    void Start()
    {
        //creating the sounds 
        GemSpawnSound = gameObject.AddComponent<AudioSource>();
        GemSpawnSound.clip = GemSpawnClip;
        GemDespawnSound = gameObject.AddComponent<AudioSource>();
        GemDespawnSound.clip = GemDespawnClip;

        //handle the next spawn 
        Invoke("Spawn", GemTimeSpawn);
    }


    private void Spawn()
    {
        //Play the sound 
        GemSpawnSound.Play();

   
        //generating random position 
        //TODO make sure the gem doesn't spawn out of the map 
        float newX = Random.Range(MinMapX, MaxMapX);
        float newZ = Random.Range(MinMapZ, MaxMapZ);

        gem = Instantiate(gemPrefab) as GameObject;
        gem.transform.SetParent(gameObject.transform);
        gem.transform.position = new Vector3(newX, 0, newZ);
        
    }

    public void Despawn()
    {
        //play the sound 
        GemDespawnSound.Play();
        Destroy(gem);

        //handle the next spawn 
        Invoke("Spawn", GemTimeSpawn);
    }
}

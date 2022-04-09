using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour    
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other){
        if(other.transform.parent.gameObject.tag == "sheep")
        {
            Debug.Log(other.transform.parent.gameObject.name + " triggers.");
            GameObject dog = FindClosestEnemy();
            dog.GetComponent<PointSystem>().IncreasePoints();
        }
     }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("dog");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

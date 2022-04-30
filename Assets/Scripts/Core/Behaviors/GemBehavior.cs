using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.tag == "dog")
        {
            //make the dog earn the gem
            other.transform.parent.gameObject.GetComponent<DogBehavior>().EarnGem();
            //make the gem despawn
            gameObject.GetComponentInParent<GemSpawner>().Despawn();     
        }
    }
}

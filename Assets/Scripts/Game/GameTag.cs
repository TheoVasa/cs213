using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerType
{
    dog,
    sheep,
    ghost
}
public class GameTag : MonoBehaviour
{
    public playerType playerType;
    private string thisTag;
   
    // Start is called before the first frame update
    void Start()
    {
        if (playerType == playerType.dog)
        {
            thisTag = "dog";
        }
        else if (playerType == playerType.sheep)
        {

            thisTag = "sheep";
        }
        else
        {
            thisTag = "ghost";
        }
        gameObject.tag = thisTag; 
    }
 

}

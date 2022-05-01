using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreColorP2 : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.color = Settings.colorP2;
    }
}

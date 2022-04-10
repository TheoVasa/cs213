using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public GameObject dog;
    private int score;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + dog.GetComponent<PointSystem>().Points();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + dog.GetComponent<PointSystem>().Points();
    }
}

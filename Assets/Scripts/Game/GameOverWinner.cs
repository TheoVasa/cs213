using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverWinner : MonoBehaviour
{

    public GameObject dog1;
    public GameObject dog2;
    private int score1;
    private int score2;
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        score1 = dog1.GetComponent<PointSystem>().Points();
        score2 = dog2.GetComponent<PointSystem>().Points();

        if (score1 > score2){
            gameOverText.color = Settings.colorP1;
            gameOverText.text = "Game Over ! \nPlayer1 won the game, press \"Start\".";            
        } else if (score2 > score1){
            gameOverText.color = Settings.colorP2;
            gameOverText.text = "Game Over ! \nPlayer2 won the game, press \"Start\".";
        } else {
            gameOverText.color = Color.black;
            gameOverText.text = "Game Over ! \nBoth player have the same amount of points. It's a draw, \n press \"Start\"";
        }
    }
}

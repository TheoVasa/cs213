using UnityEngine;
using UnityEngine.UI;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
    public float timeValue;
    public Text timerText;
    public Slider slider;
    public float maxMinutes;
    public GameObject gameOver;
    public GameManager gameManager;

    public bool pauseGame;
    private bool gameStart;

    public void initTimer(){
        pauseGame = false;
        gameStart = true;
        initTimerValue = Time.time; 
        timeValue = maxMinutes*60;
    }
    public void Awake() {
        initTimerValue = Time.time; 
    }

    // Start is called before the first frame update
    public void Start() {
        pauseGame = true;
        gameStart = false;
        timeValue = 0;
        timerText = GetComponent<Text>();
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        gameOver.SetActive(false);
        maxMinutes = 2;
        slider.maxValue = maxMinutes*60;
        slider.value = maxMinutes*60;
    }

    void Update(){

        //Pause the game
        if (pauseGame){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }

        //Decrement time
        if  (timeValue > 0){
            timeValue -= Time.deltaTime;
            slider.value = (maxMinutes*60) - timeValue;
        } else {
            timeValue = 0;
            if (gameStart){
                gameOver.SetActive(true);
            }
            pauseGame = true;
        }

        //Display time
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay){
        if (timeToDisplay < 0){
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

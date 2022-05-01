using UnityEngine;
using UnityEngine.UI;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    public GameObject timer;
    public GameObject pauseButton;
    public GameObject muteButton;
    public GameObject startButton;

    public float timeValue;
    private Text timerText;
    public Slider slider;
    private float maxMinutes = 2;
    public GameObject gameOver;

    static public bool pauseGame;
    private bool gameStart;
    static public bool hasInitOnLongTouch = false;

    public void initTimer(){
        startButton .SetActive(false);
        timer.SetActive(true);
        muteButton.SetActive(true);
        pauseButton.SetActive(true);

        pauseGame = false;
        gameStart = true;
        timeValue = maxMinutes*60;
        Debug.Log("WSH2");
    }
    public void Awake() {}

    // Start is called before the first frame update
    public void Start() {
        pauseGame = true;
        gameStart = false;
        timeValue = 0;
        timerText = GetComponent<Text>();
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        gameOver.SetActive(false);
        maxMinutes = Settings.timeValue;
        slider.maxValue = maxMinutes*60;
        slider.value = maxMinutes*60;
    }

    void Update(){

        //Init on long touch
        if (hasInitOnLongTouch == true && gameStart == false){
            initTimer();
        }

        //Decrement time
        if  (timeValue > 0 && !pauseGame){
            timeValue -= Time.deltaTime;
            slider.value = (maxMinutes*60) - timeValue;
        } else {
            if (timeValue < 0){
                gameOver.SetActive(true);
                muteButton.SetActive(false);
                pauseButton.SetActive(false);
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

    public void Pause(){
        pauseGame = true;
    }

    public void Resume(){
        pauseGame = false;
    }
}

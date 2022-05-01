using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Settings : MonoBehaviour
{
    static public InputKeyboard inputKeyP1 = InputKeyboard.wasd;
    static public InputKeyboard inputKeyP2 = InputKeyboard.arrows;
    public TMP_Text textKeyboardInput;
    public Image imageColorP1;
    public Image imageColorP2;
    private LinkedList<string> colors;
    private string strColorP1;
    private string strColorP2;
    static public Color colorP1 = new Color(1f, 1f, 1f, 1f);
    static public Color colorP2 = new Color(0f, 0f, 0f, 1f);
    [SerializeField] Slider volumeSlider;
    private float oldVolumeValue;
    public Text inputField;
    static public int timeValue = 2;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = 1;
        SetVolume();
        oldVolumeValue = volumeSlider.value;

        //Init colors
        colors = new LinkedList<string>();
        colors.AddLast("black");
        colors.AddLast("blue");
        colors.AddLast("cyan");
        colors.AddLast("green");
        colors.AddLast("yellow");
        colors.AddLast("white");
        colors.AddLast("red");

        //Init color
        strColorP1 = "white";
        strColorP2 = "black";

        //Init keyboard
        inputKeyP1 = InputKeyboard.wasd;
        inputKeyP2 = InputKeyboard.arrows;
        textKeyboardInput.text = "P1: WASD \nP2: Arrows"; 

        //Init time value
        timeValue = 2;
        inputField.GetComponent<Text>().text = "2";
    }

    // Update is called once per frame
    void Update(){
        switch (strColorP1){
            case "black":
                colorP1 = Color.black;
                break;
            case "blue":
                colorP1 = Color.blue;
                break;
            case "cyan":
                colorP1 = Color.cyan;
                break;
            case "green":
                colorP1 = Color.green;
                break;
            case "yellow":
                colorP1 = Color.yellow;
                break;
            case "white":
                colorP1 = Color.white;
                break;
            case "red":
                colorP1 = Color.red;
                break;
            default:
                colorP1 = Color.white;
                break;
        }

        switch (strColorP2){
            case "black":
                colorP2 = Color.black;
                break;
            case "blue":
                colorP2 = Color.blue;
                break;
            case "cyan":
                colorP2 = Color.cyan;
                break;
            case "green":
                colorP2 = Color.green;
                break;
            case "yellow":
                colorP2 = Color.yellow;
                break;
            case "white":
                colorP2 = Color.white;
                break;
            case "red":
                colorP2 = Color.red;
                break;
            default:
                colorP2 = Color.white;
                break;
        }

        imageColorP1.color = colorP1;
        imageColorP2.color = colorP2;

        //Get input
        string strTimeValue = inputField.GetComponent<Text>().text;
        try{
            timeValue = Int32.Parse(strTimeValue);
        }
        catch (FormatException){
            timeValue = 2;
            inputField.GetComponent<Text>().text = "2";
        }

        //Adjust time
        if (timeValue > 20 || timeValue < 1){
            timeValue = 2;
            inputField.GetComponent<Text>().text = "2";
        }
    }

    public void SetVolume(){
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    public void nextColorP1(){
        LinkedListNode<string> nextColor = colors.Find(strColorP1).Next;

        if (nextColor == null){
            strColorP1 = colors.First.Value;
        } else {
            strColorP1 = nextColor.Value;
        }
    }

    public void nextColorP2(){
        LinkedListNode<string> nextColor = colors.Find(strColorP2).Next;

        if (nextColor == null){
            strColorP2 = colors.First.Value;
        } else {
            strColorP2 = nextColor.Value;
        }
    }

    public void previousColorP1(){
        LinkedListNode<string> nextColor = colors.Find(strColorP2).Previous;

        if (nextColor == null){
            strColorP1 = colors.Last.Value;
        } else {
            strColorP1 = nextColor.Value;
        }
    }

    public void previousColorP2(){
        LinkedListNode<string> nextColor = colors.Find(strColorP1).Previous;

        if (nextColor == null){
            strColorP2 = colors.Last.Value;
        } else {
            strColorP2 = nextColor.Value;
        }
    }

    public void nextControl(){
        if (inputKeyP1 == InputKeyboard.wasd){
            inputKeyP1 = InputKeyboard.arrows;
            inputKeyP2 = InputKeyboard.wasd;
            textKeyboardInput.text = "P1: Arrows \nP2: WASD"; 
        } else {
            inputKeyP1 = InputKeyboard.wasd;
            inputKeyP2 = InputKeyboard.arrows;
            textKeyboardInput.text = "P1: WASD \nP2: Arrows"; 
        }
    }

    public void previousControl(){
        if (inputKeyP1 == InputKeyboard.wasd){
            inputKeyP1 = InputKeyboard.arrows;
            inputKeyP2 = InputKeyboard.wasd;
            textKeyboardInput.text = "P1: Arrows \nP2: WASD"; 
        } else {
            inputKeyP1 = InputKeyboard.wasd;
            inputKeyP2 = InputKeyboard.arrows;
            textKeyboardInput.text = "P1: WASD \nP2: Arrows"; 
        }
    }

    private void LoadValues(){
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }

    public void Mute(){
        oldVolumeValue = volumeSlider.value;
        float volumeValue = 0f;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    public void Unmute(){
        float volumeValue = oldVolumeValue;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

}

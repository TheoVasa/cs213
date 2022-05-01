using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    public Slider volumeSlider;
    private float oldVolumeValue;
    void Start(){}
    public void mute(){
        oldVolumeValue = volumeSlider.value;
        float volumeValue = 0f;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    public void unmute(){
        float volumeValue = oldVolumeValue;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        LoadValues();
    }

    private void LoadValues(){
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
    }
}

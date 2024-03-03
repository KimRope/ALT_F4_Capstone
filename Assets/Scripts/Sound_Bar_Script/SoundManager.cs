using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Text percentUI;
    public float percent;

    [SerializeField] Slider volumeSlider;

    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else{
            Load();
        }
    }

    public void changeVolume(){
        //AudioListener.volume = volumeSlider.value;
        mixer.SetFloat("Music", Mathf.Log10(volumeSlider.value) * 20);
        percent = volumeSlider.value;
        percentUI.text = ((int)(percent * 100)).ToString() + "%";
        Save();      
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}

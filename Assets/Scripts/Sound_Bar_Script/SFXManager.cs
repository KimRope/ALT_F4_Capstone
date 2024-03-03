using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SFXManager : MonoBehaviour
{
    public Text percentUI;
    public float percent;

    [SerializeField] Slider volumeSlider;

    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("sfxVolume")){
            PlayerPrefs.SetFloat("sfxVolume", 1);
            Load();
        }
        else{
            Load();
        }
    }

    public void changeVolume(){
        //AudioListener.volume = volumeSlider.value;
        mixer.SetFloat("SFX", Mathf.Log10(volumeSlider.value) * 20);
        percent = volumeSlider.value;
        percentUI.text = ((int)(percent * 100)).ToString() + "%";
        Save();
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    private void Save(){
        PlayerPrefs.SetFloat("sfxVolume", volumeSlider.value);
    }

}

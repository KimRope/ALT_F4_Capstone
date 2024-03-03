using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCheat : MonoBehaviour
{
    public ParticleSystem CheatEffect;

    private void OnMouseDown()
    {
        CheatEffect.Play();
        if (!PlayerPrefs.HasKey("Cheat_Open"))
        {
            PlayerPrefs.SetInt("Cheat_Open", 1);
            PlayerPrefs.SetInt("Cheat_Int", 0);
        }

        int Cheat_Open = PlayerPrefs.GetInt("Cheat_Open");
        if(Cheat_Open == 0)
        {
            PlayerPrefs.SetInt("Cheat_Open", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Cheat_Open", 0);
            PlayerPrefs.SetInt("Cheat_Int", 0);
            
        }
    }
}

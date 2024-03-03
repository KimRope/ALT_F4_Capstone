using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatToggle : MonoBehaviour
{
    public Toggle CheaterToggle;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Cheat_Int"))
        {
            PlayerPrefs.SetInt("Cheat_Int", 0);
            Load();
        }
        else
        {
            Load();
        }
    }

    private void Update()
    {
        int Cheat_Open = PlayerPrefs.GetInt("Cheat_Open");
        if(Cheat_Open == 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void Load()
    {
        bool CHEAT = (PlayerPrefs.GetInt("Cheat_Int") == 1) ? true : false;
        CheaterToggle.isOn = CHEAT;
    }


    public void CheatToggleClick(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Cheat_Int", 1);
            Debug.Log("동의");
        }
        else
        {
            PlayerPrefs.SetInt("Cheat_Int", 0);
            Debug.Log("비동의");
        }
    }
}

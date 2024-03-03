using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperToggle : MonoBehaviour
{
    public Toggle HelpToggle;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("HELP_Int"))
        {
            PlayerPrefs.SetInt("HELP_Int", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    private void Load()
    {
        bool HELP = (PlayerPrefs.GetInt("HELP_Int") == 1) ? true : false;
        HelpToggle.isOn = HELP;
    }


    public void HelpToggleClick(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("HELP_Int", 1);
            Debug.Log("동의");
        }
        else
        {
            PlayerPrefs.SetInt("HELP_Int", 0);
            Debug.Log("비동의");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help_Toggle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int HELP_Int = PlayerPrefs.GetInt("HELP_Int", 1);
        if(HELP_Int == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    
}

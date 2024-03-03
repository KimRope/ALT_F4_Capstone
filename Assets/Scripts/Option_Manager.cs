using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
#endif

public class Option_Manager : MonoBehaviour
{

    Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Option")
        {
            Canvas_Enable();
        }
    }


    public void Canvas_Enable()
    {
            canvas.enabled = !canvas.enabled;
            Pause();
    }
    public void Pause()
    {
        if (canvas.enabled)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}

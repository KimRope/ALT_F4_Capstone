using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BUTTONType mainType;
    public STAGEType stageType;
    public Transform buttonScale;

    public CanvasGroup mainGroup;
    public CanvasGroup stageGroup;

    Canvas canvas;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
        canvas = GetComponent<Canvas>();
    }
    public void OnBtnClick()
    {
        switch (mainType)
        {
            case BUTTONType.New:
                SceneManager.LoadScene("Stage1_1");
                PlayerPrefs.SetInt("Load_int", 1);
                break;
            case BUTTONType.Continue:
                Continue();
                Debug.Log("Continue");
                break;
            case BUTTONType.Stage:
                CanvasGroupOn(stageGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BUTTONType.Option:
                Debug.Log("Option");
                SceneManager.LoadScene("Option");

                break;
            case BUTTONType.Back:
                Debug.Log("Back to Menu");
                Time.timeScale = 1;
                SceneManager.LoadScene("Main");
                break;
            case BUTTONType.Quit:
                Debug.Log("Quit");
                Application.Quit();
                break;
            case BUTTONType.Close:
                Debug.Log("Close");
                canvas = transform.parent.parent.GetComponent<Canvas>();
                canvas.enabled = !canvas.enabled;
                    if (canvas.enabled)
                        Time.timeScale = 0;
                    else
                        Time.timeScale = 1;
                break;
        }
    }
    public void OnStageClick()
    {
        switch (stageType)
        {
            case STAGEType.Stage_1:
                PlayerPrefs.SetInt("Load_int", 1);
                SceneManager.LoadScene("Stage1_1");
                break;
            case STAGEType.Stage_2:
                PlayerPrefs.SetInt("Load_int", 2);
                SceneManager.LoadScene("Stage1_2");
                break;
            case STAGEType.Stage_3:
                PlayerPrefs.SetInt("Load_int", 3);
                SceneManager.LoadScene("Stage1_3");
                break;
            case STAGEType.Stage_4:
                PlayerPrefs.SetInt("Load_int", 4);
                SceneManager.LoadScene("Stage2_1");
                break;
            case STAGEType.Stage_5:
                PlayerPrefs.SetInt("Load_int", 5);
                SceneManager.LoadScene("Stage2_2");
                break;

        }
    }

    void Continue()
    {
        int Load_int = PlayerPrefs.GetInt("Load_int", 0);
        switch (Load_int)
        {
            case 1:
                SceneManager.LoadScene("Stage1_1");
                break;
            case 2:
                SceneManager.LoadScene("Stage1_2");
                break;
            case 3:
                SceneManager.LoadScene("Stage1_3");
                break;
            case 4:
                SceneManager.LoadScene("Stage2_1");
                break;
            case 5:
                SceneManager.LoadScene("Stage2_2");
                break;
            default:
                break;
        }
    }



    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}

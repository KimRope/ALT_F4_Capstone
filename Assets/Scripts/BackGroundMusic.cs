using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour
{
    GameObject StartMusic;
    AudioSource backmusic;

    public AudioClip Bgm_1;
    public AudioClip Bgm_2;
    public AudioClip Bgm_3;
    public AudioClip Bgm_4;
    public AudioClip Bgm_5;

    public float Volum = 0.25f;
    int Bgm_Number = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        StartMusic = GameObject.Find("BackGroundMusic");

        backmusic = StartMusic.GetComponent<AudioSource>();//������� �����ص�
        if (backmusic.isPlaying) //��������� ����ǰ� �ִٸ� �н�
        {
            return;
        }
        else
        {
            DontDestroyOnLoad(StartMusic); //������� ��� ���
        }
    }

    void Start()
    {
        DontDestroyOnLoad(StartMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (!backmusic.isPlaying)
        {
            if (SceneManager.GetActiveScene().name != "Main" && SceneManager.GetActiveScene().name != "Option")
            {
                switch (Bgm_Number)
                {
                    case 1:
                        backmusic.PlayOneShot(Bgm_1, Volum);
                        Bgm_Number++;
                        break;
                    case 2:
                        backmusic.PlayOneShot(Bgm_2, Volum);
                        Bgm_Number++;
                        break;
                    case 3:
                        backmusic.PlayOneShot(Bgm_3, Volum);
                        Bgm_Number++;
                        break;
                    case 4:
                        backmusic.PlayOneShot(Bgm_4, Volum);
                        Bgm_Number = 1;
                        break;
                }
            }
            else if (SceneManager.GetActiveScene().name == "Option")
            {
                backmusic.PlayOneShot(Bgm_5, Volum);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            backmusic.Stop(); //����
            Bgm_Number = 1;
        }
    }
}

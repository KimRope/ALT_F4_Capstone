using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchMove : MonoBehaviour
{
    public Transform Player;
    public Transform Scratch;
    Vector3 vel = Vector3.zero;
    Vector2 Start_location;

    public int Scratch_num = 0;

    public float delta_x = 0f; // 좌(우)로 이동가능한 (x)최대값
    public float delta_y = 1.0f; // 상(하)로 이동가능한 (y)최대값
    public float speed = 3.0f; //이동속도

    public bool follow_Player = false;
    // Start is called before the first frame update

    void Start()
    {
        Start_location = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (follow_Player)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Player.position, ref vel, 0.3f);// 카메라를 target으로 이동 (멀면 빠름 & 가까우면 느려짐)
        }
        if (Player.GetComponent<PlayerManager>().Scratch_get == 0 && follow_Player) //스크래치 안잡음 + 스크래치 따라옴 => 스크래치 안따라오고 제자리로
        {
            follow_Player = false;
            transform.position = Start_location;
        }
        if (!follow_Player)
        {
            Vector2 v = Start_location;
            v.x += delta_x * Mathf.Sin(Time.time * speed);
            v.y += delta_y * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }

        Scratch_Set();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player.GetComponent<PlayerManager>().Scratch_get == 0) //플레이어와 부딛침 + 스크래치 안잡음
        {
            Player.GetComponent<PlayerManager>().Scratch_get = Scratch_num;
            follow_Player = true;
        }
    }

    void Scratch_Set()
    {
        switch (Scratch_num)
        {
            case 1:
                if (Scratch.GetComponent<ScratchManager>().foundScratch_1)
                {
                    Destroy(gameObject);
                }
                break;

            case 2:
                if (Scratch.GetComponent<ScratchManager>().foundScratch_2)
                {
                    Destroy(gameObject);
                }
                break;

            case 3:
                if (Scratch.GetComponent<ScratchManager>().foundScratch_3)
                {
                    Destroy(gameObject);
                }
                break;

            case 4:
                if (Scratch.GetComponent<ScratchManager>().foundScratch_4)
                {
                    Destroy(gameObject);
                }
                break;

            case 5:
                if (Scratch.GetComponent<ScratchManager>().foundScratch_5)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }


}

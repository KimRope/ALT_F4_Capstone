using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Rigidbody2D Player_rigid;
    public bool Left_hit = false;
    public bool Right_hit = false;

    public bool Electro = false;

    SpriteRenderer Player_spr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") //플레이어 피격시
        {
            float JumpPower = GameObject.Find("Player").GetComponent<PlayerMove>().JumpPower; //플레이어 PlayerMove(C#)에서 JumpPower 변수 가져옴
            float MoveSpeed = GameObject.Find("Player").GetComponent<PlayerMove>().MoveSpeed; //..
            float isRight = GameObject.Find("Player").GetComponent<PlayerMove>().isRight; //..
            Player_rigid = other.gameObject.GetComponent<Rigidbody2D>();
            GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop = true; //플레이어 피격시간동안 못움직임
            
            
            


            if (Electro) //Electro 켜저있으면 감전 
            {
                Player_rigid.velocity = new Vector2(0, 0);
                Player_spr = other.gameObject.GetComponent<SpriteRenderer>();
                Player_spr.color = new Color32(0, 0, 255, 255);
                PlayerMoveStop(2); //플레이어 2초 정지
                GameObject.Find("Player").GetComponent<PlayerManager>().ElectricShockParticle();
            }
            else //(else)아니면 튕겨져나감
            {
                if (Left_hit && !Right_hit) //왼쪽공격 (만 켜져있으면)
                {
                    isRight = 1;
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower/3); //플레이어 왼쪽으로 날라감
                }
                else if (Right_hit && !Left_hit) //오른쪽공격 (만 켜져있으면)
                {
                    isRight = -1;
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower / 3); //플레이어 왼쪽으로 날라감
                }
                else
                {
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower / 3); //플레이어 반대방향 날라감
                }
                Player_spr = other.gameObject.GetComponent<SpriteRenderer>();
                Player_spr.color = new Color32(255, 150, 150, 255);
                GameObject.Find("Player").GetComponent<PlayerManager>().createHitParticle();
                PlayerMoveStop(1); //플레이어 1초 정지
            }
        }
    }
    void PlayerMoveStop(float StopTime)
    {
        if (GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop_Time < StopTime) //플레이어 현재 정지시간이 ?초 이하면
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop_Time = StopTime; //다시 ?초 정지시킴
        }
    }
}

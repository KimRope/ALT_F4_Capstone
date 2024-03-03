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
        if (other.gameObject.tag == "Player") //�÷��̾� �ǰݽ�
        {
            float JumpPower = GameObject.Find("Player").GetComponent<PlayerMove>().JumpPower; //�÷��̾� PlayerMove(C#)���� JumpPower ���� ������
            float MoveSpeed = GameObject.Find("Player").GetComponent<PlayerMove>().MoveSpeed; //..
            float isRight = GameObject.Find("Player").GetComponent<PlayerMove>().isRight; //..
            Player_rigid = other.gameObject.GetComponent<Rigidbody2D>();
            GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop = true; //�÷��̾� �ǰݽð����� ��������
            
            
            


            if (Electro) //Electro ���������� ���� 
            {
                Player_rigid.velocity = new Vector2(0, 0);
                Player_spr = other.gameObject.GetComponent<SpriteRenderer>();
                Player_spr.color = new Color32(0, 0, 255, 255);
                PlayerMoveStop(2); //�÷��̾� 2�� ����
                GameObject.Find("Player").GetComponent<PlayerManager>().ElectricShockParticle();
            }
            else //(else)�ƴϸ� ƨ��������
            {
                if (Left_hit && !Right_hit) //���ʰ��� (�� ����������)
                {
                    isRight = 1;
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower/3); //�÷��̾� �������� ����
                }
                else if (Right_hit && !Left_hit) //�����ʰ��� (�� ����������)
                {
                    isRight = -1;
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower / 3); //�÷��̾� �������� ����
                }
                else
                {
                    Player_rigid.velocity = new Vector2(MoveSpeed * 7 * -isRight, -JumpPower / 3); //�÷��̾� �ݴ���� ����
                }
                Player_spr = other.gameObject.GetComponent<SpriteRenderer>();
                Player_spr.color = new Color32(255, 150, 150, 255);
                GameObject.Find("Player").GetComponent<PlayerManager>().createHitParticle();
                PlayerMoveStop(1); //�÷��̾� 1�� ����
            }
        }
    }
    void PlayerMoveStop(float StopTime)
    {
        if (GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop_Time < StopTime) //�÷��̾� ���� �����ð��� ?�� ���ϸ�
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop_Time = StopTime; //�ٽ� ?�� ������Ŵ
        }
    }
}

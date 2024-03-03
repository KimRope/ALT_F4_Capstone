using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Transform target_Camera; //ī�޶� translate ��
    public float Speed = 0.2f;
    public float Under = 8f;
    public float Pl_Ca_interval_y;

    public bool isBossTouch; //�������� ��������

    Vector2 target_Under;//ī�޶󺸴� �ణ �Ʒ�
    Vector3 vel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerSpawn();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isBossTouch = true;
            Invoke("PlayerDeathOver", 1f);
            
        }
    }
    void PlayerDeathOver() //�÷��̾� ���� ��ƼŬ ����
    {
        transform.position = new Vector2(0, -10);   //���� ��ġ ����
    }
    void Move()
    {
        if (!isBossTouch)
        {
            target_Under = new Vector2(0f, target_Camera.transform.position.y - Under);
            if (transform.position.y < target_Under.y)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target_Camera.position, ref vel, 0.5f);// ī�޶� target���� �̵� (�ָ� ���� & ������ ������)
                transform.position = new Vector3(0f, transform.position.y, 0f);
            }
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
    }
    void PlayerSpawn()
    {
        //�÷��̾� - ī�޶� y�� ����
        Pl_Ca_interval_y = Mathf.Abs(GameObject.Find("Main Camera").transform.position.y - GameObject.Find("Player").transform.position.y);

        if (isBossTouch && Pl_Ca_interval_y < 5f)
        {
            //GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop = false; //'�÷��̾ Enemy �ǰݽ� ����' ����
            isBossTouch = false; //�������� �������� ����
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Transform target_Camera; //카메라 translate 값
    public float Speed = 0.2f;
    public float Under = 8f;
    public float Pl_Ca_interval_y;

    public bool isBossTouch; //보스에게 잡힌상태

    Vector2 target_Under;//카메라보다 약간 아래
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
    void PlayerDeathOver() //플레이어 죽음 파티클 이후
    {
        transform.position = new Vector2(0, -10);   //보스 위치 조정
    }
    void Move()
    {
        if (!isBossTouch)
        {
            target_Under = new Vector2(0f, target_Camera.transform.position.y - Under);
            if (transform.position.y < target_Under.y)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target_Camera.position, ref vel, 0.5f);// 카메라를 target으로 이동 (멀면 빠름 & 가까우면 느려짐)
                transform.position = new Vector3(0f, transform.position.y, 0f);
            }
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
        }
    }
    void PlayerSpawn()
    {
        //플레이어 - 카메라 y축 간격
        Pl_Ca_interval_y = Mathf.Abs(GameObject.Find("Main Camera").transform.position.y - GameObject.Find("Player").transform.position.y);

        if (isBossTouch && Pl_Ca_interval_y < 5f)
        {
            //GameObject.Find("Player").GetComponent<PlayerMove>().MoveStop = false; //'플레이어가 Enemy 피격시 정지' 제거
            isBossTouch = false; //보스에게 잡힘상태 제거
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject Player;
    public Transform Spawn;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerMove>().MoveStop_Time = 0f;//멈추는 시간 초기화
            Instantiate(Player.GetComponent<PlayerManager>().DeathParticle, Player.transform.position, Player.transform.rotation); //죽을때 이팩트 생성
            Player.SetActive(false); //플레이어 모습 감춤
            Invoke("PlayerDeathOver", 1f); //죽고 1초 뒤
        }
    }
    void PlayerDeathOver() //플레이어 죽음 파티클 이후
    {
        Player.transform.position = Spawn.position; //플레이어 스폰위치 이동
        Player.GetComponent<PlayerManager>().Scratch_get = 0;
        Player.SetActive(true); //플레이어 모습 다시 보이게
    }
    
}

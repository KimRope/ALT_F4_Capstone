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
            Player.GetComponent<PlayerMove>().MoveStop_Time = 0f;//���ߴ� �ð� �ʱ�ȭ
            Instantiate(Player.GetComponent<PlayerManager>().DeathParticle, Player.transform.position, Player.transform.rotation); //������ ����Ʈ ����
            Player.SetActive(false); //�÷��̾� ��� ����
            Invoke("PlayerDeathOver", 1f); //�װ� 1�� ��
        }
    }
    void PlayerDeathOver() //�÷��̾� ���� ��ƼŬ ����
    {
        Player.transform.position = Spawn.position; //�÷��̾� ������ġ �̵�
        Player.GetComponent<PlayerManager>().Scratch_get = 0;
        Player.SetActive(true); //�÷��̾� ��� �ٽ� ���̰�
    }
    
}

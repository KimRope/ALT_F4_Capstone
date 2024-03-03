using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    private GameObject contactPlatform;
    private Vector3 platformPosition;
    private Vector3 distance;
    public GameObject Boss;

    public GameObject HitParticle;
    public ParticleSystem ElectricParticle;
    public GameObject DeathParticle;
    public AudioSource mySFX;
    public AudioClip HitSound;
    public AudioClip ElectricSound;
    public int Scratch_get = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10) // -10밑으로 떨어지면 위치 초기화
        {
            GetComponent<PlayerMove>().MoveStop_Time = 0f;//멈추는 시간 초기화
            Instantiate(GetComponent<PlayerManager>().DeathParticle, transform.position, transform.rotation); //죽을때 이팩트 생성
            this.gameObject.SetActive(false); //플레이어 모습 감춤
            Invoke("PlayerDeathOver", 1f); //죽고 1초 뒤
 
        }
    }

    void PlayerDeathOver() //플레이어 죽음 파티클 이후
    {
        transform.position = GameObject.Find("ReSpawn").transform.position; //플레이어 스폰위치 이동
        GetComponent<PlayerManager>().Scratch_get = 0;
        this.gameObject.SetActive(true); //플레이어 모습 다시 보이게
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Portal")) //포탈(태그)에 부딛치면 다음씬으로 이동
        {
            if (SceneManager.GetActiveScene().name == "Stage1_1")
            {
                PlayerPrefs.SetInt("Load_int", 2);
                SceneManager.LoadScene("Stage1_2");
            }
            else if (SceneManager.GetActiveScene().name == "Stage1_2")
            {
                PlayerPrefs.SetInt("Load_int", 3);
                SceneManager.LoadScene("Stage1_3");
            }
            else if (SceneManager.GetActiveScene().name == "Stage1_3")
            {
                PlayerPrefs.SetInt("Load_int", 4);
                SceneManager.LoadScene("Stage2_1");
            }   
            else if (SceneManager.GetActiveScene().name == "Stage2_1")
            {
                PlayerPrefs.SetInt("Load_int", 5);
                SceneManager.LoadScene("Stage2_2");
            }  
            else if (SceneManager.GetActiveScene().name == "Stage2_2")
            {
                SceneManager.LoadScene("Stage_END");
            }  
        }

        Transform feetPos = GameObject.Find("Player").GetComponent<PlayerMove>().feetPos;
        LayerMask GroundLayer = GameObject.Find("Player").GetComponent<PlayerMove>().GroundLayer;
        if (collision.gameObject.tag == "Platform" && Physics2D.OverlapCircle(feetPos.position, 0.3f, GroundLayer)) //이동발판에 닿고있으면
        {

            //접촉한 순간의 오브젝트 위치를 저장
            contactPlatform = collision.gameObject;
            platformPosition = contactPlatform.transform.position;
            //접촉한 순간의 오브젝트 위치와 캐릭터 위치의 차이를 distance에 저장
            distance = platformPosition - transform.position;
        }
        
    }
    public void createHitParticle()
    {
        Instantiate(HitParticle, transform.position, transform.rotation);
        mySFX.PlayOneShot(HitSound, 0.5f);

    }

    public void ElectricShockParticle()
    {
        ElectricParticle.Play();
        mySFX.PlayOneShot(ElectricSound, 0.5f);
    }
    public void ElectricSoundStop()
    {
        mySFX.Stop();
    }

}

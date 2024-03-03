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
        if (transform.position.y < -10) // -10������ �������� ��ġ �ʱ�ȭ
        {
            GetComponent<PlayerMove>().MoveStop_Time = 0f;//���ߴ� �ð� �ʱ�ȭ
            Instantiate(GetComponent<PlayerManager>().DeathParticle, transform.position, transform.rotation); //������ ����Ʈ ����
            this.gameObject.SetActive(false); //�÷��̾� ��� ����
            Invoke("PlayerDeathOver", 1f); //�װ� 1�� ��
 
        }
    }

    void PlayerDeathOver() //�÷��̾� ���� ��ƼŬ ����
    {
        transform.position = GameObject.Find("ReSpawn").transform.position; //�÷��̾� ������ġ �̵�
        GetComponent<PlayerManager>().Scratch_get = 0;
        this.gameObject.SetActive(true); //�÷��̾� ��� �ٽ� ���̰�
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Portal")) //��Ż(�±�)�� �ε�ġ�� ���������� �̵�
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
        if (collision.gameObject.tag == "Platform" && Physics2D.OverlapCircle(feetPos.position, 0.3f, GroundLayer)) //�̵����ǿ� ���������
        {

            //������ ������ ������Ʈ ��ġ�� ����
            contactPlatform = collision.gameObject;
            platformPosition = contactPlatform.transform.position;
            //������ ������ ������Ʈ ��ġ�� ĳ���� ��ġ�� ���̸� distance�� ����
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

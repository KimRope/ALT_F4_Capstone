using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    SpriteRenderer TB_spr;
    SpriteRenderer spr;
    public GameObject TwoBit16_EG;
    float GraphicEGCount = 3f; //�̽��Ϳ��� ���۽ð�
    float GraphicEGCounter = 3f; //�̽��Ϳ��� �پ��� �ð�
    bool GraphicEG_ON;
    bool GraphicEG_YELLOW;
    float GraphicEG_YEL_Count = 0.15f; //��� �̽��Ϳ��� ���۽ð�
    float GraphicEG_YEL_Counter = 0.15f; //��� �̽��Ϳ��� �پ��� �ð�
    bool SpeedGround_Contact;
    // Start is called before the first frame update
    void Start()
    {
        TB_spr = TwoBit16_EG.GetComponent<SpriteRenderer>();
        spr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        TwoBitFlip_x();
        
    }
    private void Update()
    {
        GraphicEGON();
        GraphicEG();
    }
    // Update is called once per frame
    void TwoBitFlip_x() //2��Ʈ �̽��Ϳ��� X�� ������Ŵ
    {
        float isRight = this.GetComponent<PlayerMove>().isRight;
        Vector3 scale = TwoBit16_EG.transform.localScale;

        scale.x = Mathf.Abs(scale.x) * isRight;
        TwoBit16_EG.transform.localScale = scale;
    }

    void GraphicEGON()
    {
        float Input_x = this.GetComponent<PlayerMove>().Input_x;
        if (SpeedGround_Contact)
        {
            if (Input_x == 0 && !GraphicEG_ON)
            {
                GraphicEGCounter -= Time.deltaTime;
                if (GraphicEGCounter < 0)
                {
                    GraphicEG_ON = true;
                }
            }
            else
            {
                GraphicEGCounter = GraphicEGCount;
            }
        }
    }

    void GraphicEG()
    {
        if (GraphicEG_ON)
        {
            if (!GraphicEG_YELLOW && GraphicEG_YEL_Counter < 0)
            {
                spr.color = new Color32(255, 255, 0, 255);
                GraphicEG_YEL_Counter = GraphicEG_YEL_Count;
                GraphicEG_YELLOW = true;
            }
            else if(GraphicEG_YELLOW && GraphicEG_YEL_Counter < 0)
            {
                spr.color = new Color32(255, 255, 255, 255);
                GraphicEG_YEL_Counter = GraphicEG_YEL_Count;
                GraphicEG_YELLOW = false;
            }
            GraphicEG_YEL_Counter -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CPUEG")) //CPU �̽��Ϳ��� ������ ����
        {
            TwoBit16_EG.SetActive(true);
            TB_spr.color = new Color32(255, 255, 255, 255);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CPUEG")) //CPU �̽��Ϳ��� ������ �Ⱥ���
        {
            TB_spr.color = new Color32(255, 255, 255, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SpeedGround"))
        {
            SpeedGround_Contact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("SpeedGround"))
        {
            SpeedGround_Contact = false;
            GraphicEG_ON = false;
            GraphicEG_YELLOW = false;
            spr.color = new Color32(255, 255, 255, 255);
            GraphicEGCounter = GraphicEGCount;
            GraphicEG_YEL_Counter = GraphicEG_YEL_Count;
        }
    }
}

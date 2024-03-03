using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    
    public float JumpPower = 8; //���� ��
    public float MoveSpeed = 5;
    bool isGrounded = false; //Ground [Layer] �ν�
    bool isWallFlip; //Wall [Layer] �ν�
    bool isWallJump; //�� ���� �ν�
    public bool isJump = false; //���� ���� �ν�
    public bool isJumping = false; //���߿� �� ����
    bool isSpeedMode; //���ǵ��� �ν�
    public bool MoveStop; //Move ����
    public float MoveStop_Time; //Move �����Ǵ� �ð�
    public int JumpCount = 2; //���� ī��Ʈ
    public float isRight = 1f; //������ 1f, ���� -1f
    public float Input_x; //�¿� ���� �� üũ
    public ParticleSystem FeetDust;
    public ParticleSystem FrontDust;

    public Transform feetPos; //�� ����
    public Transform frontPos; //�պκ� ����
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public LayerMask SpeedGroundLayer;

    public AudioSource mySFX;
    public AudioClip Jump_1;
    public AudioClip Jump_2;
    public AudioClip WallSound;

    SpriteRenderer spr;

    private float JumpTimeCounter; //�����ð� ī����
    public float jumpTime; //JumpTimeCounter �ʱ�ȭ �����ִ� ����

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //Rigidbody ������Ʈ ����
        ani = GetComponent<Animator>(); //animator �θ�(��)
        spr = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        int Cheat = PlayerPrefs.GetInt("Cheat_Int");

        if (Cheat == 1) //ġƮ ����
        {
            CheatMove();
        }
        else //ġƮ ��Ŵ
        {
            Move();
            if (isWallFlip)
            {
                createFrontDust(); //���� ��ƼŬ
            }
        }
    }
    private void Update()
    {
        int Cheat = PlayerPrefs.GetInt("Cheat_Int");
        if (Cheat == 1) //ġƮ ����
        {

        }
        else
        {

            //�� animation
            ani.SetFloat("move", Mathf.Abs(Input_x));
            ani.SetBool("walltf", isWallFlip);
            ani.SetBool("jumptf", isJump);
            ani.SetBool("hittf", MoveStop);


            //OverlapCicle(A, 0.3f, B); => A�߽����� [0.3f �������� ���� ��] ��ŭ B�� ������ ��������� true
            isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.23f, GroundLayer); //[Ground�� feetPos�� ������] isGrounded = true (���)
            if (isGrounded)
            {
                JumpCount = 2;
            }
            isWallFlip = Physics2D.OverlapCircle(frontPos.position, 0.25f, WallLayer); //[Wall�� frontPos ������] isWallFlip = true (���)


            SpdMode();
            Input_Key();
            WallJump();
            Jump();

            SoundSFX();

            if (MoveStop)
            {
                MoveStop_Time -= Time.deltaTime; //MoveStop�ð����� ����
                if (MoveStop_Time <= 0)
                {
                    transform.GetComponent<PlayerManager>().ElectricSoundStop();
                    spr.color = new Color32(255, 255, 255, 255);
                    MoveStop = false;
                }
            }
        }
    }

    
    void SpdMode()//���ǵ���
    {
        if (Physics2D.OverlapCircle(feetPos.position, 0.23f, SpeedGroundLayer)) //[SpeedGround�� ���鿡 ������]
        {
            isGrounded = false;
            isSpeedMode = true;
            MoveSpeed = 10f;//MoveSpeed�� 10���� ��
            JumpCount = 2;
        }
        else if (isSpeedMode && (isGrounded || isWallFlip)) //���ǵ����� �� & ���� ������
        {
            isSpeedMode = false;
            MoveSpeed = 5f; //�⺻ 5�� ����
        }
    }
    void Input_Key() //Input_x (�¿� Ű ����) �ν�
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Input_x = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Input_x = -1f;
        }
        else { Input_x = 0; }
    }
    void Move()//Move
    {
        if (Input_x != 0 && !isWallJump &&!MoveStop) //[Ű �Է���] and [������ ��������] and [������ ������ false]�̸� <=> Move�� �����ϴ� ��Ұ� ������
        {
            Vector3 scale = transform.localScale;
            rigid.velocity = new Vector2(Input_x * MoveSpeed, rigid.velocity.y);
            if ((Input_x > 0 && isRight < 0) || (Input_x < 0 && isRight > 0))    //Ű�Է°� ���¹����� �ݴ��ϰ��
            {
                if(!isJumping)//������ �¿� ����� ��������Ŵ
                {
                    createFeetDust();
                }
                scale.x = Mathf.Abs(scale.x) * -isRight;    //scale.x�� �ݴ�������� ����
                transform.localScale = scale;   //ĳ���Ͱ� �ݴ���� �ٶ�
                isRight = -isRight;
            }
        }
    }

    void Jump()//Jump
    {
        if (!isGrounded && !isSpeedMode && !isJump && !isJumping) //���� �ȴ���ְ� ���߻��°� �ƴϿ��ٸ�
        {
            isJumping = true;
            JumpCount--;
        }
        else if (isGrounded || isSpeedMode)
        {
            isJumping = false;
        }

        if (!isWallJump && !MoveStop)
        {
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)   //JumpCount�� �����ϸ�
            {
                createFeetDust();
                isJump = true; //�������� true
                Invoke("JumpCountDown", 0.05f);
                JumpTimeCounter = jumpTime;

                JumpSound();
                //rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);//���������� �ö󰡰���       
            }
            if (Input.GetKey(KeyCode.Space) && isJump) //�������°� true�̸�
            {
                isJumping = true; //���߻��� true
                if (JumpTimeCounter > 0) //JumpTimeCounter�� 0�� �Ǳ� ������ ���οö� <=> hold to 'Jump' higher
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);
                    JumpTimeCounter -= Time.deltaTime;
                }
                else //JumpTImeCounter�� 0�� �Ǹ� isJump�� false
                {
                    isJump = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space)) //����Ű ���� isJump�� false
            {
                isJump = false;
            }

            
        }
    }

    void WallJump()
    {
        if (isWallFlip){ //��Ÿ�� �߿��� �������� and �� ���� �ʱ�ȭ
            JumpCount = 0;
            isJump = false;
            isWallJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y / 2); //���ϼӵ� ������ ����
        }


        if (Input.GetKeyDown(KeyCode.Space) && isWallFlip) //��Ÿ���� ����Ű�� ������ [�� ����]
        {
            FrontDust.Stop();
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -isRight;
            transform.localScale = scale;
            isWallFlip = false;
            isWallJump = true; //������ ���� �ν�
            Invoke("FreezeX", 0.05f); //Invoke("A", 1f); => A�Լ��� 1�� �ڿ� ȣ��
            rigid.velocity = new Vector2(MoveSpeed *3* -isRight, JumpPower*2);
            isRight = -isRight;
            mySFX.PlayOneShot(Jump_2);
        }
    }
    void FreezeX()
    { //isWallJump�� false�Ǳ� ������ Move����
        isWallJump = false;
    }
    void JumpCountDown()
    {
        JumpCount--;
    }
    

    void createFeetDust()
    {
        FeetDust.Play();
    }

    void createFrontDust()
    {
        FrontDust.Play();
    }

    void SoundSFX()
    {
        if (isWallFlip)
        {
            mySFX.PlayOneShot(WallSound, 0.1f);
        }
    }

    void JumpSound()
    {
        if(JumpCount > 1)
        {
            mySFX.PlayOneShot(Jump_1);
        }
        else if(JumpCount == 1){
            mySFX.PlayOneShot(Jump_2);
        }
    }


    void CheatMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Input_x = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Input_x = -1f;
        }
        else { Input_x = 0; }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid.velocity = new Vector2(rigid.velocity.y, MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rigid.velocity = new Vector2(rigid.velocity.y, -MoveSpeed);
        }

        Vector3 scale = transform.localScale;
        rigid.velocity = new Vector2(Input_x * MoveSpeed, rigid.velocity.y);
        if ((Input_x > 0 && isRight < 0) || (Input_x < 0 && isRight > 0))    //Ű�Է°� ���¹����� �ݴ��ϰ��
        {
            if (!isJumping)//������ �¿� ����� ��������Ŵ
            {
                createFeetDust();
            }
            scale.x = Mathf.Abs(scale.x) * -isRight;    //scale.x�� �ݴ�������� ����
            transform.localScale = scale;   //ĳ���Ͱ� �ݴ���� �ٶ�
            isRight = -isRight;
        }
  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    
    public float JumpPower = 8; //점프 힘
    public float MoveSpeed = 5;
    bool isGrounded = false; //Ground [Layer] 인식
    bool isWallFlip; //Wall [Layer] 인식
    bool isWallJump; //벽 점프 인식
    public bool isJump = false; //점프 누름 인식
    public bool isJumping = false; //공중에 뜬 상태
    bool isSpeedMode; //스피드모드 인식
    public bool MoveStop; //Move 정지
    public float MoveStop_Time; //Move 정지되는 시간
    public int JumpCount = 2; //점프 카운트
    public float isRight = 1f; //오른쪽 1f, 왼쪽 -1f
    public float Input_x; //좌우 누름 힘 체크
    public ParticleSystem FeetDust;
    public ParticleSystem FrontDust;

    public Transform feetPos; //발 감지
    public Transform frontPos; //앞부분 감지
    public LayerMask GroundLayer;
    public LayerMask WallLayer;
    public LayerMask SpeedGroundLayer;

    public AudioSource mySFX;
    public AudioClip Jump_1;
    public AudioClip Jump_2;
    public AudioClip WallSound;

    SpriteRenderer spr;

    private float JumpTimeCounter; //점프시간 카운터
    public float jumpTime; //JumpTimeCounter 초기화 시켜주는 변수

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); //Rigidbody 컴포넌트 받음
        ani = GetComponent<Animator>(); //animator 부름(휘)
        spr = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        int Cheat = PlayerPrefs.GetInt("Cheat_Int");

        if (Cheat == 1) //치트 상태
        {
            CheatMove();
        }
        else //치트 안킴
        {
            Move();
            if (isWallFlip)
            {
                createFrontDust(); //먼지 파티클
            }
        }
    }
    private void Update()
    {
        int Cheat = PlayerPrefs.GetInt("Cheat_Int");
        if (Cheat == 1) //치트 상태
        {

        }
        else
        {

            //휘 animation
            ani.SetFloat("move", Mathf.Abs(Input_x));
            ani.SetBool("walltf", isWallFlip);
            ani.SetBool("jumptf", isJump);
            ani.SetBool("hittf", MoveStop);


            //OverlapCicle(A, 0.3f, B); => A중심으로 [0.3f 반지름을 가진 원] 만큼 B를 감지시 상시적으로 true
            isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.23f, GroundLayer); //[Ground와 feetPos가 닿으면] isGrounded = true (상시)
            if (isGrounded)
            {
                JumpCount = 2;
            }
            isWallFlip = Physics2D.OverlapCircle(frontPos.position, 0.25f, WallLayer); //[Wall에 frontPos 붙으면] isWallFlip = true (상시)


            SpdMode();
            Input_Key();
            WallJump();
            Jump();

            SoundSFX();

            if (MoveStop)
            {
                MoveStop_Time -= Time.deltaTime; //MoveStop시간동안 정지
                if (MoveStop_Time <= 0)
                {
                    transform.GetComponent<PlayerManager>().ElectricSoundStop();
                    spr.color = new Color32(255, 255, 255, 255);
                    MoveStop = false;
                }
            }
        }
    }

    
    void SpdMode()//스피드모드
    {
        if (Physics2D.OverlapCircle(feetPos.position, 0.23f, SpeedGroundLayer)) //[SpeedGround의 지면에 닿으면]
        {
            isGrounded = false;
            isSpeedMode = true;
            MoveSpeed = 10f;//MoveSpeed를 10으로 줌
            JumpCount = 2;
        }
        else if (isSpeedMode && (isGrounded || isWallFlip)) //스피드모드중 땅 & 벽에 닿으면
        {
            isSpeedMode = false;
            MoveSpeed = 5f; //기본 5로 변경
        }
    }
    void Input_Key() //Input_x (좌우 키 누름) 인식
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
        if (Input_x != 0 && !isWallJump &&!MoveStop) //[키 입력중] and [벽점프 해제상태] and [움직임 정지가 false]이면 <=> Move를 방해하는 요소가 없으면
        {
            Vector3 scale = transform.localScale;
            rigid.velocity = new Vector2(Input_x * MoveSpeed, rigid.velocity.y);
            if ((Input_x > 0 && isRight < 0) || (Input_x < 0 && isRight > 0))    //키입력과 보는방향이 반대일경우
            {
                if(!isJumping)//땅에서 좌우 변경시 먼지일으킴
                {
                    createFeetDust();
                }
                scale.x = Mathf.Abs(scale.x) * -isRight;    //scale.x을 반대방향으로 변경
                transform.localScale = scale;   //캐릭터가 반대방향 바라봄
                isRight = -isRight;
            }
        }
    }

    void Jump()//Jump
    {
        if (!isGrounded && !isSpeedMode && !isJump && !isJumping) //땅에 안닿아있고 공중상태가 아니였다면
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
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)   //JumpCount가 존재하면
            {
                createFeetDust();
                isJump = true; //점프상태 true
                Invoke("JumpCountDown", 0.05f);
                JumpTimeCounter = jumpTime;

                JumpSound();
                //rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);//위방향으로 올라가게함       
            }
            if (Input.GetKey(KeyCode.Space) && isJump) //점프상태가 true이면
            {
                isJumping = true; //공중상태 true
                if (JumpTimeCounter > 0) //JumpTimeCounter가 0이 되기 전까지 위로올라감 <=> hold to 'Jump' higher
                {
                    rigid.velocity = new Vector2(rigid.velocity.x, JumpPower);
                    JumpTimeCounter -= Time.deltaTime;
                }
                else //JumpTImeCounter가 0이 되면 isJump를 false
                {
                    isJump = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space)) //점프키 때면 isJump를 false
            {
                isJump = false;
            }

            
        }
    }

    void WallJump()
    {
        if (isWallFlip){ //벽타는 중에는 점프막음 and 벽 점프 초기화
            JumpCount = 0;
            isJump = false;
            isWallJump = false;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y / 2); //낙하속도 반으로 줄임
        }


        if (Input.GetKeyDown(KeyCode.Space) && isWallFlip) //벽타기중 점프키를 누르면 [벽 점프]
        {
            FrontDust.Stop();
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -isRight;
            transform.localScale = scale;
            isWallFlip = false;
            isWallJump = true; //벽에서 점프 인식
            Invoke("FreezeX", 0.05f); //Invoke("A", 1f); => A함수를 1초 뒤에 호출
            rigid.velocity = new Vector2(MoveSpeed *3* -isRight, JumpPower*2);
            isRight = -isRight;
            mySFX.PlayOneShot(Jump_2);
        }
    }
    void FreezeX()
    { //isWallJump가 false되기 전까지 Move정지
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
        if ((Input_x > 0 && isRight < 0) || (Input_x < 0 && isRight > 0))    //키입력과 보는방향이 반대일경우
        {
            if (!isJumping)//땅에서 좌우 변경시 먼지일으킴
            {
                createFeetDust();
            }
            scale.x = Mathf.Abs(scale.x) * -isRight;    //scale.x을 반대방향으로 변경
            transform.localScale = scale;   //캐릭터가 반대방향 바라봄
            isRight = -isRight;
        }
  
    }
}

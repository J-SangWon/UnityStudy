using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 속성")]
    public float Speed = 5;
    public float JumpUp = 5;
    public float Power = 5;
    public Vector3 direction;

    Animator pAni;
    Rigidbody2D pRgid2D;
    SpriteRenderer SP;
    public GameObject Slash;

    //그림자
    public GameObject Shadow1;
    List<GameObject> sh = new List<GameObject>();

    //바닥먼지
    public GameObject Jumpdust;

    public GameObject Hit_Lazer;

    //벽점프
    public Transform wallchk;
    public float wallchkDistance;
    public LayerMask wLayer;
    bool isWall;
    public float slidingSpeed;
    public float wallJumpPower;
    public bool isWallJump;
    float isRight = 1;

    void Start()
    {
        pAni = GetComponent<Animator>();
        pRgid2D = GetComponent<Rigidbody2D>();
        SP = GetComponent<SpriteRenderer>();
        direction = Vector2.zero;
    }

    void KeyInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");

        if (direction.x < 0)
        {
            //left
            SP.flipX = true;
            pAni.SetBool("Run", true);

            for(int i = 0; i <sh.Count; i++)
            {
                sh[i].GetComponent<SpriteRenderer>().flipX = SP.flipX;
            }
        }
        else if (direction.x > 0)
        {
            //right
            SP.flipX = false;
            pAni.SetBool("Run", true);

            for (int i = 0; i < sh.Count; i++)
            {
                sh[i].GetComponent<SpriteRenderer>().flipX = SP.flipX;
            }
        }
        else if(direction.x == 0)
        {
            pAni.SetBool("Run",false);

            for(int i = 0; i < sh.Count; i++)
            {
                Destroy(sh[i]); //게임오브젝트 지우기
                sh.RemoveAt(i); //게임오브젝트 관리하는 리스트 지우기
            }
 
        }
        if (Input.GetMouseButtonDown(0))//0은 좌클릭
        {
            pAni.SetTrigger("Attack");
            Lazer();
        }


    }

    public void Move()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }
    public void Jump()
    {
        pRgid2D.linearVelocity = Vector2.zero;
        pRgid2D.AddForce(new Vector2(0, JumpUp),ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        Debug.DrawRay(pRgid2D.position, Vector3.down, new Color(0, 1, 0));

        //레이캐스트로 땅 체크
        RaycastHit2D rayHit = Physics2D.Raycast(pRgid2D.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (pRgid2D.linearVelocityY < 0)
        {
            if (rayHit.collider != null)
            {
                if(rayHit.distance < 0.7f)
                {
                    pAni.SetBool("Jump",false );
                }
            }
        }
        ////계단 체크
        //RaycastHit2D rayHitStair = Physics2D.Raycast(pRgid2D.position, Vector3.down, 1, LayerMask.GetMask("Stairs"));
        //if (pRgid2D.linearVelocityY < 0)
        //{
        //    if (rayHitStair.collider != null)
        //    {
        //        pRgid2D.gravityScale = 0.1f;
        //        pAni.SetBool("Jump", false);
        //    }
        //    else
        //    {
        //        pRgid2D.gravityScale = 1f;
        //    }
        //}

    }

    public void AttackSlash()
    {
        if(SP.flipX == false)
        {
            //플레이어 오른쪽
            pRgid2D.AddForce(Vector2.right * Power, ForceMode2D.Impulse);
            GameObject go = Instantiate(Slash, transform.position,Quaternion.identity);
            //go.GetComponent<SpriteRenderer>().flipX = SP.flipX;
        }
        else
        {
            pRgid2D.AddForce(Vector2.left * Power, ForceMode2D.Impulse);
            GameObject go = Instantiate(Slash, transform.position, Quaternion.identity);
            //go.GetComponent<SpriteRenderer>().flipX = SP.flipX;
        }
    }

    //그림자
    public void RunShadow()
    {
        if (sh.Count < 6)
        {
            GameObject go = Instantiate(Shadow1, transform.position, Quaternion.identity);
            go.GetComponent<Shadow>().TwSpeed = 10 - sh.Count;
            sh.Add(go);
        }
    }

    public void Lazer()
    {
        Instantiate(Hit_Lazer, transform.position, Quaternion.identity);
    }

    public void RandDust(GameObject dust)
    {
        Instantiate(dust, transform.position + new Vector3(-0.2f, -0.4f,0), Quaternion.identity);
    }
    public void JumpDust()
    {
        Instantiate(Jumpdust, transform.position , Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(wallchk.position, Vector2.right * isRight * wallchkDistance);
    }

    void Update()
    {
        if (!isWallJump)
        {
            KeyInput();
            Move();
        }

        //벽 체크
        isWall = Physics2D.Raycast(wallchk.position, Vector2.right * isRight, wallchkDistance, wLayer);
        pAni.SetBool("Grab", isWall);

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (pAni.GetBool("Jump") == false)
            {
                Jump();
                pAni.SetBool("Jump", true);
                JumpDust();
            }
        }

        if (isWall)
        {
            isWallJump = false;
            //벽점프상태
            pRgid2D.linearVelocity = new Vector2(pRgid2D.linearVelocityX, pRgid2D.linearVelocityY * slidingSpeed);
            if (Input.GetKeyDown(KeyCode.W))
            {
                isWallJump = true;
                //벽점프 먼지

                Invoke("FreezeX", 0.3f);
                //Velocity를 이용해서 벽점프
                isRight = -isRight;
                pRgid2D.linearVelocity = new Vector2(isRight * wallJumpPower, 0.9f * wallJumpPower);

                SP.flipX = SP.flipX == false ? true : false;
            }
        }
        
    }

    void FreezeX()
    {
        isWallJump = false;
    }


}

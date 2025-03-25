using System.Collections.Generic;
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

    public GameObject Hit_Lazer;
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


    void Update()
    {
        KeyInput();
        Move();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (pAni.GetBool("Jump") == false)
            {
                Jump();
                pAni.SetBool("Jump", true);
                
            }
        }

        
    }


}

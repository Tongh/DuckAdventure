using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    private Animator AnimatorComp;
    private Rigidbody2D Rigidbody2DComp;
    private bool bGameStart = false;
    private bool bIsInAir = false;
    private bool bIsJumpFall = false;
    private bool bIsSlide = false;

    public GameObject SmokePoint;
    public GameObject SmokeEffect;
    public GameObject StarHitEffect;
    public Transform TriggerIsInAir;
    public LayerMask GroundMask;
    public BoxCollider2D BoxColliderComp;
    public CircleCollider2D CircleColliderComp;
    public float MoveSpeed = .02f;
    public float JumpForce = 300;
    public float GravityNormal = 3.0f;
    public float GravityJump = 1.2f;
    public float GroundDistance = .15f;

    private void Awake()
    {
        AnimatorComp = GetComponent<Animator>();
        Rigidbody2DComp = GetComponent<Rigidbody2D>();

        StartCoroutine(CreateSmoke());
    }

    // Start is called before the first frame update
    void Start()
    {
        JumpOff();
    }

    private void FixedUpdate()
    {
        if (bGameStart)
        {
            transform.Translate(new Vector2(MoveSpeed, .0f));

            var RayHitResult = Physics2D.Raycast(TriggerIsInAir.position, Vector2.down, GroundDistance, GroundMask);
            // Debug.DrawLine(TriggerIsInAir.position, TriggerIsInAir.position + Vector3.down * GroundDistance, Color.red);
            if (RayHitResult)
            {
                bIsInAir = false;
                bIsJumpFall = false;
                // Debug.Log(RayHitResult.transform.name);
            }
            else
            {
                bIsInAir = true;
            }
            AnimatorComp.SetBool("bIsInAir", bIsInAir);
            AnimatorComp.SetBool("bIsJumpFall", bIsJumpFall);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpOff();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WalkToSlide();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            SlideToWalk();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            GameManager.Instance.Score++;
            GameManager.Instance.Stars += 10;
            //Debug.Log("Hit Star");
            Instantiate(StarHitEffect, gameObject.transform);
            
            Destroy(other.gameObject);
        }
    }

    public void Play()
    {
        AnimatorComp.SetTrigger("WalkTrigger");
        bGameStart = true;
    }

    public void Jump()
    {
        if (!bIsInAir)
        {
            Rigidbody2DComp.gravityScale = GravityJump;
            Rigidbody2DComp.AddForce(new Vector2(0, JumpForce));
        }
    }

    public void JumpOff()
    {
        Rigidbody2DComp.gravityScale = GravityNormal;
        bIsJumpFall = true;
    }

    public void WalkToSlide()
    {
        bIsSlide = true;
        AnimatorComp.SetBool("bIsSlide", bIsSlide);
        
        BoxColliderComp.enabled = false;
        CircleColliderComp.enabled = true;

    }

    public void SlideToWalk()
    {
        bIsSlide = false;
        AnimatorComp.SetBool("bIsSlide", bIsSlide);
        BoxColliderComp.enabled = true;
        CircleColliderComp.enabled = false;
    }

    IEnumerator CreateSmoke()
    {
        while (true)
        {
            if (!bIsInAir && bIsSlide)
            {
                Instantiate(SmokeEffect, SmokePoint.transform.position, Quaternion.identity);
            }
            
            yield return new WaitForSeconds(0.2f);
        }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator AnimatorComp;
    private Rigidbody2D Rigidbody2DComp;
    private bool bGameStart = false;
    private bool bIsInAir = false;
    private bool bIsJumpFall = false;
    private bool bIsSlide = false;

    public Transform TriggerIsInAir;
    public LayerMask GroundMask;
    public float MoveSpeed = .02f;
    public float JumpForce = 300;
    public float GravityNormal = 3.0f;
    public float GravityJump = 1.2f;
    public float GroundDistance = .1f;

    private void Awake()
    {
        AnimatorComp = GetComponent<Animator>();
        Rigidbody2DComp = GetComponent<Rigidbody2D>();
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
    }

    public void SlideToWalk()
    {
        bIsSlide = false;
        AnimatorComp.SetBool("bIsSlide", bIsSlide);
    }
}

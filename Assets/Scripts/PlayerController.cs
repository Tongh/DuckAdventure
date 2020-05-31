using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator AnimatorComp;
    private Rigidbody2D Rigidbody2DComp;
    private bool bGameStart = false;

    public float MoveSpeed = .02f;
    public float JumpForce = 300;
    public float GravityNormal = 3.0f;
    public float GravityJump = 1.2f;

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
    }

    public void Play()
    {
        AnimatorComp.SetTrigger("WalkTrigger");
        bGameStart = true;
    }

    public void Jump()
    {
        Rigidbody2DComp.gravityScale = GravityJump;
        Rigidbody2DComp.AddForce(new Vector2(0, JumpForce));
    }

    public void JumpOff()
    {
        Rigidbody2DComp.gravityScale = GravityNormal;
    }
}

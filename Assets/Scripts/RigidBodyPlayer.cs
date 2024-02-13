using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    [SerializeField] private bool isGround = false;
    private bool isJump;
    private Vector3 moveDir;
    private Rigidbody rigid;
    private CapsuleCollider cap;

    [Header("플레이어 스텟")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField,Tooltip("마우스 감도")] float mouseSensitvity = 5f;
    private Vector2 rotateValue;

    private Transform trsCam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        trsCam = transform.GetChild(0);
    }

    void Update()
    {
        checkGround();
        moving();
        jumping();
        checkGravity();

        rotation();
    }

    private void checkGround()
    {
        if (rigid.velocity.y < 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }
        else if(rigid.velocity.y > 0)
        {
            isGround = false;
        }
    }

    private void moving()
    {
        //moveDir.x = inputHorizontal();
        //moveDir.y = rigid.velocity.y;
        //moveDir.z = inputVertical();
        //rigid.velocity = transform.rotation * moveDir;
        if (Input.GetKey(KeyCode.W))
        { 
            rigid.AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Force);
        }
        
        if (Input.GetKey(KeyCode.A))
        { 
            rigid.AddForce(new Vector3(-moveSpeed, 0, 0), ForceMode.Force);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(moveSpeed, 0, 0), ForceMode.Force);
        }
    }

    private float inputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }
    private float inputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void jumping()
    {
        if (isGround == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void checkGravity()
    {
        if (isJump)
        {
            isJump = false;
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    private void rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitvity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitvity * Time.deltaTime;

        rotateValue += new Vector2(-mouseY, mouseX);

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(0, rotateValue.y,0));

        trsCam.rotation = Quaternion.Euler(rotateValue.x, rotateValue.y, 0);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float mouseMoveSpeed = 5f;
    private Vector3 rotateValue;

    void Start()
    {
        rotateValue = transform.rotation.eulerAngles;//쿼터니언을 vector3 형변환

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void checkMouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        { 
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;//마우스가 보이지 않도록, 항상 마우스는 화면의 가운데 존재
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void Update()
    {
        moving();//이동기능
        rotating();//회전기능

        checkMouse();
    }

    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=
                transform.forward * mouseMoveSpeed * Time.deltaTime;
            //transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
            //transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
            //Vector3.forward * mouseMoveSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.S)) 
        {
            transform.position +=
            //    -transform.forward * mouseMoveSpeed * Time.deltaTime;
            //transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
            transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
            //    -transform.right * mouseMoveSpeed * Time.deltaTime;
            //transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
            transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D)) 
        {
            transform.position +=
            //    transform.right * mouseMoveSpeed * Time.deltaTime;
            //transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
            transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position +=
               Vector3.up * mouseMoveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.position +=
               Vector3.down * mouseMoveSpeed * Time.deltaTime;
        }

    }

    private void rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);

        //if (rotateValue.x > 90)
        //{
        //    rotateValue.x = 90;
        //}
        //else if (rotateValue.x < -90)
        //{
        //    rotateValue.x = -90;
        //}
        rotateValue.x = Mathf.Clamp(rotateValue.x, -15f, 15f);

        transform.rotation = Quaternion.Euler(rotateValue);
    }
}

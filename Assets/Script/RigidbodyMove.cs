using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMove : MonoBehaviour
{
    Rigidbody rigid;
    [SerializeField]
    private float power = 30f;
    [SerializeField]
    private float jumpPower = 10f;
    private Vector3 moveDir;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        //rigid.AddForce(moveDir * power);
        if (Input.GetButton("Jump"))
        {
                rigid.AddForce(Vector3.up * jumpPower);
        }

        rigid.AddForce((Vector3.right * xMove + Vector3.forward * zMove) * power);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0f;
    [SerializeField]
    protected float jumpForce = 0f;
    [SerializeField]
    protected float gravitySpeed = 0f;
    protected float moveY = 0;

    
    protected CharacterController controller = null;
    protected Vector3 moveDir;
    protected Animator animator;

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    protected virtual void Start()
    {
        speed = 10;
        jumpForce = 20;
        gravitySpeed = 50;
    }

    protected virtual void PlayerJump()
    {
        if (controller.isGrounded)
        {

            if (Input.GetButton("Jump"))
            {
                moveY = jumpForce;
            }
        }
        else
        {
            moveY -= gravitySpeed * Time.deltaTime;
        }
    }
}

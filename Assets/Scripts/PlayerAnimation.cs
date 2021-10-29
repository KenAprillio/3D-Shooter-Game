using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public float speed = 4;
    public float rotSpeed = 80;
    public float rot = 0f;
    public float gravity = 8;
    Animator anim;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
        SwapAk47();
        SwapShotgun();  
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("shot") == true)
                {
                    return;
                } 
                else if (anim.GetBool("shot") == false)
                {
                    anim.SetBool("run", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("run", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0);
            }
            rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
        }
    }

    void GetInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (anim.GetBool("run") == true)
            {
                anim.SetBool("run", false);
                anim.SetInteger("condition", 0);
            }
            if (anim.GetBool("run") == false)
            {
                Attack();
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("shot", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("condition", 0);
        anim.SetBool("shot", false);
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    void SwapAk47()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            anim.SetBool("switch weapon", true);
            anim.SetInteger("condition", 3);
        } else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            anim.SetBool("switch weapon", false);
            anim.SetInteger("condition", 0);
        }
    }

    void SwapShotgun()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            anim.SetBool("switch weapon", true);
            anim.SetInteger("condition", 3);
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            anim.SetBool("switch weapon", false);
            anim.SetInteger("condition", 0);
        }
    }
}

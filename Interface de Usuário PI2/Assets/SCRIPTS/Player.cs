using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;

    public float speed;
    public float gravity;
    public float rotSpeed;

    private float rot;
    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
       controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(controller.isGrounded)
        {
            float speedTemp = speed;
            if (Input.GetKey(KeyCode.LeftShift))
                speedTemp *= 2;

            moveDirection = Input.GetAxis("Vertical") * Vector3.forward * speedTemp;

            if (Input.GetAxis("Vertical") != 0)
                anim.SetInteger("transition", 1);
            else
                anim.SetInteger("transition", 0);
        }

        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * Time.deltaTime);

    }
}

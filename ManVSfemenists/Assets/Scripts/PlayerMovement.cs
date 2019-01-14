using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 6.0f;
    public float RotSpeed = 1.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public bool CanMove, CanRotate;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Rigidbody rigidBody;
    private GameObject PlayerCamera;

    Vector3 PlayerRot;
    Vector3 CamRot;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        PlayerCamera = GetComponentInChildren<Camera>().gameObject;

        // let the gameObject fall down
        this.transform.position += new Vector3(0, 5, 0);
    }

    void Update()
    {
        if (CanMove)
        {
            if (controller.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection = moveDirection * MoveSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if (CanRotate)
        {
            //Find The Amount To Add
            float MouseY = Input.GetAxisRaw("Mouse X");
            float MouseX = Input.GetAxisRaw("Mouse Y");

            //Add The Rotation To the Vector
            PlayerRot += new Vector3(0, MouseY) * RotSpeed;
            CamRot += new Vector3(-MouseX, MouseY) * RotSpeed;

            //Apply the Rotation
            this.transform.rotation = Quaternion.Euler(PlayerRot);
            PlayerCamera.transform.rotation = Quaternion.Euler(CamRot);
        }
    }
}
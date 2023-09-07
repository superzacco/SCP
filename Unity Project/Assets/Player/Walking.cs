using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [Header("Movement")]
    public float defaultMoveSpeed;
    public float moveSpeed;
    public float rbDrag;

    float horizontalMovement;
    float verticalMovement;

    Vector3 moveDirection;

    public Rigidbody rb;

    private void Start()
    {
        moveSpeed = defaultMoveSpeed;
        rb.freezeRotation = true;
    }



    private void Update()
    {
        MyInput();
        Drag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }



    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }

    void Drag()
    {
        rb.drag = rbDrag;
    }
}

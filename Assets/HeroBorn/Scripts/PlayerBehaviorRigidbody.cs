using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorRigidbody : MonoBehaviour
{
    public float moveSpeed = 10f;

    private float verticalInput;
    private float horizontalInput;

    public Rigidbody rb; //assigned in inspector

    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Vector3 targetHorizontalPosition = verticalInput * moveSpeed * Time.fixedDeltaTime * transform.forward;
        Vector3 targetVerticalPosition = horizontalInput * moveSpeed * Time.fixedDeltaTime * transform.right;
        rb.MovePosition (transform.position + targetHorizontalPosition + targetVerticalPosition);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Vector3 CamOffset = new(0, 1.5f, -5);

    private readonly float min = -70;
    private readonly float max = 70;
    private readonly float sensitivity = 10;

    public Transform playerTransform; //assigned in inspector
    public GameObject pirateAnimation; //^

    private Vector3 currentRotation;

    private bool zoomInput;

    private void Update()
    {
        playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        zoomInput = Input.GetButton("Zoom");

        if (Input.GetButtonDown("Zoom"))
            pirateAnimation.SetActive(true);
        else if (Input.GetButtonUp("Zoom"))
            pirateAnimation.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = zoomInput ? playerTransform.TransformPoint(CamOffset) : playerTransform.TransformPoint(0, 1, 0);
        transform.position = Vector3.Lerp(transform.position, targetPosition, .2f);
    }

    private void LateUpdate()
    {
        currentRotation.x += (Input.GetAxis("Mouse X") * sensitivity);
        currentRotation.y = Mathf.Clamp(currentRotation.y - (Input.GetAxis("Mouse Y") * sensitivity), min, max);
        Quaternion targetRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, .2f);
    }
}
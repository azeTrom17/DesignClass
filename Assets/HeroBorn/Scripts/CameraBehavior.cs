using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    private Vector3 CamOffset = new(0, 1.5f, -5);

    public float min;
    public float max;

    private Transform playerTransform;
    private MeshRenderer playerRenderer;
    private Vector3 currentRotation;

    private bool zoomInput;

    public float sensitivity;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GameObject.Find ("Player").transform;
        playerRenderer = playerTransform.GetComponent<MeshRenderer> ();
    }

    private void Update()
    {
        playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        zoomInput = Input.GetButton("Zoom");

        playerRenderer.enabled = Input.GetButton("Zoom");
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
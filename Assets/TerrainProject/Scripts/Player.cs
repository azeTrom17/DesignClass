using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private readonly float moveSpeed = 10;
    private readonly float climbSpeed = 10;

    private float verticalInput;
    private float horizontalInput;

    public Rigidbody rb; //assigned in inspector
    public TMP_Text txt; //assigned in inspector

    private bool hasToggledView;
    private bool climbingLadder;

    private bool bulletInput;
    private bool bulletOnCooldown;
    private readonly float bulletSpeed = 20;
    public ObjectPool objectPool; //assigned in inspector

    private void Start()
    {
        txt.text = "Left click to toggle third person view";
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        bulletInput = Input.GetButton("Shoot");

        if (!hasToggledView && Input.GetButtonDown("Zoom"))
        {
            hasToggledView = true;
            txt.text = "Find something to drink";
        }
    }

    private void FixedUpdate()
    {
        if (climbingLadder)
        {
            rb.velocity = climbSpeed * Vector3.up;
            return;
        }

        Vector3 horizontalSpeed = horizontalInput * moveSpeed * transform.right;
        Vector3 verticalSpeed = verticalInput * moveSpeed * transform.forward;
        rb.velocity = horizontalSpeed + verticalSpeed + new Vector3(0, rb.velocity.y, 0);

        if (bulletInput && !bulletOnCooldown)
        {
            bulletInput = false;
            StartCoroutine(BulletCooldown());

            Bullet newBullet = objectPool.GetPooledInfo().bullet;
            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = transform.position + new Vector3(0, 2, 0);
            newBullet.player = this;
            newBullet.rb.velocity = transform.forward * bulletSpeed;
            StartCoroutine(DestroyBullet(newBullet));
        }
    }

    private IEnumerator BulletCooldown()
    {
        bulletOnCooldown = true;
        yield return new WaitForSeconds(.3f);
        bulletOnCooldown = false;
    }

    private IEnumerator DestroyBullet(Bullet newBullet)
    {
        yield return new WaitForSeconds(3);
        newBullet.rb.velocity = Vector3.zero;
        newBullet.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.name == "EmptyBottle")
                txt.text = "This bottle is empty. Try looking in the tower";
            else if (col.name == "FullBottle")
                txt.text = "You won!!! (and now you're trapped forever. Nice job, genius)";
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.name == "Ladder" && verticalInput > 0)
                climbingLadder = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.name == "Ladder")
                climbingLadder = false;
        }
    }
}
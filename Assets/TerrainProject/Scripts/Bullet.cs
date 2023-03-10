using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb; //assigned in inspector
    [NonSerialized] public Player player;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Food"))
            col.gameObject.SetActive(false);
    }
}
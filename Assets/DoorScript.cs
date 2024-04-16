using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(Animator))]
public class DoorScript : MonoBehaviour
{
    private Collider collisionBox;
    private Animator doorAnimator;
    private void Awake()
    {
        collisionBox = GetComponent<Collider>();
        doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        doorAnimator.SetBool("inDoorRange",true);
    }

    private void OnTriggerExit(Collider other)
    {
        doorAnimator.SetBool("inDoorRange",false);
    }
}

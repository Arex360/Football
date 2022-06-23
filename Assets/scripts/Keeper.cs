using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{
    public float speed;
    private float playerSpeed;
    private Rigidbody rigidbody;
    private Animator animator;
    void Start()
    {
        playerSpeed = speed;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        animator.SetBool("catch", GameManger.instance.ballCaught);
        if (GameManger.instance.ballCaught)
            return;
        rigidbody.MovePosition(this.transform.position + this.transform.right * playerSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("r"))
        {
            print("Entered right");
            playerSpeed = speed;
        }
        else
        {
            print("Entered left");
            playerSpeed = -speed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CharacterController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float rotationSpeed;
    Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(-horizontalInput, 0, -verticalInput);
        rb.velocity = movement * movementSpeed;
        //rb.velocity += Physics.gravity * Time.fixedDeltaTime;
        Vector3 direction = movement.normalized;
        float velocity = rb.velocity.magnitude;
        anim.SetFloat("Speed", velocity);
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed;
    [SerializeField] private float turnSpeed;

    [SerializeField] Rigidbody sphereRigidbody;

    void Start()
    {
        sphereRigidbody.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        verticalInput *= verticalInput > 0 ? forwardSpeed : backwardSpeed;

        transform.position = sphereRigidbody.transform.position;

        float newRotation = horizontalInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);
    }

    private void FixedUpdate(){
        sphereRigidbody.AddForce(transform.forward * verticalInput, ForceMode.Acceleration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    private bool breaking;

    private float driftTime;

    private bool isCarGrounded;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float driftPotential;
    [SerializeField] private float driftForce;

    [SerializeField] private float airDrag;
    [SerializeField] private float groundDrag;

    [SerializeField] private LayerMask groundLayer;

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
        breaking = Input.GetKey(KeyCode.Space);

        verticalInput *= verticalInput > 0 ? forwardSpeed : backwardSpeed;
        if(breaking){
            verticalInput = 0;
            horizontalInput *= driftForce * driftTime/driftPotential;
            driftTime -= Time.deltaTime;
            driftTime = driftTime < 0 ? 0 : driftTime;
        } else if(driftTime < driftPotential){
            driftTime += Time.deltaTime;
        }
        transform.position = sphereRigidbody.transform.position;
        if(driftTime > 0){
            float newRotation = horizontalInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            transform.Rotate(0, newRotation, 0, Space.World);
        }

        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);
        sphereRigidbody.drag = isCarGrounded ? groundDrag : airDrag;

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
    }

    private void FixedUpdate() {

        if(isCarGrounded){
            sphereRigidbody.AddForce(transform.forward * verticalInput, ForceMode.Acceleration);
        } else {
            sphereRigidbody.AddForce(transform.up * -9.8f);
        }
    }
}

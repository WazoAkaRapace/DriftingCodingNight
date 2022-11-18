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
    private bool fastModeEnabled;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float driftPotential;
    [SerializeField] private float driftForce;

    [SerializeField] private float airDrag;
    [SerializeField] private float groundDrag;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] Rigidbody sphereRigidbody;

    private TrailRenderer[] trailRenderers;

    void Start()
    {
        trailRenderers = GetComponentsInChildren<TrailRenderer>();
        sphereRigidbody.transform.parent = null;
    }

    public void EnableFastMode(){
        fastModeEnabled = true;
        foreach(TrailRenderer tr in trailRenderers){
            tr.emitting = true;
        }
    }

    public void DisableFastMode(){
        fastModeEnabled = false;
        foreach(TrailRenderer tr in trailRenderers){
            tr.emitting = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        breaking = Input.GetKey(KeyCode.Space);

        verticalInput *= verticalInput > 0 ? fastModeEnabled ? forwardSpeed : forwardSpeed / 3 : backwardSpeed;
        if(breaking && fastModeEnabled) {
            verticalInput = 0;
            horizontalInput *= driftForce * driftTime/driftPotential;
            driftTime -= Time.deltaTime;
            driftTime = driftTime < 0 ? 0 : driftTime;
        } else if(driftTime < driftPotential) {
            driftTime += Time.deltaTime;
        }
        transform.position = sphereRigidbody.transform.position;
        if(driftTime > 0) {
            float newRotation = horizontalInput * (fastModeEnabled ? turnSpeed : turnSpeed / 3) * Time.deltaTime * Input.GetAxisRaw("Vertical");
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

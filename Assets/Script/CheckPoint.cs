using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckPoint : MonoBehaviour
{

    [SerializeField] Transform emmiter1Transform;
    [SerializeField] Transform emmiter2Transform;

    public Action onReach;

    private float angle = 0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setCallbackAction(Action callback){
        onReach += callback;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        emmiter1Transform.position = transform.position + new Vector3(1.5F * Mathf.Sin(angle), 3, 1.5F*Mathf.Cos(angle));
        emmiter2Transform.position = transform.position + new Vector3(1.5F * Mathf.Sin(angle + Mathf.PI), 3, 1.5F*Mathf.Cos(angle + Mathf.PI));

        angle += 0.05F;
    }
    
    private void OnTriggerEnter(Collider other){
        onReach();
        Destroy(gameObject);
    }


}

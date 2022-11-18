using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCarAnimation : MonoBehaviour
{

    [SerializeField] float rotationSpeed;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0), Space.World);
        transform.position = new Vector3(0, Mathf.Sin(time)/3, 0);
        time += Time.deltaTime;
    }
}

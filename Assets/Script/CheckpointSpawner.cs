using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnRandomCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomCheckpoint(){
        GameObject checkpoint = Instantiate(checkpointPrefab, new Vector3(Random.Range(-50, 50) , 0.5F, Random.Range(-50, 50)), Quaternion.identity);
        CheckPoint script = checkpoint.GetComponent<CheckPoint>();
        script.setCallbackAction((SpawnRandomCheckpoint));
    }
}

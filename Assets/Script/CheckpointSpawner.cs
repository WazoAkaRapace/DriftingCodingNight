using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointPrefab;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private float resetTime;

    [SerializeField] private CarController carController;

    private bool started = false;

    private float remainingTime;

    private GameObject lastCheckPoint;
    public GameObject[] checkpointSpawns;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = resetTime;
        SpawnRandomCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(started){
            remainingTime -= Time.deltaTime;
            remainingTime = remainingTime < 0 ? 0 : remainingTime;
            scoreManager.UpdateTimer(remainingTime);
        }
        if(remainingTime == 0){
            Stop();
        }
    }

    void SpawnRandomCheckpoint(){
        //get all checkpoints
        checkpointSpawns = GameObject.FindGameObjectsWithTag("CheckpointSpawn");
        var spawnCount = checkpointSpawns.Length-1;
        var spawn = Random.Range(0,spawnCount);
       
        lastCheckPoint = Instantiate(checkpointPrefab, checkpointSpawns[spawn].transform.position, Quaternion.identity);
        Debug.Log("It here:"+lastCheckPoint.transform.position);
        CheckPoint script = lastCheckPoint.GetComponent<CheckPoint>();
        script.setCallbackAction((Next));
    }

    void Next(){
        if(!started){
            started = true;
            carController.EnableFastMode();
            musicManager.setDejaVu();
        }
        scoreManager.incrementScore();
        remainingTime = resetTime;
        SpawnRandomCheckpoint();
    }

    void Stop(){
        Destroy(lastCheckPoint);
    }
}

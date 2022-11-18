using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointPrefab;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private float resetTime;

    [SerializeField] private CarController carController;
    [SerializeField] private Image gameOverScreen;
    [SerializeField] public TMP_Text gameOverText;
    [SerializeField] private float fadeoutSpeed;

    private bool started = false;

    private bool stopped = false;

    private float remainingTime;

    private GameObject lastCheckPoint;

    private float waitForScreen = 4;

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
        if(remainingTime == 0 && !stopped){
            Stop();
        }
        if(stopped){
            Color color = gameOverScreen.color;
            float alpha = color.a;
            if(alpha < 255){
                color.a = (alpha + Time.deltaTime * fadeoutSpeed);
                gameOverScreen.color = color;
                gameOverText.color = new Color(255, 255, 255, color.a);

            } 
            waitForScreen -= Time.deltaTime;            

            if(waitForScreen < 0){
                SceneManager.LoadScene(0);
            }

            float volume = musicManager.getVolume();
            if(volume > 0){
                float targetVolume = volume - Time.deltaTime * fadeoutSpeed;
                musicManager.setVolume(targetVolume < 0 ? 0 : targetVolume);
            }
        }
    }

    void SpawnRandomCheckpoint(){
        lastCheckPoint = Instantiate(checkpointPrefab, new Vector3(Random.Range(-45, 45) , 0.5F, Random.Range(-45, 45)), Quaternion.identity);
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
        carController.DisableFastMode();
        gameOverText.text = "GAME OVER\nScore : " + (scoreManager.getScore());
        stopped = true;
    }
}

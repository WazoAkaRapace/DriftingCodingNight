using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] public TMP_Text textScore;
    [SerializeField] public TMP_Text textTimer;
    private float score;
    // Start is called before the first frame update
    void Start()
    {
        textScore.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void incrementScore(){
        score += 1;
        textScore.text = score.ToString();
    }

    public float getScore(){
        return score;
    }

    public void UpdateTimer(float remaining){
        textTimer.text = remaining.ToString("0.00");
    }

}

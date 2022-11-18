using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioClip calmSong;
    [SerializeField] private AudioClip dejaVuSong;

    [SerializeField] private AudioSource musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCalm(){
        musicPlayer.clip = calmSong;
    }

    public void setDejaVu(){
        musicPlayer.clip = dejaVuSong;
        musicPlayer.time = 64.5f;
        musicPlayer.Play();
    }

    public void setVolume(float volume){
        musicPlayer.volume = volume;
    }

    public float getVolume(){
        return musicPlayer.volume;
    }

}

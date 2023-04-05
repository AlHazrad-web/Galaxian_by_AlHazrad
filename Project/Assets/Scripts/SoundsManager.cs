using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource soundsSource;
    public List<AudioClip> sounds = new List<AudioClip>();
    public AudioClip gameMusic;

    public void StartGame() {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void PlayOneShot(int index) {
        soundsSource.PlayOneShot(sounds[index]);
    }

}

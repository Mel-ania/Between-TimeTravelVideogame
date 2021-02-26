using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Manager
{
    [SerializeField] private Slider slider = null;
    private AudioSource music;

    private float volume;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        music = GetComponent<AudioSource>();
        volume = (PlayerPrefs.GetFloat("MusicVolume") > 0) ? PlayerPrefs.GetFloat("MusicVolume") : 1f;
    }

    private void Start()
    {
        music.Play();
        slider.value = (PlayerPrefs.GetFloat("MusicVolume") > 0) ? PlayerPrefs.GetFloat("MusicVolume") : 1f;
    }

    private void Update()
    {
        music.volume = volume;
    }

    // when the volume is updated, the actual music volume change, and the preference
    // is saved
    public void OnVolumeUpdate(float volume)
    {
        this.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", (volume > 0) ? volume : 0.0001f);
    }
}

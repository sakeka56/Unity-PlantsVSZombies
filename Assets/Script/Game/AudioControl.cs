using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public static AudioControl Instance { get; private set; }
    private AudioSource audioSource;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PlayBgm(Config.bgm3);
    }
    private void Update()
    {
        if (audioSource.clip == Resources.Load<AudioClip>(Config.bgm3))
        {
            audioSource.volume -= 0.05f * Time.deltaTime;
        }
    }

    public void PlayBgm(string path)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.volume = 0.2f;
    }

    public void PlayClip(string path, float volume = 0.5f)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(audioClip, transform.position, volume);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public List<AudioClip> audios;       //0 bgmusic, 1 boss music, 2 victory music, 3 gameover;

    public AudioSource audioSource;

    public float volume = 1f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.clip == audios[0])
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }
    }

    public void SetClipNoFade(AudioClip audio)
    {
        audioSource.Stop();
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void SetClip(AudioClip audio)
    {
        StartCoroutine(FadeOut(audioSource, 2f, audio));
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime, AudioClip audioToPlay)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();


        StartCoroutine(FadeIn(audioSource, 2f, audioToPlay));

    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime, AudioClip audioToPlay)
    {
        audioSource.clip = audioToPlay;
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void PlayVictorySound()
    {
        audioSource.Stop();
        audioSource.clip = audios[2];
        audioSource.Play();
    }


    public void PlayGameOverSound()
    {
        audioSource.Stop();
        audioSource.clip = audios[3];
        audioSource.Play();
    }

    public void SetBgMusic()
    {
        audioSource.Stop();
        audioSource.clip = audios[0];
        audioSource.Play();
    }

    public void AudioSourceToggle()
    {
        audioSource.enabled = !audioSource.enabled;
    }

    public void ChangeVolume(float volumeValue)
    {
        AudioListener.volume = volumeValue;
    }

}




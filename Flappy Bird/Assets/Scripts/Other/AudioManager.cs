using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource source;
    public AudioClip crashSound;
    public AudioClip deathSound;
    public AudioClip scoreSound;
    public AudioClip flapSound;
    public AudioClip transitionSound;

    void Awake()
    {
        if(instance != null) Destroy(gameObject);
        instance = this;

        source = GetComponent<AudioSource>();

        if(PlayerPrefs.GetString("Audio Mute") == "True")
        {
            source.mute = true;
        }
        else if(PlayerPrefs.GetString("Audio Mute") == "False")
        {
            source.mute = false;
        }
    }

    public void PlayCrashSound()
    {
        source.PlayOneShot(crashSound);
    }

    public void PlayFallSound()
    {
        StartCoroutine(PlayDelayedFallSound());
    }

    public void PlayScoreSound()
    {
        source.PlayOneShot(scoreSound);
    }

    public void PlayFlapSound()
    {
        source.PlayOneShot(flapSound);
    }

    public void PlayTransitionSound()
    {
        source.PlayOneShot(transitionSound);
    }

    IEnumerator PlayDelayedFallSound()
    {
        yield return new WaitForSeconds(.1f);
        source.PlayOneShot(deathSound);
    }

    public void Mute()
    {
        source.mute = !source.mute;
    }
}

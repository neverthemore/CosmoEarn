using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Main Tracks")]
    public AudioClip track1;
    public AudioClip track2;

    public AudioClip BossFightTrack;

    private AudioSource audioSource;
    private bool currentTrackIsFirst = true;
    private Coroutine switchCoroutine;
    private bool isBossFight = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        StartMainTrackCycle();
    }

    void StartMainTrackCycle()
    {
        PlayTrack(track1);
        currentTrackIsFirst = true;

        if (switchCoroutine != null)
            StopCoroutine(switchCoroutine);

        switchCoroutine = StartCoroutine(TrackCycleCoroutine());
    }

    IEnumerator TrackCycleCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            SwitchTrack();
        }
    }

    void SwitchTrack()
    {
        if (currentTrackIsFirst)
        {
            PlayTrack(track2);
        }
        else
        {
            PlayTrack(track1);
        }
        currentTrackIsFirst = !currentTrackIsFirst;
    }

    void PlayTrack(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.Play();
    }


     public void BossFightTrackRoutine()
    {
        isBossFight = true;

        AudioClip currentClip = audioSource.clip;
        float currentTime = audioSource.time;

        if (switchCoroutine != null)
            StopCoroutine(switchCoroutine);

        PlayTrack(BossFightTrack);   
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
}
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioConroller : MonoBehaviour
{
    [SerializeField] Slider music_slider;
    [SerializeField] Slider sounds_slider;
    [SerializeField] AudioSource music_audioSource;
    [SerializeField] AudioClip music_audioClip;
    [SerializeField] AudioSource sounds_audioSource;
    [SerializeField] AudioClip sounds_audioClip;
    
    void Update()
    {
        music_audioSource.volume = music_slider.value;
        sounds_audioSource.volume = sounds_slider.value;
    }
}

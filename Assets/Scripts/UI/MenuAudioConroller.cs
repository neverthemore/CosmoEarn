using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
      
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

    [SerializeField] private Button exitButton;
    public Slider sfxSlider;
    public Slider musicSlider;
    void SetSliders()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
          
            musicMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            sfxMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            SetSliders();   
       
    }

    public void UpdateSFXVolume()
    {
        sfxMixer.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
    // called when we update the music slider
    public void UpdateMusicVolume()
    {
        musicMixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
   
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

   
}
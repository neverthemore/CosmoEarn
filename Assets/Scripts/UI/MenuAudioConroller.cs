using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    
    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;

    [Header("UI Elements")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button exitButton;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeAudioSettings();
        SetupUIListeners();
    }


    private void InitializeAudioSettings()
    {
   
        musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey, 0.4f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey, 0.75f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    private void SetupUIListeners()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        exitButton.onClick.AddListener(ExitGame);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("MasterVolume", ConvertToDecibel(volume));
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("MasterVolume", ConvertToDecibel(volume));
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    private float ConvertToDecibel(float volume)
    {
        // Конвертация линейной шкалы (0-1) в децибелы
        return volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f;
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void OnDestroy()
    {     
        if (Instance == this)
        {
            PlayerPrefs.Save();
        }
    }
}
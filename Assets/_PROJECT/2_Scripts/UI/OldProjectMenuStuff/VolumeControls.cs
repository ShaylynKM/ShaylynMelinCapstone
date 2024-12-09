using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControls : Singleton<VolumeControls>
{
    [SerializeField]
    private AudioMixer _musicMixer;

    [SerializeField]
    private AudioMixer _sfxMixer;

    [SerializeField]
    private Slider _musicSlider;

    [SerializeField]
    private Slider _sfxSlider;

    private const float _maxValue = 0f; // Audio volume in db

    private float _currentMusicVolume;
    private float _currentSFXVolume;

    protected override void Awake()
    {
        base.Awake();
        _isPersistent = true;

    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged; // Subscribes our scene changing method to the callback event

        _currentMusicVolume = PlayerPrefs.GetFloat("MusicVolume", _maxValue); // Assigns the volume as the saved volume (unless there is no saved volume, then the default, maximum level is used)
        _currentSFXVolume = PlayerPrefs.GetFloat("SFXVolume", _maxValue);

        OnSliderFound();
        SetMusicVolume(_currentMusicVolume);
        SetSFXVolume(_currentSFXVolume);
    }

    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        OnSliderFound();
        SetMusicVolume(_currentMusicVolume);
        SetSFXVolume(_currentSFXVolume);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.activeSceneChanged -= OnActiveSceneChanged; // Unsubscribes from the callback method when this object is destroyed
    }

    public void SetMusicVolume(float sliderValue)
    {
        // -60 as the lowest value because past that isn't audible anyway
        float dB = Mathf.Lerp(-60, 0f, sliderValue);

        // Save the values
        _currentMusicVolume = sliderValue;

        PlayerPrefs.SetFloat("MusicVolume", _currentMusicVolume);

        _musicMixer.SetFloat("MusicVolume", dB); // Apply to the mixer
    }

    public void SetSFXVolume(float sliderValue)
    {
        float dB = Mathf.Lerp(-60f, 0f, sliderValue);

        // Save the values
        _currentSFXVolume = sliderValue;

        PlayerPrefs.SetFloat("SFXVolume", _currentSFXVolume);

        _sfxMixer.SetFloat("SFXVolume", dB); // Apply to the mixer
    }

    // Finds and assigns a new volume slider for when the scene changes
    private void OnSliderFound()
    {
        Slider[] sliders = FindObjectsOfType<Slider>(true);

        // Makes sure we find the correct slider before proceeding
        foreach (Slider slider in sliders)
        {
            if (slider.name == "MusicSlider")
            {
                _musicSlider = slider;
                _musicSlider.onValueChanged.AddListener(SetMusicVolume);
                _musicSlider.value = _currentMusicVolume;
            }
            else if (slider.name == "SFXSlider")
            {
                _sfxSlider = slider;
                _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
                _sfxSlider.value = _currentSFXVolume;
            }
        }
    }
}
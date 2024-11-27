using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControls : Singleton<VolumeControls>
{
    /// <summary>
    /// NEEDS TO BE REFACTORED
    /// Some stuff in here won't be applicable, and some of it just needs to be written a little better
    /// </summary>

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _volumeSlider; // For the overall volume

    [SerializeField]
    private Toggle _voiceToggle; // For muting the voiced effects

    private const float _maxValue = 0f; // Audio volume in db

    [SerializeField]
    private float _currentSavedVolume;

    [SerializeField]
    private bool _voiceIsAudible = true;

    [SerializeField]
    private AudioSource _typeSFX;

    private void Start()
    {
        _isPersistent = true;

        SceneManager.activeSceneChanged += OnActiveSceneChanged; // Subscribes our scene changing method to the callback event

        _currentSavedVolume = PlayerPrefs.GetFloat("Volume", _maxValue); // Assigns the volume as the saved volume (unless there is no saved volume, then the default, maximum level is used

        _voiceIsAudible = PlayerPrefs.GetInt("VoiceMute", 1) == 1;

        OnSliderFound();
        OnToggleFound();
        AssignTypeSFX();
        SetVolume(_currentSavedVolume);
        SFXMuteState();
    }

    void OnActiveSceneChanged(Scene previous, Scene next)
    {
        OnSliderFound();
        OnToggleFound();
        AssignTypeSFX();
        SetVolume(_currentSavedVolume);
        SFXMuteState();
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged; // Unsubscribes from the callback method when this object is destroyed
    }

    // Sets the volume for all audio
    public void SetVolume(float volume)
    {
        _currentSavedVolume = volume;

        PlayerPrefs.SetFloat("Volume", _currentSavedVolume);

        _audioMixer.SetFloat("MasterVolume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
    }

    // Finds and assigns a new volume slider for when the scene changes
    private void OnSliderFound()
    {
        Slider[] sliders = FindObjectsOfType<Slider>(true);

        // Makes sure we find the correct slider before proceeding
        foreach(Slider slider in sliders)
        {
            if(slider.name == "VolumeSlider")
            {
                _volumeSlider = slider;

                break;
            }
        }

        if(_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener(SetVolume);
            _volumeSlider.value = _currentSavedVolume;
        }

        else
        {
            Debug.Log("Slider not found");
        }

    }

    // Assigns the muted/unmuted state of the mute toggle in relation to the voice SFX
    private void SFXMuteState()
    {
        if (_typeSFX != null)
        {
            _typeSFX.mute = !_voiceIsAudible;
        }
    }

    public void OnVoiceMute(bool isMuted)
    {
        _voiceIsAudible = !isMuted;
        PlayerPrefs.SetInt("VoiceMute", _voiceIsAudible ? 1 : 0); // Saves the bool in PlayerPrefs as an Int, since PlayerPrefs doesn't natively support boolean values
        SFXMuteState();
    }

    // Finds and assigns a new voice effect toggle for when the scene changes
    private void OnToggleFound()
    {
        Toggle[] toggles = FindObjectsOfType<Toggle>(true);

        // Makes sure we find the correct toggle before proceeding
        foreach (Toggle toggle in toggles)
        {
            if (toggle.name == "VoiceToggle")
            {
                _voiceToggle = toggle;

                break;
            }
        }

        if (_voiceToggle != null)
        {
            // Assigns the listener for the value changed
            _voiceToggle.onValueChanged.AddListener(delegate
            {
                OnVoiceMute(!_voiceToggle.isOn);
            });

            _voiceToggle.isOn = _voiceIsAudible; // Sets the toggle in the scene to on or off based on the current mute state
        }
        else
        {
            Debug.Log("Mute toggle not found");
        }
    }

    // Finds and assigns the typing sounds
    private void AssignTypeSFX()
    {
        GameObject typeSFXObject = GameObject.FindWithTag("TypingSFX"); // Finds the object with the audio source

        if (typeSFXObject != null)
        {
            _typeSFX = typeSFXObject.GetComponent<AudioSource>();           
        }
    }
}

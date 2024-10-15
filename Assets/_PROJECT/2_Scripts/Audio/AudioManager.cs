using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    /// <summary>
    /// One scriptable object per type of clip (for example, if I have 3 player hit sounds, put those together in one SO.)
    /// Remember to assign a mixer group to each SO depending on if it's music or sfx.
    /// </summary>

    private AudioSO[] _audioSOs; // Array of audioSOs

    private Dictionary<string, AudioSO> _audioPairs; // Name of our audio SOs plus their objects

    private AudioSource _audioSource; // Individual audio source to reference

    private List<AudioSource> _audioPool; // Audio sources to put in our pool

    private int _audioPoolSize; // How many audio sources we need

    private float _lowestPitch = 0.8f; // The lowest pitch we can get with randomized pitch
    private float _highestPitch = 1.2f; // The highest pitch we can get with randomized pitch

    protected override void Awake()
    {
        base.Awake();

        _audioSOs = Resources.LoadAll<AudioSO>("ScriptableObjects/Audio"); // Load all the SOs from the Audio folder in Resources
        _audioPairs = new Dictionary<string, AudioSO>(); // Our dictionary of audioSOs
        _audioPoolSize = _audioSOs.Length; // We have as many audios in our pool as there are audioSOs

        foreach(var audioData in _audioSOs)
        {
            if(!_audioPairs.ContainsKey(audioData.ClipName))
            {
                _audioPairs.Add(audioData.ClipName, audioData); // Add the audioSO to our dictionary if it does not already exist
                audioData.CanPlay = true; // All the clips are available to play at the start of the game
                audioData.IsPaused = false; // None of the clips are paused
            }
        }

        _audioPool = new List<AudioSource>();

        for(int i = 0; i < _audioPoolSize; i++)
        {
            AudioSource pooledAudioSource = this.gameObject.AddComponent<AudioSource>(); // Add an audio source for every available audioSO
            _audioPool.Add(pooledAudioSource); // Add this audio source to the pool
        }

    }

    public AudioSource GetAudioSource(AudioClip clipName)
    {
        // Gets an available audio source out of the pool
        foreach(AudioSource audioSource in _audioPool)
        {
            if(audioSource.clip == null || audioSource.clip == clipName) // If this audio source isn't occupied, or it is occupied by the current clip we want to play, it is available
            {
                return audioSource;
            }
        }
        return null;
    }

    public void PlayAudio(string audioName)
    {
        if(_audioPairs.TryGetValue(audioName, out var audioData)) // If our audio is in the dictionary, assign its value to "audioData"
        {
            _audioSource = GetAudioSource(audioData.Clip); // Get an available audio source out of our pool to use

            if(audioData.IsPaused == true)
            {
                return; // This audio source is taken by a paused audio. Try again
            }

            if(_audioSource != null && audioData.CanPlay == true)
            {
                AudioClip currentClip = audioData.Clip;

                audioData.CanPlay = false;

                _audioSource.clip = currentClip;
                _audioSource.volume = audioData.ClipVolume;
                _audioSource.pitch = audioData.ClipPitch;
                _audioSource.loop = audioData.IsLooped;
                _audioSource.outputAudioMixerGroup = audioData.Mixer;
                audioData.Source = _audioSource;

                if (audioData.PitchRandomized == true)
                {
                    audioData.ClipPitch = Random.Range(_lowestPitch, _highestPitch); // Randomize the pitch, if we've selected the option to do so in the SO
                    _audioSource.pitch = audioData.ClipPitch; // Reassign the pitch
                }
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play(); // Play the audio, as long as it is not currently in the process of playing

                    if(_audioSource.isPlaying == true)
                        StartCoroutine(WaitBeforePlaying(audioName, currentClip.length)); // Must wait the length of the current clip before playing it again
                }
                else
                {
                    Debug.LogError("The clip" + audioName + "cannot be found.");
                }
            }
        }
    }

    IEnumerator WaitBeforePlaying(string audioName, float time)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData)) // If our audio is in the dictionary, assign its value to "audioData"
        {
            yield return new WaitForSeconds(time);
            audioData.CanPlay = true;

        }
    }

    public void StopAudio(string audioName)
    {
        if(_audioPairs.TryGetValue(audioName, out var audioData)) // If our audio is in the dictionary, assign its value to "audioData"
        {
            // If this clip has a source, it is currently playing and the clip in question is the name we specified:
            if (audioData.Source != null && audioData.Source.isPlaying && audioData.Source.clip == audioData.Clip)
            {
                audioData.Source.Stop(); // Stop this audio
            }
        }
    }

    public void PauseAudio(string audioName)
    {
        if(_audioPairs.TryGetValue(audioName, out var audioData)) // If our audio is in the dictionary, assign its value to "audioData"
        {
            // If this clip has a source, it is currently playing and the clip in question is the name we specified:
            if (audioData.Source != null && audioData.Source.isPlaying && audioData.Source.clip == audioData.Clip)
            {
                audioData.Source.Pause(); // Pause this audio
                audioData.IsPaused = true;

                Debug.Log(audioData.name + audioData.IsPaused);
            }
        }
        else
        {
            Debug.LogError("The clip" + audioName + "cannot be found.");
        }
    }
    public void UnpauseAudio(string audioName)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData)) // If our audio is in the dictionary, assign its value to "audioData"
        {
            // If this clip has a source, it is not currently playing and the clip in question is the name we specified:
            if (audioData.Source != null && audioData.Source.isPlaying == false && audioData.Source.clip == audioData.Clip)
            {
                audioData.Source.UnPause(); // Play this audio after pausing it
                audioData.IsPaused = false;

                Debug.Log(audioData.name + audioData.IsPaused);
            }
        }
        else
        {
            Debug.LogError("The clip" + audioName + "cannot be found.");
        }
    }
}

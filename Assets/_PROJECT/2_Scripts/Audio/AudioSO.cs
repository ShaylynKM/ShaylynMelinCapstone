using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioSO", menuName = "ScriptableObjects/Audio")]
public class AudioSO : ScriptableObject
{
    [SerializeField]
    private AudioClip[] _clip; // Array of audio clips

    // (It was my idea to do the below on the last project, but credit to Blake for the syntax)
    public AudioClip Clip
    {
        get
        {
            return _clip[Random.Range(0, _clip.Length)]; // If we have more than one clip in this SO, randomly access one of them (to allow for a variety of sounds assigned to one trigger)
        }
    }

    [Tooltip("Assign music and SFX to their own category mixer group.")]
    public AudioMixerGroup Mixer; // Should have a mixer for both sfx and music, so the volume of those can be controlled independently in the settings.

    public string ClipName;
    public float ClipVolume = .5f;
    public float ClipPitch = 1f; // May randomize the pitch for certain sounds
    public bool PitchRandomized;
    public bool IsLooped;
    public bool CanPlay; // If this audio can be played right now (if it isn't already playing)
    public bool IsPaused; // Keep track of if this audio is paused so that a different audio doesn't take over its audio source

    [HideInInspector]
    public AudioSource Source;
}

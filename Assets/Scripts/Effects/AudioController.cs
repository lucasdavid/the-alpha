using UnityEngine;
using System;
using System.Collections;

public class AudioController : MonoBehaviour {

    public enum DefaultSounds { attack, hurt, death, charge }

    public AudioClip[] sounds;
 
    AudioSource source;

    void Start()
    {
        if (sounds.Length == 0) { Debug.LogWarning("no sounds were attached to the component"); }
        
        if ( ( source = GetComponent<AudioSource>() ) == null )
            throw new MissingComponentException("AudioController requires an AudioSource component attached to the same object");
    }

    public void Play ( int _index )
    {
        Play ( _index, false );
    }

	public void Play ( int _index, bool _force )
    {
        if (_index >= sounds.Length)
            Debug.LogException(new UnityException("invalid _index value " + _index));

        if ( ! source.isPlaying || _force )
        {
            source.clip = sounds[_index];
            source.Play ();
        }
    }
}

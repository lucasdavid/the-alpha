using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    public AudioClip[] sounds;
 
    AudioSource source;

    void Start()
    {
        if ( (source = GetComponent<AudioSource>()) == null )
            throw new MissingComponentException("AudioController requires an AudioSource component attached to the same object");
    }

    public void Play ( int _index )
    {
        Play ( _index, false );
    }

	public void Play ( int _index, bool _force )
    {
        if ( !source.isPlaying || _force )
        {
            source.clip = sounds[_index];
            source.Play ();
        }
    }
}

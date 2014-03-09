using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    protected AudioClip[] sounds;
    AudioSource source;

    void Start()
    {
        if ( ( source = GetComponent<AudioSource>() ) == null )
            throw new MissingComponentException("AudioController requires an AudioSource component attached to the same object");
    }

    public void PlayRandom()
    {
        PlayRandom(false);
    }

    public void PlayRandom( bool _force )
    {
        int sound = (int)(UnityEngine.Random.value * sounds.Length);
        if (sound == sounds.Length) sound--;

        Play(sound, _force);
    }

    protected void Play ( int _index )
    {
        Play ( _index, false );
    }

	protected void Play ( int _index, bool _force )
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

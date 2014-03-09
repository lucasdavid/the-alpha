using UnityEngine;
using System.Collections;

public class StormAudioController : AudioController {

    public AudioClip[] thunders;

    public void Thunder()
    {
        sounds = thunders;
        PlayRandom(true);
    }
}

using UnityEngine;
using System.Collections;

public class HumanAudioController : AudioController {

    public AudioClip[] attacks;
    public AudioClip[] hurts;

    public void Attack()
    {
        if (Random.value > .25f)
        {
            sounds = attacks;
            PlayRandom();
        }
    }

    public void Hurt()
    {
        sounds = hurts;
        PlayRandom(true);
    }

}

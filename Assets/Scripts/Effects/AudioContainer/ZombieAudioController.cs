using UnityEngine;
using System.Collections;

public class ZombieAudioController : AudioController {

    public AudioClip[] moans;
    public AudioClip[] attacks;
    public AudioClip[] rages;
    public AudioClip[] hurts;

    public void Moan()
    {
        sounds = moans;
        PlayRandom();
    }

    public void Rage()
    {
        sounds = rages;
        PlayRandom();
    }

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
        PlayRandom();
    }
}

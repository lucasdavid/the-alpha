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
        // this motherfucking sound of the zombie attack is fucking annoyng,
        // only plays it 25% of the time.
        if (Random.value > .75f)
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

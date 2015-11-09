using UnityEngine;
using System.Collections;

public class StormLightEffect : MonoBehaviour {

    public float thunderDelay, minLightningInterval, maxLightningInterval;

    Animator animator;
    StormAudioController stormAudio;

	void Start()
    {
        animator = GetComponent<Animator>();
        stormAudio = GetComponent<StormAudioController>();
        StartCoroutine("Thunder");
    }

    IEnumerator Thunder()
    {
        while (true)
        {
            // thunders occurr in an interval between minLightingInterval and maxLightningInterval
            yield return new WaitForSeconds(
                Random.value * (maxLightningInterval - minLightningInterval) + minLightningInterval
            );
            animator.SetTrigger("thunder");

            yield return new WaitForSeconds(thunderDelay);
            stormAudio.Thunder();
        }
    }
}

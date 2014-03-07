using UnityEngine;
using System.Collections;

public class StormLightEffect : MonoBehaviour {

    public float minLightingInterval, maxLightningInterval;

    Animator anim;

	void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Thunder");
    }

    IEnumerator Thunder()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.value * ( maxLightningInterval - minLightingInterval ) + minLightingInterval
            );
            anim.SetTrigger("thunder");
        }
    }
}

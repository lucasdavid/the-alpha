using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectiveLight : MonoBehaviour {

    public bool lightsOn;
    public List<Light> Lights;

    void OnPreCull()
    {
        foreach (Light light in Lights)
        {
            light.enabled = lightsOn;
        }
    }

    void OnPostRender()
    {
        foreach (Light light in Lights)
        {
            light.enabled = !lightsOn;
        }
    }
}

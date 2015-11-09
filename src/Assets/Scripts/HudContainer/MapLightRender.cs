using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapLightRender : MonoBehaviour {

    public List<Light> Lights;
    
    void OnPreRender()
    {
        foreach ( Light light in Lights )
            light.enabled = false;
    }
    
    void OnPostRender()
    {
        foreach ( Light light in Lights )
            light.enabled = true;
    }
}

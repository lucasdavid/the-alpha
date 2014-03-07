using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    public Camera mapCamera;

    LayerMask layer;
    CameraMarquee marquee;

    void Start()
    {
        marquee = Camera.main.GetComponent<CameraMarquee>();
        layer = 1 << 12; // Ignore Tier Layer
    }

    void OnMouseEnter()
    {
        marquee.mouseIsBeingUsedByHUD = true;
    }

    void OnMouseDrag()
    {
        if ( marquee.mouseIsBeingUsedByHUD )
        {
            Ray ray = mapCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if ( Physics.Raycast(ray, out hit, Mathf.Infinity, ~layer))
                Camera.main.GetComponent<CameraMovement>().Move ( hit.point );
        }
    }
    
    void OnMouseUp()
    {
        marquee.mouseIsBeingUsedByHUD = false;
    }

}

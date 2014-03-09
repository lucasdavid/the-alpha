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

    void OnMouseOver()
    {
        marquee.mouseIsBeingUsedByHUD = true;
    }

    void OnMouseUp()
    {
        marquee.mouseIsBeingUsedByHUD = false;
    }

    void OnMouseDrag()
    {
        if ( marquee.mouseIsBeingUsedByHUD )
        {
            RaycastHit hit;
            if ( Physics.Raycast(
                mapCamera.ScreenPointToRay(Input.mousePosition),
                out hit, Mathf.Infinity, ~layer)
            )
                Camera.main.GetComponent<CameraMovement>().Move ( hit.point );
        }
    }

}

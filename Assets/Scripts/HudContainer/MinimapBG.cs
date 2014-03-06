using UnityEngine;
using System.Collections;

public class MinimapBG : MonoBehaviour {

    CameraMarquee marquee;

    void Start()
    {
        marquee = Camera.main.GetComponent<CameraMarquee>();
    }

	void OnMouseDown()
    {
        marquee.mouseIsBeingUsedByHUD = true;
    }

    void OnMouseUp()
    {
        marquee.mouseIsBeingUsedByHUD = false;
    }
}

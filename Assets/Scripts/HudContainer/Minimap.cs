using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {
    // Can migrate this code over the CameraMarquee when I figure out how to detect IF-INSIDE-HUD
    // - Victor
    LayerMask layer;
    Camera thisCamera;

	// Use this for initialization
	void Start () {
        thisCamera = this.camera;
        layer = 1 << 12; // Ignore Tier Layer
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton (0)) {
            MoveMap();
        }
	}

    void MoveMap() {
        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit, Mathf.Infinity, ~layer)) {
            // Keep camera height fixed, move x, z

            // Z Offset for camera angle
            Vector3 moveCamera = new Vector3(hit.point.x, Camera.main.transform.position.y, hit.point.z - 25.0f);

            if (hit.point.x > 0 && hit.point.z > 0)
                Camera.main.transform.position = moveCamera;
        }
    }

}

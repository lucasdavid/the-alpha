using UnityEngine;
using System.Collections;

public class CameraSlider : MonoBehaviour {
    float _cameraDistance;
    float _cameraRotation;
    Vector3 camDistance;
    Vector3 camRotation;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        _cameraDistance = 15.0f;
        _cameraRotation = 45.0f;
	}
	
	// Update is called once per frame
	void Update () {
        camDistance = cam.transform.position;
        camRotation = cam.transform.rotation.eulerAngles;

        camDistance.y = _cameraDistance;
        camRotation.x = _cameraRotation;

        cam.transform.eulerAngles = camRotation;
        cam.transform.position = camDistance;
	}

    void OnGUI() {
        //_sliderValue = GUI.HorizontalSlider(new Rect(50.0f, 50.0f, 100.0f, 100.0f), _sliderValue, 15.0f, 50.0f);
        GUILayout.BeginArea(new Rect(50.0f, 50.0f, 175.0f, 175.0f));
        _cameraDistance = GUILayout.HorizontalSlider(_cameraDistance, 10.0f, 50.0f);
        GUILayout.Label("Camera Distance: " + _cameraDistance);
        _cameraRotation = GUILayout.HorizontalSlider(_cameraRotation, 30.0f, 60.0f);
        GUILayout.Label("Camera Rotation: " + _cameraRotation);
        GUILayout.EndArea();
    }
}

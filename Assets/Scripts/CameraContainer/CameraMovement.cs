using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    // speed in which the camera moves
    public float speed;
    public float distance;
    public float rotationSpeed;

    Vector3 movement;
    Rect[] rectangles;
    
    void Start ()
    {
        movement    = transform.position;
        rectangles  = new Rect[4];
        
        // rectangles used to verify if mouse is over one of the edges of the screen
        rectangles[0] = new Rect(0, 0, Screen.width, 20);
        rectangles[1] = new Rect(0, Screen.height - 20, Screen.width, 20);
        rectangles[2] = new Rect(0, 0, 20, Screen.height);
        rectangles[3] = new Rect(Screen.width - 20, 0, 20, Screen.height);
    }
    
    void Update ()
    {
        RaycastHit hit;
        int layerMask = 1 << 11;
        Ray ray = new Ray(transform.position, Vector3.down);

        if ( Physics.Raycast(ray, out hit, 100f, layerMask) && transform.position.y - hit.point.y != distance ) {
            transform.position += ( transform.position.y - hit.point.y ) * Vector3.up;
        }

        // get input from axis
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement.y = 0;

        // if mouse is over bottom edge
        if ( rectangles[0].Contains(Input.mousePosition) )
            movement.z = -1;
        // if mouse is over top edge
        if ( rectangles[1].Contains(Input.mousePosition) )
            movement.z = 1;
        // if mouse is over left edge
        if ( rectangles[2].Contains(Input.mousePosition) )
            movement.x = -1;
        // if mouse is over right edge
        if ( rectangles[3].Contains(Input.mousePosition) )
            movement.x = 1;

        // if the mouse scrollwheel is moved
        if ( ! Input.GetKey(Keymap.kmCamera.SecondFunction) )
        {
            if ( Input.GetAxis("Mouse ScrollWheel") > 0 )
                movement.y = 5;
            if ( Input.GetAxis("Mouse ScrollWheel") < 0 )
                movement.y = -5;
        }
        // if ctrl is pressed and the mouse scrollwheel is moved
        else
        {
            Vector3 rotation = transform.eulerAngles;

            if ( Input.GetAxis("Mouse ScrollWheel") < 0 )
                rotation.x += rotationSpeed * Time.deltaTime;
            if ( Input.GetAxis("Mouse ScrollWheel") > 0 )
                rotation.x -= rotationSpeed * Time.deltaTime;

            transform.eulerAngles = rotation;
        }
        
        // if there is any significant movement
        if ( Mathf.Abs(movement.x) > .1f || Mathf.Abs(movement.z) > .1f || Mathf.Abs(movement.y) > .1f )
            transform.position += movement * speed * Time.deltaTime;
    }
}

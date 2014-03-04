using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    const int MIN_X = 24, MAX_X = 280, MIN_Y = 10, MAX_Y = 60, MIN_Z = -4, MAX_Z = 260;
    
    // speed in which the camera moves
    public float distance;
    public float movementSpeed;
    public float rotationSpeed;

    public LayerMask distanceMask;

    Vector3 movement;
    Rect[] rectangles;
    
    void Start ()
    {
        movement    = Vector3.zero;
        rectangles  = new Rect[4];
        
        // rectangles used to verify if mouse is over one of the edges of the screen
        rectangles[0] = new Rect(0, 0, Screen.width, 35);
        rectangles[1] = new Rect(0, Screen.height - 35, Screen.width, 35);
        rectangles[2] = new Rect(0, 0, 35, Screen.height);
        rectangles[3] = new Rect(Screen.width - 35, 0, 35, Screen.height);
    }
    
    void Update ()
    {
        // get input from axis
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

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

        // constrains the camera to the extremes of the field
        if ( movement.x > 0 && transform.position.x >= MAX_X ||
             movement.x < 0 && transform.position.x <= MIN_X )
            movement.x = 0;
        if ( movement.z > 0 && transform.position.z >= MAX_Z ||
             movement.z < 0 && transform.position.z <= MIN_Z )
            movement.z = 0;

        // if the mouse scrollwheel is moved
        if ( ! Input.GetKey(Keymap.kmCamera.SecondFunction) )
        {
            if ( Input.GetAxis("Mouse ScrollWheel") < 0 && distance >= MAX_Y )
                distance += movementSpeed * Time.deltaTime;
            if ( Input.GetAxis("Mouse ScrollWheel") > 0 && distance <= MIN_Y )
                distance -= movementSpeed * Time.deltaTime;
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
        if ( Mathf.Abs(movement.x) > .1f || Mathf.Abs(movement.z) > .1f )
            transform.position += movement * movementSpeed * Time.deltaTime;

        // process vertical movement of camera
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        
        if ( Physics.Raycast (ray, out hit, 100f, distanceMask) && transform.position.y - hit.point.y != distance ) {
            transform.position = new Vector3(
                transform.position.x,
                (
                hit.point.y > 0
                ? hit.point.y
                : 0
                ) + distance,
                transform.position.z
                );
        }
    }
}

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    // speed in which the camera moves
    public float speed;
    
    Vector3 movement;
    Rect[] rectangles;
    
    void Start ()
    {
        movement    = transform.position;
        movement.y  = 0;
        rectangles  = new Rect[4];
        
        // rectangles used to verify if mouse is over one of the edges of the screen
        rectangles[0] = new Rect(0, 0, Screen.width, 50);
        rectangles[1] = new Rect(0, Screen.height - 50, Screen.width, 50);
        rectangles[2] = new Rect(0, 0, 50, Screen.height);
        rectangles[3] = new Rect(Screen.width - 50, 0, 50, Screen.height);
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
        
        // if there is any significant movement
        if ( Mathf.Abs(movement.x) > .1f || Mathf.Abs(movement.z) > .1f )
            transform.position += movement * speed * Time.deltaTime;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class CameraMarquee : MonoBehaviour
{
    public Rect marqueeRect;
    public Texture marqueeGraphics;

    public bool mouseIsBeingUsedByHUD;
    
    public List<GameObject> SelectedUnits;
    
    Vector2 marqueeOrigin;
    Vector2 marqueeSize;
    Rect backupRect;
    
    void Start()
    {
        SelectedUnits = new List<GameObject>();
    }
    
    private void OnGUI()
    {
        marqueeRect = new Rect(marqueeOrigin.x, marqueeOrigin.y, marqueeSize.x, marqueeSize.y);
        GUI.color = new Color(0, 0, 0, .3f);
        GUI.DrawTexture(marqueeRect, marqueeGraphics);
    }

    void Update()
    {
        // ignore all actions performed by the mouse when it is over the HUD
        if ( ! mouseIsBeingUsedByHUD )
        {
            if ( Input.GetMouseButtonDown(0) )
            {
                MarqueeStart();
            }
            
            if ( Input.GetMouseButtonUp(0) )
            {
                MarqueeFinish();
            }
            
            if ( Input.GetMouseButton(0) )
            {
                MarqueeUpdate();
            }
            
            if ( Input.GetMouseButtonDown(1) )
            {
                Plane playerPlane = new Plane(Vector3.up, new Vector3(
                    transform.position.x,
                    1,
                    transform.position.z
                    ));
                
                Ray   ray     = Camera.main.ScreenPointToRay (Input.mousePosition);
                float hitdist = 0f;
                
                if (playerPlane.Raycast (ray, out hitdist)) {
                    Vector3 target = ray.GetPoint(hitdist);
                    
                    MoveUnits( target, true );
                }
            }
        }
    }

    void MarqueeStart()
    {
        // clear previous selection
        UnselectUnits();
        
        // initiate selection marquee
        float _invertedY = Screen.height - Input.mousePosition.y;
        marqueeOrigin = new Vector2(Input.mousePosition.x, _invertedY);
        
        //Check if the player just wants to add one unit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit) && hit.collider.CompareTag("selectable") )
        {
            hit.transform.gameObject.SendMessage("OnSelected",SendMessageOptions.DontRequireReceiver);
            SelectedUnits.Add(hit.collider.gameObject);
        }
    }

    void MarqueeUpdate()
    {
        float _invertedY = Screen.height - Input.mousePosition.y;
        marqueeSize = new Vector2(Input.mousePosition.x - marqueeOrigin.x, (marqueeOrigin.y - _invertedY) * -1);
        //FIX FOR RECT.CONTAINS NOT ACCEPTING NEGATIVE VALUES
        if (marqueeRect.width < 0)
        {
            backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y, Mathf.Abs(marqueeRect.width), marqueeRect.height);
        }
        else if (marqueeRect.height < 0)
        {
            backupRect = new Rect(marqueeRect.x, marqueeRect.y - Mathf.Abs(marqueeRect.height), marqueeRect.width, Mathf.Abs(marqueeRect.height));
        }
        if (marqueeRect.width < 0 && marqueeRect.height < 0)
        {
            backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y - Mathf.Abs(marqueeRect.height), Mathf.Abs(marqueeRect.width), Mathf.Abs(marqueeRect.height));
        }
    }

    void MarqueeFinish()
    {
        //Poppulate the selectableUnits array with all the selectable units that exist
        foreach (GameObject unit in FindSelectableUnits())
        {
            // ignore caracter if he was selected in the first click
            if ( SelectedUnits.Count > 0 && unit.GetInstanceID() == SelectedUnits[0].GetInstanceID() )
                continue;
            
            //Convert the world position of the unit to a screen position and then to a GUI point
            Vector3 _screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            Vector2 _screenPoint = new Vector2(_screenPos.x, Screen.height - _screenPos.y);
            
            //Ensure that any units not within the marquee are currently unselected
            if (marqueeRect.Contains(_screenPoint) || backupRect.Contains(_screenPoint))
            {
                SelectedUnits.Add (unit);
            }
        }

        SelectUnits();
        
        //Reset the marquee so it no longer appears on the screen.
        marqueeRect.width = 0;
        marqueeRect.height = 0;
        backupRect.width = 0;
        backupRect.height = 0;
        marqueeSize = Vector2.zero;
    }

    GameObject[] FindSelectableUnits()
    {
        return GameObject.FindGameObjectsWithTag("selectable");
    }

    public void UnselectUnits ()
    {
        // remove previous selection
        foreach ( GameObject unit in SelectedUnits )
            unit.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);
        
        SelectedUnits.Clear();
    }

    public void SelectUnits ( GameObject[] units )
    {
        UnselectUnits();
        SelectedUnits.AddRange(units);

        SelectUnits();
    }

    void SelectUnits ()
    {
        // add new selection
        foreach ( GameObject unit in SelectedUnits ) {
            unit.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void MoveUnits ( Vector3 _target, bool _overrideAttack )
    {
        foreach ( GameObject unit in SelectedUnits )
        {
            // Force movement to override attacking
            unit.GetComponent<UnitMovement>().Move( _target, _overrideAttack );
        }
    }

    public void HoldUnits ()
    {

        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitMovement>().hold = true;
    }

    public void UnholdUnits ()
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitMovement>().hold = false;
    }

    public void StopUnits ()
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitMovement>().Stop();
    }

    public void ResumeUnits ( )
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitMovement>().Resume();
    }
}

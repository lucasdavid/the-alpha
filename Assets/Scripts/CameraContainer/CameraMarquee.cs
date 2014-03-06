using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMarquee : MonoBehaviour
{
    public Texture marqueeGraphics;
    public bool    mouseIsBeingUsedByHUD;
    
    public List<GameObject> SelectedUnits;

    Rect marqueeRect;
    Vector2 marqueeOrigin;
    Vector2 marqueeSize;
    Rect backupRect;
    LayerMask layer;

    void Start()
    {
        layer = 1 << 12; // Ignore Tier Layer
        SelectedUnits = new List<GameObject>();
    }
    
    private void OnGUI()
    {
        marqueeRect = new Rect(marqueeOrigin.x, marqueeOrigin.y, marqueeSize.x, marqueeSize.y);
        GUI.color   = new Color(0, 0, 0, .3f);
        GUI.DrawTexture(marqueeRect, marqueeGraphics);
    }

    void Update()
    {
        // ignore all actions performed by the mouse when it is over the HUD
        if ( ! mouseIsBeingUsedByHUD )
        {
            if ( Input.GetMouseButtonDown(0) )
                MarqueeStart();

            if ( Input.GetMouseButtonUp(0) )
                MarqueeFinish();

            if ( Input.GetMouseButton(0) )
                MarqueeUpdate();

            if ( Input.GetMouseButtonDown(1) )
                StartCoroutine("RightMouseClick");
        }
    }

    void MarqueeStart()
    {
        // clear previous selection if "shift" is not held down
        if (!Input.GetKey (Keymap.kmSelect.Shift)) {
            UnselectUnits();
        }
        
        // initiate selection marquee
        float _invertedY = Screen.height - Input.mousePosition.y;
        marqueeOrigin = new Vector2(Input.mousePosition.x, _invertedY);
        
        //Check if the player just wants to add one unit
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(ray, out hit, Mathf.Infinity, ~layer) && hit.collider.CompareTag("selectable") )
        {
            // If selected already, unselect
            // If not selected, select
            if (SelectedUnits.Contains(hit.collider.gameObject)) {
                hit.collider.gameObject.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);
                SelectedUnits.Remove(hit.collider.gameObject);
            } else {
                hit.transform.gameObject.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
                SelectedUnits.Add(hit.collider.gameObject);
            }
        }
    }

    void MarqueeUpdate()
    {
        float _invertedY = Screen.height - Input.mousePosition.y;
        marqueeSize = new Vector2(Input.mousePosition.x - marqueeOrigin.x, (marqueeOrigin.y - _invertedY) * -1);

        if (marqueeRect.width < 0)
            backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y, Mathf.Abs(marqueeRect.width), marqueeRect.height);
        else if (marqueeRect.height < 0)
            backupRect = new Rect(marqueeRect.x, marqueeRect.y - Mathf.Abs(marqueeRect.height), marqueeRect.width, Mathf.Abs(marqueeRect.height));

        if (marqueeRect.width < 0 && marqueeRect.height < 0)
            backupRect = new Rect(marqueeRect.x - Mathf.Abs(marqueeRect.width), marqueeRect.y - Mathf.Abs(marqueeRect.height), Mathf.Abs(marqueeRect.width), Mathf.Abs(marqueeRect.height));
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
                SelectedUnits.Add (unit);
        }

        SelectUnits();
        
        //Reset the marquee so it no longer appears on the screen.
        marqueeRect.width = 0;
        marqueeRect.height = 0;
        backupRect.width = 0;
        backupRect.height = 0;
        marqueeSize = Vector2.zero;
    }

    void RightMouseClick()
    {
        RaycastHit hit;
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Right click an enemy
        if ( Physics.Raycast(r, out hit, Mathf.Infinity, ~layer) && hit.collider.CompareTag("enemy"))
        {
            Debug.Log ("Attacking " + hit.collider.gameObject);
            AttackUnit(hit.collider.gameObject);
        }
        else if ( Physics.Raycast(r, out hit, Mathf.Infinity, ~layer) )
        {
            MoveUnits( hit.point, true );
        }
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

    public void AttackUnit( GameObject _target ) {
        foreach ( GameObject unit in SelectedUnits )
        {
            // Force movement to override attacking
            unit.GetComponent<UnitController>().SetTarget(true);
            unit.GetComponent<Mob>().Target = _target;
        }
    }

    public void MoveUnits ( Vector3 _target, bool _overrideAttack )
    {
        // offset from _target of units, when they move towards it
        Vector3 relative = new Vector3(-8, 0, 0);

        foreach ( GameObject unit in SelectedUnits )
        {
            unit.GetComponent<UnitController>().hold = false;
            // Force movement to override attacking
            unit.GetComponent<UnitController>().SetTarget(false);
            unit.GetComponent<Mob>().Target = null;
            unit.GetComponent<UnitController>().Move( _target + relative, _overrideAttack );

            Debug.Log(_target + relative);

            if ( (relative.x += 4) == 8 )
            {
                relative.x = -8;
                relative.z -= 4;
            }
        }
    }

    public void HoldUnits ()
    {

        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitController>().hold = true;
    }

    public void UnholdUnits ()
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitController>().hold = false;
    }

    public void StopUnits ()
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitController>().Stop();
    }

    public void ResumeUnits ( )
    {
        foreach ( GameObject unit in SelectedUnits )
            unit.GetComponent<UnitController>().Resume();
    }

}

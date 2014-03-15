using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMarquee : MonoBehaviour {

    public List<GameObject> SelectedUnits;
    public Texture marqueeGraphics;
    public GameObject movementRing;
    public GameObject attackRing;
    public LayerMask marqueeLayer;
    public bool mouseIsBeingUsedByHUD;

    bool      marqueeStarted;
    Rect      marqueeRect;
    Rect      backupRect;
    Vector2   marqueeOrigin;
    Vector2   marqueeSize;
    HudController HUD;

    void Start()
    {
        SelectedUnits = new List<GameObject>();
        HUD = GameObject.Find ("HUD").GetComponent<HudController>();
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
        if (!mouseIsBeingUsedByHUD)
        {
            if (Input.GetMouseButtonDown(0))
                MarqueeStart();

            if (Input.GetMouseButtonUp(0))
                MarqueeFinish();

            if (Input.GetMouseButton(0))
                MarqueeUpdate();

            if (Input.GetMouseButtonDown(1))
                RightMouseClick();
        }
    }

    void MarqueeStart()
    {
        // clear previous selection if "shift" is not held down
        if (!Input.GetKey(Keymap.select.Inclusive))
            UnselectUnits();

        //Check if the player just wants to add/remove an unit
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, marqueeLayer) &&
                hit.collider.CompareTag("selectable"))
        {
            marqueeStarted = false;

            if (Input.GetKey(Keymap.select.Alternative))
                SelectedUnits.AddRange(FindSelectableUnits(hit.collider.name));
            else
                SelectedUnits.Add(hit.collider.gameObject);

            SelectUnits();
        }
        else
        {
            marqueeStarted = true;

            // initiate selection marquee
            float _invertedY = Screen.height - Input.mousePosition.y;
            marqueeOrigin = new Vector2(Input.mousePosition.x, _invertedY);
        }
    }

    void MarqueeUpdate()
    {
        if (marqueeStarted)
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
    }

    void MarqueeFinish()
    {
        if (marqueeStarted)
        {
            //Poppulate the selectableUnits array with all the selectable units that exist
            foreach (GameObject unit in FindSelectableUnits())
            {
                //Convert the world position of the unit to a screen position and then to a GUI point
                Vector3 _screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
                Vector2 _screenPoint = new Vector2(_screenPos.x, Screen.height - _screenPos.y);

                //Ensure that any units not within the marquee are currently unselected
                if (marqueeRect.Contains(_screenPoint) || backupRect.Contains(_screenPoint))
                    SelectedUnits.Add(unit);
            }

            SelectUnits();
        }
        
        //Reset the marquee so it no longer appears on the screen.
        marqueeStarted = false;
        marqueeRect.width  = 0;
        marqueeRect.height = 0;
        backupRect.width   = 0;
        backupRect.height  = 0;
        marqueeSize = Vector2.zero;
    }

    void RightMouseClick()
    {
        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            Mathf.Infinity,
            marqueeLayer
        ))
        {
            // right click over an enemy unit
            if (hit.collider.CompareTag("enemy"))
            {
                AttackRing(hit.collider.transform.position);
                AttackUnit(hit.collider.gameObject);
            }
            // right click over the ground
            else
            {
                MovementRing(hit.point);
                MoveUnits(hit.point);
            }
        }
    }

    List<GameObject> FindSelectableUnits(string name)
    {
        List<GameObject> selectable = FindSelectableUnits();

        for (int i = 0; i < selectable.Count; i++)
            if (selectable[i].name != name)
            {
                selectable.RemoveAt(i);
                i--;
            }

        return selectable;
    }
    
    List<GameObject> FindSelectableUnits()
    {
        return new List<GameObject>(
            GameObject.FindGameObjectsWithTag("selectable")
        );
    }

    public void UnselectUnits ()
    {
        HUD.CleanAction();

        AttackRing(false);
        MovementRing(false);

        // remove previous selection
        foreach ( GameObject unit in SelectedUnits )
            unit.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);
        
        SelectedUnits.Clear();
    }

    public void SelectUnits (GameObject unit)
    {
        List<GameObject> units = new List<GameObject>();
        units.Add(unit);

        SelectUnits(units);
    }

    public void SelectUnits ( List<GameObject> _units )
    {
        if (_units != null && _units.Count > 0)
        {
            if (!Input.GetKey(Keymap.select.Inclusive))
                UnselectUnits();

            SelectedUnits.AddRange(_units);
            SelectUnits();
        }
    }

    void SelectUnits ()
    {
        // if at least on zombie is selected, make him to moan
        if (SelectedUnits.Count > 0)
        {
            SelectedUnits[0]
                .GetComponent<ZombieAudioController>()
                .Moan();

            // if zombie selected is moving, show its destination
            if (SelectedUnits[0].GetComponent<UnitController>().State() == UnitState.moving)
                MovementRing(SelectedUnits[0].GetComponent<NavMeshAgent>().destination);
            // hide MovementRing otherwise
            else
                MovementRing(false);
        }

        // add new selection
        foreach ( GameObject unit in SelectedUnits )
            unit.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
    }

    public void AttackUnit ( GameObject _target )
    {
        // if at least on zombie is selected, make him scream
        if (SelectedUnits.Count > 0)
        {
            SelectedUnits[0]
                .GetComponent<ZombieAudioController>()
                .Rage();

            AttackRing(_target);
        }

        foreach ( GameObject unit in SelectedUnits )
            unit
                .GetComponent<UnitController>()
                .MoveAndAttack(_target);
    }

    public void MoveUnits ( Vector3 _target )
    {
        int row    = 0;
        int column = 0;

        foreach ( GameObject unit in SelectedUnits )
        {
            // offset from _target of units, when they move towards it
            Vector3 target = _target;

            target.x += ( column % 2 == 0 ? 2 : -2 ) * column;
            target.z += -2 * row;

            if ( ++ column == 5 )
            {
                column = 0;
                row ++;
            }

            UnitController current = unit.GetComponent<UnitController>();
            
            // Force movement to override attacking
            current.hold = false;
            unit.GetComponent<Mob>().Target = null;
            current.Move( target );
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
            unit
                .GetComponent<UnitController>()
                .Stop();
    }

    public void ResumeUnits ( )
    {
        foreach ( GameObject unit in SelectedUnits )
            unit
                .GetComponent<UnitController>()
                .Resume();
    }

    void MovementRing( bool _active )
    {
        movementRing.SetActive(_active);
    }

    void MovementRing(Vector3 _position)
    {
        AttackRing(false);

        movementRing.transform.position = _position + Vector3.up;
        movementRing.SetActive(true);
    }

    void AttackRing(Vector3 _position)
    {
        MovementRing(false);

        attackRing.transform.position = _position + Vector3.up;
        attackRing.SetActive(true);
        attackRing.GetComponent<Animator>().SetTrigger("active");

        StartCoroutine(AttackRing(false));
    }

    IEnumerator AttackRing(bool _active)
    {
        yield return new WaitForSeconds(2.5f);

        if (_active)
        {
            attackRing.SetActive(true);
            attackRing.GetComponent<Animator>().SetTrigger("active");
        }
        else
        {
            attackRing.SetActive(false);
        }
    }

}

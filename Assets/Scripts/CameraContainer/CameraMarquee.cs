using UnityEngine;
using System.Collections.Generic;

public class CameraMarquee : MonoBehaviour
{
	public Rect marqueeRect;
	public Texture marqueeGraphics;

	public List<GameObject> SelectableUnits;
	private List<GameObject> SelectedUnits;

	private Vector2 marqueeOrigin;
	private Vector2 marqueeSize;
	private Rect backupRect;

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
		if ( Input.GetMouseButtonDown(0) )
		{
			SelectedUnits.Clear();

			//Poppulate the selectableUnits array with all the selectable units that exist
			SelectableUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("selectable"));

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

		if ( Input.GetMouseButtonUp(0) )
		{
            foreach (GameObject unit in SelectableUnits)
            {
                // ignore caracter selected as a first click

                if ( SelectedUnits.Count > 0 && unit.GetInstanceID() == SelectedUnits[0].GetInstanceID() ) {
                    continue;
                }

                //Convert the world position of the unit to a screen position and then to a GUI point
                Vector3 _screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
                Vector2 _screenPoint = new Vector2(_screenPos.x, Screen.height - _screenPos.y);
                
                //Ensure that any units not within the marquee are currently unselected
                if (!marqueeRect.Contains(_screenPoint) || !backupRect.Contains(_screenPoint))
                {
                    unit.SendMessage("OnUnselected", SendMessageOptions.DontRequireReceiver);
                }
                if (marqueeRect.Contains(_screenPoint) || backupRect.Contains(_screenPoint))
                {
                    unit.SendMessage("OnSelected", SendMessageOptions.DontRequireReceiver);
                    SelectedUnits.Add (unit);
                }
            }

			//Reset the marquee so it no longer appears on the screen.
			marqueeRect.width = 0;
			marqueeRect.height = 0;
			backupRect.width = 0;
			backupRect.height = 0;
			marqueeSize = Vector2.zero;
		}

		if ( Input.GetMouseButton(0) )
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

				foreach ( GameObject unit in SelectedUnits )
				{
                    Debug.Log (unit);
                    // Force movement to override attacking
					unit.GetComponent<UnitMovement>().Move( target, true );
				}	
			}
		}
	}
}

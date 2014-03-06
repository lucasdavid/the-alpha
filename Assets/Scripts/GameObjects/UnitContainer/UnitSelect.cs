using UnityEngine;
using System.Collections;

public class UnitSelect : MonoBehaviour {
    // Temporarily sticking health bar in selected units
    //private Rect OverlayRect = new Rect();

	public bool selected;
	
    //private float m_OverlayWidth = 100.0f;
    //public Texture healthTexture = HealthBar.GetHealthTexture();

    /*
    void Update() {
        Vector3 centerPoint = Camera.main.WorldToScreenPoint (transform.position);

        OverlayRect.xMin = centerPoint.x - (m_OverlayWidth/2.0f);
        OverlayRect.xMax = centerPoint.x + (m_OverlayWidth/2.0f);
        OverlayRect.yMax = Screen.height - (centerPoint.y + 100);
        OverlayRect.yMin = Screen.height - (centerPoint.y + 90);
    }
*/

    /* Really sloppy, but it works for now
    void OnGUI() {
        if (selected) {
            Vector3 centerPoint = Camera.main.WorldToScreenPoint (transform.position);

            OverlayRect.x = centerPoint.x - (m_OverlayWidth/2.0f);
            OverlayRect.y = Screen.height - (centerPoint.y + 70);

            // Move it up a little for the Alpha
            if (GetComponent<Mob>().Name == "Alpha")
                OverlayRect.y = Screen.height - (centerPoint.y + 120);


            OverlayRect.width = m_OverlayWidth;
            OverlayRect.height = 20.0f;
            GUI.Box(OverlayRect, GetComponent<Mob>().Health + "/" + GetComponent<Mob>().MaxHealth);
            //GUI.Box(new Rect(10.0f, 10.0f, 100.0f, 20.0f), GetComponent<Mob>().Health + "/" + GetComponent<Mob>().MaxHealth);
        }
        //    GUI.DrawTexture(OverlayRect, healthTexture);
    }*/

	private void OnSelected()
	{
		selected = true;
		//gameObject.GetComponentInChildren<Light>().enabled = true;
        gameObject.GetComponentInChildren<HealthBar>().Enable();
		//renderer.material.color = Color.red;
	}
	
	private void OnUnselected()
	{
		selected = false;
		//renderer.material.color = Color.white;
		//gameObject.GetComponentInChildren<Light>().enabled = false;
        gameObject.GetComponentInChildren<HealthBar>().Disable();
	}
}

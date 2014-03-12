using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DebugWindow : MonoBehaviour {
    bool open;
    string bpText = "0";
    string tlText = "0";
    int bp, tl, cv; // Brain Points, Threat Level, CurrentValue

    Mob alpha;
    GameObject hudCamera;

	void Start ()
    {
        open = false;
        hudCamera = GameObject.Find("_camera-hud");
	}
	
	void Update ()
    {
        // Toggle menu
	    if (Input.GetKeyDown(KeyCode.BackQuote)) 
            open = !open;

        if (open && Input.GetKeyDown(KeyCode.Escape))
            open = false;

        if (open && Input.GetKeyDown(KeyCode.Return))
            open = false;
	}

    void OnGUI()
    {
        float left = Screen.width / 3;
        float top = Screen.height / 4;
        float boxWidth = Screen.width / 3;
        float boxHeight = Screen.height / 3;
        float offset = 10.0f;

        if ( open )
        {
            bp = Horde.BrainPoints;

            GUI.Box(new Rect(left, top, boxWidth, boxHeight), "Debug Menu");

            GUI.Label (new Rect (left + offset, top + offset + 20, boxWidth - offset, boxHeight - offset), "BP:");
            bpText = GUI.TextField(new Rect(left + offset * 7, top + offset + 20, 50, 20), bp.ToString(), 6);
            bpText = Regex.Replace(bpText, @"[^0-9]", "");

            if (int.TryParse(bpText, out bp))
                Horde.BrainPoints = bp;

            if (GUI.Button(new Rect(left + offset * 4, top + offset + 20, 20, 20), "+"))
                Horde.BrainPoints++;
            if (GUI.Button(new Rect(left + offset * 13, top + offset + 20, 20, 20), "-"))
                Horde.BrainPoints--;

            // thread level
            tl = Horde.ThreatLevel;
            GUI.Label (new Rect (left + offset, top + offset + 40, boxWidth - offset, boxHeight - offset), "TL:");
            tlText = GUI.TextField(new Rect(left + offset * 7, top + offset + 40, 50, 20), tl.ToString(), 6);
            tlText = Regex.Replace(tlText, @"[^0-9]", "");
            
            if (int.TryParse(tlText, out tl))
                Horde.ThreatLevel = tl;
            
            if (GUI.Button(new Rect(left + offset * 4, top + offset + 40, 20, 20), "+"))
                Horde.ThreatLevel++;
            if (GUI.Button(new Rect(left + offset * 13, top + offset + 40, 20, 20), "-"))
                Horde.ThreatLevel--;

            // current value
            GUI.Label (new Rect (left + offset, top + offset + 60, boxWidth - offset, boxHeight - offset), "CV: " + Horde.CurrentValue);

            // enemy value
            GUI.Label (new Rect (left + offset, top + offset + 80, boxWidth - offset, boxHeight - offset), "EV: " + Humans.CurrentValue);

            if ( GUI.Button(new Rect(left + offset, top + offset + 100, 120, 20), "Enable/disable HUD"))
                hudCamera.SetActive( !hudCamera.activeInHierarchy );
        }
    }
}

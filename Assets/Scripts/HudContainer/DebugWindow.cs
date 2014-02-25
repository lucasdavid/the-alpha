using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DebugWindow : MonoBehaviour {
    bool open;
    string text = "0";
    int bp;

	// Use this for initialization
	void Start () {
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Toggle menu
	    if (Input.GetKeyDown(KeyCode.BackQuote)) 
            open = !open;

        if (open && Input.GetKeyDown(KeyCode.Escape))
            open = false;

        if (open && Input.GetKeyDown(KeyCode.Return))
            open = false;
	}

    void OnGUI() {
        float left = Screen.width / 3;
        float top = Screen.height / 4;
        float boxWidth = Screen.width / 3;
        float boxHeight = Screen.height / 3;
        float offset = 10.0f;

        if (open) {
            // BRAINPOINT STUFF
            bp = BP.BrainPoints;

            GUI.Box(new Rect(left, top, boxWidth, boxHeight), "Debug Menu");
            GUI.Label (new Rect (left + offset, top + offset, boxWidth - offset, boxHeight - offset), "BP:");
            text = GUI.TextField(new Rect(left + offset * 7, top + offset, 50, 20), bp.ToString(), 6);
            text = Regex.Replace(text, @"[^0-9]", "");

            if (int.TryParse(text, out bp)) {
                BP.BrainPoints = bp;
            }

            if (GUI.Button(new Rect(left + offset * 4, top + offset, 20, 20), "+"))
                BP.BrainPoints++;
            if (GUI.Button(new Rect(left + offset * 13, top + offset, 20, 20), "-"))
                BP.BrainPoints--;

            // OTHER STUFF ?
        }
    }
}

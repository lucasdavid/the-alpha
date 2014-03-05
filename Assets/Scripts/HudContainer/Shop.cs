using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {
    public static bool open;

    // This shop to be replaced with real graphics

	// Use this for initialization
	void Start () {
        open = false;	
	}
	
	// Update is called once per frame
	void OnGUI () {
        if (GUI.Button(new Rect(10, Screen.height - 110, 200, 20), "[O] Zombie Shop"))
            open = !open;

        // ACTUAL STUFF HERE
        if (open) {
            float left = 10.0f;
            float top = Screen.height - 260.0f;
            float boxWidth = 200.0f;
            float offset = 5.0f;

            GUI.Box(new Rect(left, top, boxWidth, 140.0f), "Zombie Shop");

            // Clean up
            if (GUI.Button(new Rect(left + offset, top + 20.0f, boxWidth - 2 * offset, 20), "[Q] Basic Zombie - 1BP")) {
                GetComponent<CharacterSpawn>().Spawn(0);
            }
            if (GUI.Button(new Rect(left + offset, top + 50.0f, boxWidth - 2 * offset, 20), "[W] Scout Zombie - 1BP")) {
                GetComponent<CharacterSpawn>().Spawn(1);
            }
            if (GUI.Button(new Rect(left + offset, top + 80.0f, boxWidth - 2 * offset, 20), "[E] Soldier Zombie - 2BP")) {
                GetComponent<CharacterSpawn>().Spawn(2);
            }
            if (GUI.Button(new Rect(left + offset, top + 110.0f, boxWidth - 2 * offset, 20), "[R] Tank Zombie - 2BP")) {
                GetComponent<CharacterSpawn>().Spawn(3);
            }

        }
	}
}

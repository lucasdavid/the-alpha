using UnityEngine;
using System.Collections;

public class Alpha : MonoBehaviour {
    bool _lose;
    float _healTimer;               // Heal timer
    static GameObject theAlpha;

    int tutorial;

    public static GameObject GetAlpha() {
        return theAlpha;
    }

	// Use this for initialization
	void Start () {
        _healTimer = 0;
        theAlpha = gameObject;
        tutorial = 0;
	}

	// Update is called once per frame
	void Update () {
        _healTimer -= Time.deltaTime;

        // Heal 1 HP every second until back to max
        if (_healTimer <= 0) {
            _healTimer = 1.0f;
            if (GetComponent<Mob>().Health < GetComponent<Mob>().MaxHealth)
                GetComponent<Mob>().Health++;
        }

        // If the Alpha dies, you lose!
	    if (GetComponent<Mob> ().Health <= 0) {
            _lose = true;
            StartCoroutine(Wait());
        }

        if (tutorial == 1 && GetComponent<Mob>().Health <= 600) {
            StartCoroutine(HealTutorial());
            tutorial++;
        }
	}

    IEnumerator HealTutorial() {
        Camera.main.GetComponent<HintController>().Add("Looks like your Alpha is a bit hurt...");
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("Send units back to the Graveyard to heal faster!");
    }

    IEnumerator Wait() {
        Camera.main.transform.LookAt(transform.position);
        Camera.main.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);

        yield return new WaitForSeconds (10.0f);
        Application.LoadLevel("credits");
    }

    void OnGUI() {
        if (_lose) {
            string Lose = "Your Alpha has died. You lose!";
            GUI.Label (new Rect(Screen.width/2, Screen.height/3, 300.0f, 20.0f), Lose);
        }
    }

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0) && tutorial == 0) {
            tutorial++;
            Camera.main.GetComponent<HintController>().Add("Now, find the nearest human and right click on him.");
        }
    }
}

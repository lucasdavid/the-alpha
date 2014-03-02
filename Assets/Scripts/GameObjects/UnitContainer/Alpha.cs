using UnityEngine;
using System.Collections;

public class Alpha : MonoBehaviour {
    bool _lose;
    float _healTimer;               // Heal timer
    static GameObject theAlpha;

    public static GameObject GetAlpha() {
        return theAlpha;
    }

	// Use this for initialization
	void Start () {
        _healTimer = 0;
        theAlpha = gameObject;
	}

	// Update is called once per frame
	void Update () {
        _healTimer -= Time.deltaTime;

        // Heal 1 HP every 5 seconds until back to 200 hp (change static number).
        if (_healTimer <= 0) {
            _healTimer = 5.0f;
            if (GetComponent<Mob>().Health < 200)
                GetComponent<Mob>().Health++;
        }

        // If the Alpha dies, you lose!
	    if (GetComponent<Mob> ().Health <= 0) {
            _lose = true;
            StartCoroutine(Wait());
        }
	}

    IEnumerator Wait() {
        yield return new WaitForSeconds (2.5f);
        Time.timeScale = 0;
    }

    void OnGUI() {
        if (_lose) {
            string Lose = "You Lose!";
            GUI.Label (new Rect(Screen.width/2, Screen.height/3, 100.0f, 20.0f), Lose);
        }

    }
}

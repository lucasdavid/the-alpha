using UnityEngine;
using System.Collections;

public class Alpha : MonoBehaviour {
    bool _lose;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

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

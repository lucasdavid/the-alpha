using UnityEngine;
using System.Collections;

public class King : MonoBehaviour {
    bool _win;
	
	void Start () {
	
	}
	
	void Update () {
        // If the King dies, you win!
        if (GetComponent<Mob> ().Health <= 0) {
            _win = true;
            StartCoroutine(Wait());
        }
	}

    
    IEnumerator Wait() {
        Camera.main.transform.LookAt(transform.position);
        Camera.main.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
        
        yield return new WaitForSeconds (10.0f);
        Time.timeScale = 0;
        Application.LoadLevel("credits");
    }
    
    void OnGUI() {
        if (_win) {
            string Lose = "You have defeated the King. You win!";
            GUI.Label (new Rect(Screen.width/2, Screen.height/3, 300.0f, 20.0f), Lose);
        }
        
    }
}

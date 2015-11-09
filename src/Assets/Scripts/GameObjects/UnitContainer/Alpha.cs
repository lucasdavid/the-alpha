using UnityEngine;
using System.Collections;

public class Alpha : MonoBehaviour {
    bool _lose;
    float _healTimer;               // Heal timer
    static GameObject theAlpha;

    int tutorial;

    public static GameObject GetAlpha()
    {
        return theAlpha;
    }

	void Start ()
    {
        _healTimer = 0;
        theAlpha = gameObject;
        tutorial = 0;
	}

	// Update is called once per frame
	void Update ()
    {
        _healTimer -= Time.deltaTime;

        // Heal 1 HP every second until back to max
        if (_healTimer <= 0) {
            _healTimer = 1.0f;
            if (GetComponent<Mob>().Health < GetComponent<Mob>().MaxHealth)
                GetComponent<Mob>().Health++;
        }

        if (tutorial == 1 && GetComponent<Mob>().Health <= 600) {
            StartCoroutine(HealTutorial());
            tutorial++;
        }
	}

    IEnumerator HealTutorial()
    {
        Camera.main.GetComponent<HintController>().Add("Looks like your Alpha is a bit hurt...");
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("Send units back to the Graveyard to heal faster!");
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && tutorial == 0)
        {
            tutorial++;
            Camera.main.GetComponent<HintController>().Add("Now, find the nearest human and right click on him.");
        }
    }
}

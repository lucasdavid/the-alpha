using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HintController : MonoBehaviour {

    public AudioSource source;
    public float messageLifeTime;

    public static int step;

    LinkedList<string> messages;

	void Start ()
    {
        step = 0;
        messages = new LinkedList<string>();
        StartCoroutine(Begin());
	}

    void OnGUI()
    {
        if (messages.Count > 0)
        {
            int index = 0;

            foreach (string message in messages)
                GUI.Label(new Rect(30, (Screen.height / 2 + 50) + (20 * index++), 400, 20), message);
        }
    }

    public void EndTutorial() {
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("Only the Alpha Zombie has this special ability.");
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("If you wish to purchase even more zombies, press \"o\".");
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("This allows you to spawn even more zombies at the graveyard.");
        yield return new WaitForSeconds(4.0f);
        Camera.main.GetComponent<HintController>().Add("Go kill some more humans!");
    }

	IEnumerator Begin() {
        yield return new WaitForSeconds(2.0f);
        Add ("Welcome to The Alpha.");
        yield return new WaitForSeconds(4.0f);
        Add ("To begin, click on the Alpha Zombie.");
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(messageLifeTime);
        messages.RemoveFirst();
    }

    public HintController Add(string _message)
    {
        messages.AddLast(_message);
        source.Play();
        StartCoroutine(Fade());

        return this;
    }

    public HintController Flush()
    {
        messages.Clear();

        return this;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HintController : MonoBehaviour {

    public AudioSource source;
    public float messageLifeTime;

    LinkedList<string> messages;

	void Start ()
    {
        messages = new LinkedList<string>();
	}

    void OnGUI()
    {
        if (messages.Count > 0)
        {
            int index = 0;

            foreach (string message in messages)
                GUI.Label(new Rect(Screen.width - 100, Screen.height / 3 + 20 * index++, 100, 20), message);
        }
    }
	
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(messageLifeTime);
        messages.RemoveFirst();
    }

    public void Add(string _message)
    {
        messages.AddLast(_message);
        source.Play();
        StartCoroutine(Fade());
    }
}

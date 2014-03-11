using UnityEngine;
using System.Collections;

public class CreditController : MonoBehaviour {

    public bool skip;

    public void Start()
    {
        StartCoroutine("CanSkip");
    }

    public void Update()
    {
        if (Input.anyKey || skip) {
            Application.LoadLevel("main-menu");
        }
    }

    IEnumerator CanSkip()
    {
        yield return new WaitForSeconds(40f);
        skip = true;
    }

}

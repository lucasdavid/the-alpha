using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            animator.SetTrigger("exit");
            StartCoroutine("StartGame");
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevelAsync("final-game-scene");
    }

}

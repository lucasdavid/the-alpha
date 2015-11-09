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
        }
    }

    public void StartGame()
    {
        Application.LoadLevel("final-game-scene");
    }

}

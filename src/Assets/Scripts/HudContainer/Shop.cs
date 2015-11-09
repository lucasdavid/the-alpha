using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

    static bool open;

    public float cooldown;
    
    Animator anim;
    TextMesh shopText;
    
    CharacterSpawn spawner;

    void Start ()
    {
        //spawningList = new List<CharacterSpawn.type>();

        anim     = GetComponent<Animator>();
        shopText = GetComponentInChildren<TextMesh>();
        spawner  = Camera.main.GetComponent<CharacterSpawn>();

        shopText.text =
            "1 Basic\n" + 
            "2 Scout\n" + 
            "3 Soldier\n" + 
            "4 Tank";
	}

    void Update ()
    {
        if ( Input.GetKeyDown( Keymap.shop.Interact ) )
        {
            Interact();

            if (open)
            {
                StopCoroutine("Close");
                StartCoroutine("Close");
            }
        }

        if ( open )
        {
            if (Input.GetKeyDown(Keymap.spawn.Basic))
            {
                spawner.Spawn((int)CharacterSpawn.type.basic);
                StopCoroutine("Close");
                StartCoroutine("Close");
            }

            if (Input.GetKeyDown(Keymap.spawn.Scout))
            {
                spawner.Spawn((int)CharacterSpawn.type.scout);
                StopCoroutine("Close");
                StartCoroutine("Close");
            }

            if (Input.GetKeyDown(Keymap.spawn.Soldier))
            {
                spawner.Spawn((int)CharacterSpawn.type.soldier);
                StopCoroutine("Close");
                StartCoroutine("Close");
            }

            if (Input.GetKeyDown(Keymap.spawn.Tank))
            {
                spawner.Spawn((int)CharacterSpawn.type.tank);
                StopCoroutine("Close");
                StartCoroutine("Close");
            }
        }
    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(5f);
        Interact();
    }

    public void Interact()
    {
        StopCoroutine("Close");
        anim.SetBool("open", open = !open);
    }

    public static bool IsOpen() { return open; }

}

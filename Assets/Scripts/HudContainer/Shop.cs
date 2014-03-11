using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public static bool open;

    Animator anim;
    CharacterSpawn spawner;

    void Start ()
    {
        open    = false;
        anim    = GetComponent<Animator>();
        spawner = Camera.main.GetComponent<CharacterSpawn>();
	}

    void Update ()
    {
        if ( Input.GetKeyDown( Keymap.shop.Interact ) )
        {
            anim.SetTrigger("interact");
            open = ! open;
        }

        if ( open )
        {
            if (Input.GetKeyDown (Keymap.spawn.Basic))
                spawner.Spawn ((int) CharacterSpawn.type.basic);
            
            if (Input.GetKeyDown (Keymap.spawn.Scout))
                spawner.Spawn ((int) CharacterSpawn.type.scout);
            
            if (Input.GetKeyDown (Keymap.spawn.Soldier))
                spawner.Spawn ((int) CharacterSpawn.type.soldier);
            
            if (Input.GetKeyDown (Keymap.spawn.Tank))
                spawner.Spawn ((int) CharacterSpawn.type.tank);
        }
    }

    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Camera.main.GetComponent<HintController>().Add ("Use keys 1-4 to purchase units.");
        }
    }
}

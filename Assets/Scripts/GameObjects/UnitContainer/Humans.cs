using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humans : MonoBehaviour {
    public Mob[] characters;
    private static int _currentValue;          // How much value the player has on the field
    private static Vector3 _spawnPoint;

    public static int CurrentValue
    {
        get { return _currentValue; }
        set { _currentValue = value; }
    }

    public static Vector3 SpawnPoint
    {
        get { return _spawnPoint; }
        set { _spawnPoint = value; }
    }

    GameObject[] FindGameObjectsWithLayer (int layer) {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0) {
            return null;
        }
        return goList.ToArray();
    }

	// Use this for initialization
	void Start () {
        CurrentValue = 0;
 
        // Find all human units on the field
        GameObject[] all = FindGameObjectsWithLayer(8);
        foreach (GameObject g in all) {
            if (g.GetComponent<Mob>().Alliance == 1)
                CurrentValue += g.GetComponent<Mob>().Value;
        }

        for (int i = 0; i < characters.Length; i++)
            characters[i].CreatePool();
	}

    public void SpawnHuman(int index) {
        ObjectPool.Spawn (characters [index], SpawnPoint);
    }
}

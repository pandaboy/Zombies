using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    // Prefabs
    public GameObject[] zombiePrefabs;
    public GameObject[] citizenPrefabs;
    
    // spawnlocations
    public Transform[] zombieLocations;
    public Transform[] citizenLocations;

    // counters
    private int rescued = 0;
    private int zombified = 0;

	void Start ()
    {
        // spawn the zombies
        if (zombiePrefabs.Length > 0)
        {
            for(int i = 0; i < zombieLocations.Length; i++) {
                Instantiate(zombiePrefabs[(i % zombiePrefabs.Length)], zombieLocations[i].position, Quaternion.identity);
            }
        }

        // spawn the citizens
        if (citizenPrefabs.Length > 0)
        {
            for (int i = 0; i < citizenLocations.Length; i++) {
                Instantiate(citizenPrefabs[(i % citizenPrefabs.Length)], citizenLocations[i].position, Quaternion.identity);
            }
        }
	}

    public int Zombified
    {
        set
        {
            this.zombified = value;
        }

        get
        {
            return this.zombified;
        }
    }

    public int Rescued
    {
        set
        {
            this.rescued = value;
        }

        get
        {
            return this.rescued;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

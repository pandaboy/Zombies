using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RelationshipGraph;
using Zombies;

public class GameController : MonoBehaviour
{
    // UI.Text to display information to
    public Text dialogText;

    // Player information
    public GameObject playerPrefab;
    public Transform playerLocation;
    private GameObject player;

    // Zombies, Items and Citizens
    public GameObject[] zombiePrefabs;
    public Transform[] zombieLocations;
    public GameObject[] itemPrefabs;
    public Transform[] itemLocations;
    public GameObject[] citizenPrefabs;
    public Transform[] citizenLocations;

    // counters
    private int rescued = 0;
    private int zombified = 0;
    private int items = 0;

    // RelationshipGraph Stuff
    private ZombieGraph graph = ZombieGraph.Instance;

    // relationships in use
    private Relationship trust = new Relationship(Zombies.RelationshipType.TRUST);
    private Relationship distrust = new Relationship(Zombies.RelationshipType.DISTRUST);

	void Start ()
    {
        // spawn the player
        player = Instantiate(playerPrefab, playerLocation.position, playerLocation.rotation) as GameObject;

        // spawn the zombies
        if (zombiePrefabs.Length > 0 && zombieLocations.Length > 0)
        {
            for(int i = 0; i < zombieLocations.Length; i++) {
                Instantiate(
                    zombiePrefabs[(i % zombiePrefabs.Length)],
                    zombieLocations[i].position,
                    zombieLocations[i].rotation
                );
            }
        }

        // spawn the citizens
        if (citizenPrefabs.Length > 0 && citizenLocations.Length > 0)
        {
            for (int i = 0; i < citizenLocations.Length; i++) {
                GameObject c = Instantiate(
                    citizenPrefabs[(i % citizenPrefabs.Length)],
                    citizenLocations[i].position,
                    citizenLocations[i].rotation
                ) as GameObject;
                c.GetComponent<ClickCharacter>().GC = this;
                c.GetComponent<Actor>();
                graph.AddDirectConnection(new Connection(c, player, trust));
            }
        }

        // spawn the items
        if (itemPrefabs.Length > 0 && itemLocations.Length > 0)
        {
            for (int i = 0; i < itemLocations.Length; i++)
            {
                Instantiate(
                    itemPrefabs[(i % itemPrefabs.Length)],
                    itemLocations[i].position,
                    itemLocations[i].rotation
                );
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

    public int Items
    {
        set
        {
            this.items = value;
        }

        get
        {
            return this.items;
        }
    }

    void buildRelationships()
    {
        // set up relationships using the ZombieGraph here
    }

    public void UpdateDialog(string msg)
    {
        dialogText.text = msg;
    }
}

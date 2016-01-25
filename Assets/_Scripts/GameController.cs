using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RelationshipGraph;
using Zombies;

public class GameController : MonoBehaviour
{
    // UI.Text to display information to
    public Text dialogText; // displays actor dialog
    public Text infoText;   // displays actor information (relationships)
    public Text relationshipText; // displays the players relationships

    // Player stuff
    public GameObject playerPrefab;
    public Transform playerLocation;
    private GameObject player;
    private Actor playerActor;

    // Generic 'Actors' that other actors can be a member of
    // - we use this to define a relationship to a group
    public GameObject groupPrefab;
    private GameObject _group;
    public GameObject Group
    {
        get
        {
            return _group;
        }

        set
        {
            _group = value;
        }
    }

    // Zombies, Items and Citizens
    public GameObject[] zombiePrefabs;
    public Transform[] zombieLocations;
    public GameObject[] itemPrefabs;
    public Transform[] itemLocations;
    public GameObject[] citizenPrefabs;
    public Transform[] citizenLocations;

    // counters
    private int zombified = 0;
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

    private int rescued = 0;
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

    private int items = 0;
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

    // RelationshipGraph Stuff
    private ZombieGraph _graph = ZombieGraph.Instance;
    public ZombieGraph Graph
    {
        get
        {
            return this._graph;
        }
    }

    // Main Camera
    private GameObject mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

	void Start ()
    {
        // set the "authority" group
        Group = Instantiate(groupPrefab) as GameObject;

        // spawn the player
        player = Instantiate(playerPrefab, playerLocation.position, playerLocation.rotation) as GameObject;
        mainCamera.GetComponent<CameraFollow>().Target = player.transform;
        playerActor = player.GetComponent<Actor>();

        // spawn the zombies
        if (zombiePrefabs.Length > 0 && zombieLocations.Length > 0) {
            for(int i = 0; i < zombieLocations.Length; i++) {
                Instantiate(
                    zombiePrefabs[(i % zombiePrefabs.Length)],
                    zombieLocations[i].position,
                    zombieLocations[i].rotation
                );
            }
        }

        // spawn the citizens
        if (citizenPrefabs.Length > 0 && citizenLocations.Length > 0) {
            for (int i = 0; i < citizenLocations.Length; i++) {
                GameObject cgo = Instantiate(
                    citizenPrefabs[(i % citizenPrefabs.Length)],
                    citizenLocations[i].position,
                    citizenLocations[i].rotation
                ) as GameObject;

                // create a 'stranger' relationship with this other character
                _graph.AddDirectConnection(
                    new Connection(
                        cgo.GetComponent<Actor>(),
                        Group.GetComponent<Actor>(),
                        Zombies.RelationshipType.TRUST
                    )
                );
            }
        }

        // spawn the items
        if (itemPrefabs.Length > 0 && itemLocations.Length > 0) {
            for (int i = 0; i < itemLocations.Length; i++)
            {
                GameObject igo = Instantiate(
                    itemPrefabs[(i % itemPrefabs.Length)],
                    itemLocations[i].position,
                    itemLocations[i].rotation
                ) as GameObject;
                igo.GetComponent<Actor>().actorType = ActorType.ITEM;
                _graph.AddDirectConnection(
                    new Connection(
                        igo.GetComponent<Actor>(),
                        Group.GetComponent<Actor>(),
                        Zombies.RelationshipType.MEMBER
                    )
                );
            }
        }
	}

    public void SetDialogText(string msg)
    {
        dialogText.text = msg;
    }

    public void SetInfoText(string msg)
    {
        infoText.text = msg;
    }

    public void SetRelationshipText(string msg)
    {
        relationshipText.text = msg;
    }
}

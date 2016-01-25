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

    // Player information
    public GameObject playerPrefab;
    public Transform playerLocation;
    private GameObject player;
    private Actor playerActor;

    // Generic 'Actors' that other actors can be a member of
    public GameObject authorityGroupPrefab;
    private GameObject authorityGroup;
    public GameObject AuthorityGroup
    {
        get
        {
            return authorityGroup;
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
    private int rescued = 0;
    private int zombified = 0;
    private int items = 0;

    // RelationshipGraph Stuff
    private ZombieGraph _graph = ZombieGraph.Instance;
    public ZombieGraph Graph
    {
        get
        {
            return this._graph;
        }
    }

    // relationships in use
    private Relationship trust = new Relationship(Zombies.RelationshipType.TRUST);
    private Relationship distrust = new Relationship(Zombies.RelationshipType.DISTRUST);
    private Relationship stranger = new Relationship(Zombies.RelationshipType.STRANGER);
    private Relationship member = new Relationship(Zombies.RelationshipType.MEMBER);

    // Main Camera
    private GameObject mainCamera;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

	void Start ()
    {
        // set the "authority" group
        authorityGroup = Instantiate(authorityGroupPrefab) as GameObject;

        // spawn the player
        player = Instantiate(playerPrefab, playerLocation.position, playerLocation.rotation) as GameObject;
        mainCamera.GetComponent<CameraFollow>().Target = player.transform;
        playerActor = player.GetComponent<Actor>();

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
                GameObject cgo = Instantiate(
                    citizenPrefabs[(i % citizenPrefabs.Length)],
                    citizenLocations[i].position,
                    citizenLocations[i].rotation
                ) as GameObject;

                // create a 'stranger' relationship with this other character
                _graph.AddDirectConnection(
                    new Connection(
                        cgo.GetComponent<Actor>(),
                        authorityGroup.GetComponent<Actor>(),
                        trust
                    )
                );
            }
        }

        // spawn the items
        if (itemPrefabs.Length > 0 && itemLocations.Length > 0)
        {
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
                        authorityGroup.GetComponent<Actor>(),
                        member
                    )
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

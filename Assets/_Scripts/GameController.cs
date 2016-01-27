using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RelationshipGraph;
using Zombies;

/// <summary>
/// Manages the Level. Loads gameObjects at spawn locations
/// </summary>
public class GameController : MonoBehaviour
{
    // UI.Text to display information to
    public Text titleText;          // game title text
    public Text dialogText;         // displays actor dialog
    public Text infoText;           // displays actor information (relationships)
    public Text relationshipText;   // displays the players relationships
    public Text healthText;         // displays the players health
    public Text itemsText;          // displays the number of items collected
    public Text citizensText;       // displays the number of citizes saved

    // Player stuff
    public GameObject    playerPrefab;
    public Transform     playerLocation;
    protected GameObject _player;
    protected Actor      _playerActor;

    // Exit stuff
    public GameObject   exitPrefab;
    public Transform    exitLocation;

    // Generic 'Actors' that other actors can be a member of
    // - we use this to define a relationship to a group
    public GameObject   groupPrefab;
    protected GameObject _group;
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
    public Transform[]  zombieLocations;
    public GameObject[] itemPrefabs;
    public Transform[]  itemLocations;
    public GameObject[] citizenPrefabs;
    public Transform[]  citizenLocations;

    // counters
    protected int _zombified = 0;
    public int Zombified
    {
        set
        {
            _zombified = value;
            if (_rescued < 0) {
                _rescued = 0;
            }
        }

        get
        {
            return _zombified;
        }
    }

    protected int _rescued = 0;
    public int Rescued
    {
        set
        {
            _rescued = value;
            if (_rescued < 0) {
                _rescued = 0;
            }
            citizensText.text = "Saved: " + _rescued;
        }

        get
        {
            return _rescued;
        }
    }

    protected int _items = 0;
    public int Items
    {
        set
        {
            _items = value;
            if (_items < 0) {
                _items = 0;
            }
            itemsText.text = "Items: " + _items;
        }

        get
        {
            return _items;
        }
    }

    // RelationshipGraph Stuff
    protected ZombieGraph _graph = ZombieGraph.Instance;
    public ZombieGraph Graph
    {
        get
        {
            return _graph;
        }
    }

    // Main Camera
    protected GameObject _mainCamera;

    // Level loading logic
    public string nextLevel = "";
    public Button nextLevelButton;
    public Button restartLevelButton;

    private float lerpTime = 0.0f;

	void Start ()
    {
        // find the MainCamera
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        // set the "authority" group
        Group = Instantiate(groupPrefab) as GameObject;

        // spawn the player
        _player = Instantiate(playerPrefab, playerLocation.position, playerLocation.rotation) as GameObject;
        _mainCamera.GetComponent<CameraFollow>().Target = _player.transform;
        _playerActor = _player.GetComponent<Actor>();

        // spawn the exit vehicle
        GameObject exitVehicle = Instantiate(exitPrefab, exitLocation.position, exitLocation.rotation) as GameObject;
        exitVehicle.GetComponent<DriveOff>().Player = _player;

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
                GameObject citizenGO = Instantiate(
                    citizenPrefabs[(i % citizenPrefabs.Length)],
                    citizenLocations[i].position,
                    citizenLocations[i].rotation
                ) as GameObject;

                // citizens will trust members of the GROUP
                _graph.AddDirectConnection(
                    new Connection(
                        citizenGO.GetComponent<Actor>(),
                        Group.GetComponent<Actor>(),
                        Zombies.RelationshipType.TRUST
                    )
                );
            }
        }

        // spawn weapons
        if (itemPrefabs.Length > 0 && itemLocations.Length > 0) {
            for (int i = 0; i < itemLocations.Length; i++) {
                Instantiate(
                    itemPrefabs[(i % itemPrefabs.Length)],
                    itemLocations[i].position,
                    itemLocations[i].rotation
                );
            }
        }
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            DisplayEndScreen("YOU ARE A ZOMBIE");
        }

        FadeDialogText();
    }

    public void SetTitleText(string msg)
    {
        titleText.text = msg;
    }

    public void SetDialogText(string msg)
    {
        dialogText.text = msg;
        dialogText.color = Color.white;
        lerpTime = 0.0f;
    }

    void FadeDialogText()
    {
        // fade out the dialogText
        if (lerpTime < 1.0f) {
            lerpTime += Time.deltaTime * 0.15f;
        }

        dialogText.color = Color.Lerp(Color.white, Color.clear, lerpTime);
    }

    public void SetInfoText(string msg)
    {
        infoText.text = msg;
    }

    public void SetRelationshipText(string msg)
    {
        relationshipText.text = msg;
    }

    public void SetHealthText(string msg)
    {
        healthText.text = msg;
    }

    public void NextLevel()
    {
        Application.LoadLevel(nextLevel);
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void DisplayEndScreen(string titleMsg = "Game Over")
    {
        titleText.text = titleMsg;
        titleText.enabled = true;
        nextLevelButton.GetComponentInChildren<Text>().enabled = true;
        restartLevelButton.GetComponentInChildren<Text>().enabled = true;

        // disable player controls
        _player.GetComponent<ClickToMoveTo>().enabled = false;
    }
}

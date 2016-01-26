using UnityEngine;
using System.Collections.Generic;
using Zombies;

public class DriveOff : MonoBehaviour
{
    public float speed = 10.0f;

    private ZombieGraph _graph;
    private GameController _gc;

    private Player _player;
    private GameObject _playerGO;
    public GameObject Player
    {
        set
        {
            _playerGO = value;
            _player = _playerGO.GetComponent<Player>();
        }

        get
        {
            return _playerGO;
        }
    }
    
    private bool drive_off;
    public bool Drive_Off
    {
        get
        {
            return drive_off;
        }

        set
        {
            drive_off = value;
        }
    }

	void Start ()
    {
        drive_off = false;
        _graph = ZombieGraph.Instance;
        _gc = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
	}
	
	void Update ()
    {
        if (drive_off) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    public void Escape()
    {
        // trigger the car to drive off
        drive_off = true;

        // get rid of the player
        Destroy(_playerGO);

        // get rid of the followers
        IList<Actor> followers = _player.GetFollowers();
        foreach (Actor a in followers) {
            Destroy(a.gameObject);
        }

        _gc.DisplayEndScreen("Success!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            Escape();
        }
    }
}

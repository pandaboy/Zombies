using UnityEngine;
using Zombies;
using System.Collections.Generic;

/// <summary>
/// Manages damage done to the gameObject by another.
/// </summary>
public class DriveOff : MonoBehaviour
{
    public float speed = 10.0f;

    protected ZombieGraph    _graph;
    protected GameController _gc;

    protected Player _player;
    protected GameObject _playerGO;
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

    protected bool _driveOff;
    public bool Drive_Off
    {
        get
        {
            return _driveOff;
        }

        set
        {
            _driveOff = value;
        }
    }

	void Start ()
    {
        Drive_Off = false;
        _graph = ZombieGraph.Instance;
        _gc = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
	}
	
	void Update ()
    {
        if (Drive_Off) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    public void Escape()
    {
        // trigger the car to drive off
        Drive_Off = true;

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

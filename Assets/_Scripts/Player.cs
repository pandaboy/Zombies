using UnityEngine;
using Zombies;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private GameController _gc;
    private ZombieGraph _graph;
    private Actor _actor;

    void Awake()
    {
        _graph = ZombieGraph.Instance;
        _gc = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
        _actor = GetComponent<Actor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC") {
            Actor otherActor = other.gameObject.GetComponent<Actor>();
            // create a 'stranger' relationship with this other character
            _graph.AddDirectConnection(new Connection(_actor, otherActor, RelationshipType.STRANGER));

            _actor.DisplayRelationships(true);
        }
    }
}
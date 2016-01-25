using UnityEngine;
using UnityEngine.UI;
using Zombies;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private GameController _gc;
    private ZombieGraph _graph;
    private Damage _damage;
    private Actor _actor;

    void Start()
    {
        _graph = ZombieGraph.Instance;
        _actor = GetComponent<Actor>();
        _damage = GetComponent<Damage>();
        
        _gc = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();

        // set the player health
        _gc.SetHealthText(_damage.health.ToString());
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

    void Update()
    {
        // check if health is below 0
        if (_damage.health <= 0)
        {
            // display title text
            _gc.SetTitleText("YOU ARE A ZOMBIE");
        }

        // update UI with new health value
        _gc.SetHealthText(_damage.health.ToString());
    }
}
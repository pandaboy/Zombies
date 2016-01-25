using UnityEngine;
using UnityEngine.UI;
using Zombies;
using System.Collections.Generic;

public class Player : Actor
{
    private Damage _damage;

    void Start()
    {
        _damage = GetComponent<Damage>();

        // set the player health
        this._gc.SetHealthText(_damage.health.ToString());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC") {
            Actor otherActor = other.gameObject.GetComponent<Actor>();
            // create a 'stranger' relationship with this other character
            _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.STRANGER));

            DisplayRelationships(true);
        }
    }

    void Update()
    {
        // check if health is below 0
        if (_damage.health <= 0) {
            // display title text
            _gc.SetTitleText("YOU ARE A ZOMBIE");
        }

        // update UI with new health value
        _gc.SetHealthText(_damage.health.ToString());
    }
}
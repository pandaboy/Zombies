using UnityEngine;
using UnityEngine.UI;
using Zombies;
using System.Collections.Generic;

/// <summary>
/// Manages the player, including relationships and UI elements
/// </summary>
public class Player : Actor
{
    protected Damage _damage;

    void Start()
    {
        _damage = GetComponent<Damage>();

        // set the player health
        _gc.SetHealthText(_damage.health.ToString());

        DisplayRelationships();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC") {
            Actor otherActor = other.gameObject.GetComponent<Actor>();
            // create a 'stranger' relationship with this other character
            _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.STRANGER));

            // update the relationships for the player
            DisplayRelationships(true);
        }
    }

    void Update()
    {
        // check if health is below 0
        if (_damage.health <= 0) {
            // display title text
            _gc.DisplayEndScreen("YOU ARE A ZOMBIE");
        }

        // update UI with new health value
        _gc.SetHealthText(_damage.health.ToString());
    }

    public IList<Actor> GetFollowers()
    {
        return _graph.WithRelationshipTo(this, new Relationship(RelationshipType.FOLLOWER));
    }

    public override void DisplayRelationships(bool player = false)
    {
        string info = "Name: " + Name + "\n";

        // what type are we?
        info += "TYPE: ";
        info += StringifyActorType(actorType) + "\n";

        info += "RELATIONSHIPS:\n";
        if (_graph.GetDirectConnections(this).Count == 0) {
            info += "No relationships yet!";
        }
        foreach (Connection conn in _graph.GetDirectConnections(this)) {
            info += "- " + StringifyRelationshipType(conn.Relationship.RelationshipType);
            info += " " + conn.To.Name + "\n";
        }

        _gc.SetRelationshipText(info);
        // base.DisplayRelationships(false);
    }
}
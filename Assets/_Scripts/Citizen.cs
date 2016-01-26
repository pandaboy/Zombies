using UnityEngine;
using Zombies;
using System.Collections.Generic;

/// <summary>
/// Manages Citizen Actors.
/// </summary>
public class Citizen : Actor
{
    protected Follow _follow;

    void Start()
    {
        _follow = GetComponent<Follow>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            CheckRelationship(other.gameObject);
        }
    }

    private void CheckRelationship(GameObject other)
    {
        Actor otherActor = other.GetComponent<Actor>();

        // Get the direct connection from this actor to the other
        Connection conn = _graph.GetConnection(this, otherActor);

        if (conn == null) {
            // initially distrusts the player
            _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.DISTRUST));

            // put some dialog
            _gc.SetDialogText("I don't know you!");
        }
        else {
            if (conn.Relationship.RelationshipType == RelationshipType.TRUST) {
                _follow.Target = other;
                _follow.CanFollow = true;
                _gc.SetDialogText("Hey! I know you! I'm following you!");
            }
            else {
                _gc.SetDialogText("Hey! I met you before! I " + conn.Relationship.RelationshipType + " you!\nI'm not following you!");
            }
        }

        // Next let's check our Actor's relationship to the other actors connections
        ICollection<Actor> actors = _graph.GetDirectActors(otherActor);

        foreach (Actor actor in actors) {
            actor.PrintRelationships();

            // get the direct relationships to this other actor
            if (_graph.HaveRelationship(actor, _gc.Group.GetComponent<Actor>(), RelationshipType.MEMBER)) {
                // Actor agrees to become a follower
                _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.FOLLOWER));

                _follow.Target = other;
                _follow.CanFollow = true;
                
                _gc.SetDialogText("I don't know you! But I trust " + actor + ", so I'll following you!");
            }
        }
    }
}

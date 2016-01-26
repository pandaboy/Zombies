using UnityEngine;
using Zombies;
using System.Collections.Generic;

/// <summary>
/// Manages Citizen Actors.
/// </summary>
public class Citizen : Actor
{
    public ActorType trustsType;

    protected Follow _follow;

    protected bool _isRescued;
    public bool IsRescued
    {
        get
        {
            return _isRescued;
            _gc.Rescued++;
        }

        set
        {
            // if this was previously rescued, but isn't anymore
            // reduce the rescued amount
            if (_isRescued && value == false) {
                _gc.Rescued--;
            }
            _isRescued = value;
        }
    }

    void Start()
    {
        _follow = GetComponent<Follow>();
        _isRescued = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            CheckRelationship(other.gameObject);
        }
    }

    private void CheckRelationship(GameObject other)
    {
        // Use the Actor class with the ZombieGraph
        Actor otherActor = other.GetComponent<Actor>();

        // Get the connection from this actor to the other actor
        Connection conn = _graph.GetConnection(this, otherActor);

        // if no connection exists, create a new "distrustful" relationship
        if (conn == null) {
            // check if we can trust the player
            if (trustsType == otherActor.actorType) {
                _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.FOLLOWER));

                // put some dialog
                _gc.SetDialogText("You look like someone I can trust!");
            }
            else {
                // initially distrusts the player, as the actorTypes don't match up
                _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.DISTRUST));

                // put some dialog
                _gc.SetDialogText("I don't know you!");
            }
        }
        else { // otherwise check the type of relationship to the other character
            // if we trust them already, we'll just follow them
            if (conn.Relationship.RelationshipType == RelationshipType.TRUST) {
                _follow.Target = other;
                _follow.CanFollow = true;
                _gc.SetDialogText("Hey! I know you! I'm following you!");
            }
            else { // if we don't trust them, remind them of the relationship
                _gc.SetDialogText("I met you before! I " + conn.Relationship.RelationshipType + " you!\nI'm not following you anywhere!");
            }
        }

        // check the otherActor's followers to determine if we should follow them
        ICollection<Actor> followers = other.gameObject.GetComponent<Player>().GetFollowers();
        foreach (Actor actor in followers)
        {
            // if we trust the other type, follow them
            if (actor.actorType == trustsType)
            {
                // we follow the one we trust lol
                _graph.AddDirectConnection(new Connection(this, actor, RelationshipType.FOLLOWER));

                _follow.Target = actor.gameObject;
                _follow.CanFollow = true;

                _gc.SetDialogText("I know " + actor + "! I'll follow THEM!");
                IsRescued = true;
            }
        }

        // Next let's check our Actor's relationship to the other actors connections
        ICollection<Actor> actors = _graph.GetDirectActors(otherActor);

        foreach (Actor actor in actors) {
            // get the direct relationships to this other actor
            if (_graph.HaveRelationship(actor, _gc.Group.GetComponent<Actor>(), RelationshipType.MEMBER)) {
                // Actor agrees to become a follower
                _graph.AddDirectConnection(new Connection(this, otherActor, RelationshipType.FOLLOWER));

                _follow.Target = other;
                _follow.CanFollow = true;
                
                _gc.SetDialogText("I don't know you!\nBut I trust " + actor + ", so I'll following you!");
                IsRescued = true;
            }
        }
    }

    public override void DisplayRelationships(bool player = false)
    {
        string info = "Name: " + Name + "\n";

        // what type are we?
        info += "IS:\n";
        info += " - " + StringifyActorType(actorType) + "\n";

        // who do we trust?
        info += "TRUSTS:\n";
        info += " - " + StringifyActorType(trustsType) + "\n";

        // what relationships do we have?
        info += "RELATIONSHIPS:\n";
        if (_graph.GetDirectConnections(this).Count == 0) {
            info += "No relationships yet!";
        }

        foreach (Connection conn in _graph.GetDirectConnections(this)) {
            info += "- " + StringifyRelationshipType(conn.Relationship.RelationshipType);
            info += " " + conn.To.Name + "\n";
        }

        _gc.SetInfoText(info);
        // base.DisplayRelationships(false);
    }
}

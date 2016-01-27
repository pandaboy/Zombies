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
    protected GameObject _turnTarget;
    protected Actor _groupActor; // the groupActor for this scene

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
        _groupActor = _gc.Group.GetComponent<Actor>();
        _isRescued = false;
    }

    void Update()
    {
        if (_turnTarget) {
            TurnTo();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            CheckRelationships(other.gameObject);

            if(!_isRescued) {
                _turnTarget = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_turnTarget == other.gameObject)
        {
            _turnTarget = null;
        }
    }

    // returns true if we're following/rescued
    private bool CheckRelationships(GameObject other)
    {
        Actor otherActor = other.GetComponent<Actor>();     // the actor for the other GameObject

        // check direct relationship to the other actor
        if (!CheckDirectRelationship(otherActor))
        {
            // check the other actors direct relationships
            if (!CheckItems(otherActor))
            {
                // check relationship to their followers
                return CheckFollowers(otherActor);
            }
        }

        return true;
    }

    private bool CheckDirectRelationship(Actor other)
    {
        // Get the connection from this actor to the other actor
        Connection conn = _graph.GetConnection(this, other);

        // no connection exists, make a decision based on actor types
        if (conn == null)
        {
            // if the actor type matches our trust type, follow them
            if (trustsType == other.actorType)
            {
                Rescue(other, "You look like someone I can trust!");
                return true;
            }
            else
            {
                // initially distrusts the player, as the actorTypes don't match up
                _graph.AddDirectConnection(new Connection(this, other, RelationshipType.DISTRUST));
                _gc.SetDialogText("I don't know you! I'm not going to follow you!");
                return false;
            }
        }
        else
        {
            // check the pre-existing relationship type
            switch (conn.Relationship.RelationshipType)
            {
                // if we already follow them, just return
                case RelationshipType.FOLLOWER:
                    return true;

                // if we trust them already, we'll just follow them
                case RelationshipType.TRUST:
                    Rescue(other, "Hey! I know you! I'm following you!");
                    return true;

                // if we don't trust them, remind them of the relationship
                default:
                    _gc.SetDialogText("I met you before! I " + conn.Relationship.RelationshipType + " you!\nI'm not following you anywhere!");
                    return false;
            }
        }

        return false;
    }

    private bool CheckItems(Actor other)
    {
        // Next let's check our Actor's relationship to the other actors direct connections
        //ICollection<Actor> actors = _graph.GetDirectActors(other);
        ICollection<Actor> actors = other.gameObject.GetComponent<Player>().GetItems();
        foreach (Actor actor in actors)
        {
            Debug.Log(actor.actorType);
            if (actor.actorType == trustsType)
            {
                Rescue(other, "I don't know you!\nBut I trust " + actor.Name + ", so I'll come along!");

                return true;
            }

            // get the direct relationships to this other actor's groups
            if (_graph.HaveRelationship(actor, _groupActor, RelationshipType.MEMBER))
            {
                Rescue(other, "I don't know you!\nBut I trust " + actor.Name + ", so I'll come along!");

                return true;
            }
        }

        return false;
    }

    private bool CheckFollowers(Actor other)
    {
        // now check the otherActor's followers to determine if we should follow them
        ICollection<Actor> followers = other.gameObject.GetComponent<Player>().GetFollowers();
        foreach (Actor actor in followers)
        {
            // if we trust the other type, follow them
            if (actor.actorType == trustsType)
            {
                Rescue(actor, "I trust " + actor.Name + "! I'll follow THEM!");

                return true;
            }
            // if we trust the other types group, follow them
            else if (_graph.HaveRelationship(actor, _groupActor, RelationshipType.MEMBER) && _groupActor.actorType == trustsType)
            {
                Rescue(other, actor.Name + " is an " + StringifyActorTypeSingle(trustsType) + ", so I'll following you!");

                return true;
            }
        }

        return false;
    }

    protected void Rescue(Actor actor, string dialog = "")
    {
        // Actor agrees to become a follower
        _graph.AddDirectConnection(new Connection(this, actor, RelationshipType.FOLLOWER));

        _follow.Target = actor.gameObject;
        _follow.CanFollow = true;

        _gc.SetDialogText(dialog);
        IsRescued = true;
    }

    public override void DisplayRelationships(bool player = false)
    {
        string info = "NAME: " + Name + "\n";

        // what type are we?
        info += "TYPE: ";
        info += StringifyActorTypeSingle(actorType) + "\n";

        // who do we trust?
        info += "TRUSTS:\n";
        info += " - " + StringifyActorType(trustsType) + "\n";

        // what relationships do we have?
        info += "RELATIONSHIPS:\n";
        if (_graph.GetDirectConnections(this).Count == 0) {
            info += "NONE YET!";
        }

        foreach (Connection conn in _graph.GetDirectConnections(this)) {
            info += "- " + StringifyRelationshipType(conn.Relationship.RelationshipType);
            info += " " + conn.To.Name + "\n";
        }

        _gc.SetInfoText(info);
    }

    // rotate to face target
    void TurnTo()
    {
        gameObject.transform.LookAt(_turnTarget.transform.position);
    }
}

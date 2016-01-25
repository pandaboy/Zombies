using UnityEngine;
using System.Collections.Generic;
using Zombies;

public class Citizen : MonoBehaviour
{
    private ZombieGraph _graph;
    private Follow _follow;
    private Actor _thisActor;

    // relationships in use
    private Relationship stranger = new Relationship(Zombies.RelationshipType.STRANGER);
    private Relationship trust = new Relationship(Zombies.RelationshipType.TRUST);
    private Relationship distrust = new Relationship(Zombies.RelationshipType.DISTRUST);
    private Relationship member = new Relationship(Zombies.RelationshipType.MEMBER);
    private GameController _gc;

    void Start()
    {
        this._follow = GetComponent<Follow>();
        this._thisActor = GetComponent<Actor>();
        this._graph = ZombieGraph.Instance;
        this._gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckRelationship(other.gameObject);
        }
    }

    private void CheckRelationship(GameObject other)
    {
        Actor otherActor = other.GetComponent<Actor>();

        // Get the direct connection from this actor to the other
        Connection conn = _graph.GetConnection(_thisActor, otherActor);

        if (conn == null) {
            // initially distrusts the player
            _graph.AddDirectConnection(new Connection(_thisActor, otherActor, distrust));

            // put some dialog
            _gc.SetDialogText("I don't know you!");
        }
        else
        {
            if (conn.Relationship.RelationshipType == RelationshipType.TRUST)
            {
                _follow.Target = other;
                _follow.CanFollow = true;
                _gc.SetDialogText("Hey! I know you! I'm following you!");
            }
            else
            {
                _gc.SetDialogText("Hey! I met you before! I " + conn.Relationship.RelationshipType + " you!\nI'm not following you!");
            }
        }

        // Next let's check our Actor's relationship to the other actors connections
        ICollection<Actor> actors = _graph.GetDirectActors(otherActor);

        foreach (Actor actor in actors)
        {
            Debug.Log(actor);

            actor.PrintRelationships();

            // get the direct relationships to this other actor
            if (_graph.HaveRelationship(actor, _gc.AuthorityGroup.GetComponent<Actor>(), member))
            {
                _follow.Target = other;
                _follow.CanFollow = true;
                _gc.SetDialogText("I don't know you! But I trust " + actor + ", so I'll following you!");
            }
        }
    }
}

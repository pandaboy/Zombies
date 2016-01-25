using UnityEngine;
using System;
using RelationshipGraph.Interfaces;
using Zombies;

namespace Zombies
{
    /// <summary>
    /// Basic Actor implementation
    /// </summary>
    public class Actor : MonoBehaviour, INode<Actor>
    {
        protected ZombieGraph _graph;
        protected GameController _gc;

        protected static int _count = 0;
        public int Count
        {
            get
            {
                return _count;
            }
        }

        protected int _ActorId;
        public int ActorId
        {
            get
            {
                return _ActorId;
            }

            set
            {
                _ActorId = value;
            }
        }

        public virtual void Awake()
        {
            _graph = ZombieGraph.Instance;
            _gc = GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();
            ActorId = ++_count;
        }

        /// <summary>
        /// Type of Actor. Uses Zombies.ActorType enum
        /// </summary>
        public ActorType actorType;

        /// <summary>
        /// HandleMessage receives IMessage's sent to the Node
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual bool HandleMessage(IMessage message)
        {
            // do something with the message...
            // successfully did nothing :)
            return false;
        }

        /// <summary>
        /// Equals is required to identify a unique Actor in the graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool Equals(Actor other)
        {
            if (other == null)
                return false;

            if (this.ActorId == other.ActorId)
                return true;

            return false;
        }

        public override bool Equals(object o)
        {
            if (o == null)
                return false;

            Actor actor = o as Actor;

            if (actor == null)
                return false;

            return Equals(actor);
        }

        public override int GetHashCode()
        {
            return _ActorId;
        }

        public override string ToString()
        {
            return actorType + " " + ActorId;
        }

        public void PrintRelationships()
        {
            string relationships = "R:" + this.ActorId + " - ";
            foreach (Connection conn in _graph.GetDirectConnections(this)) {
                relationships += conn.Relationship.RelationshipType + "'s " + conn.To + "";
            }

            // print to the console
            Debug.Log(relationships);
        }

        // updates the UI with this Actors Direct Relationships
        public void DisplayRelationships(bool player = false)
        {
            string relationshipsMsg = "";
            foreach (Connection conn in _graph.GetDirectConnections(this)) {
                relationshipsMsg += conn.Relationship.RelationshipType + "'s " + conn.To + "\n";
            }

            if (player)
                _gc.SetRelationshipText(relationshipsMsg);
            else
                _gc.SetInfoText(relationshipsMsg);
        }
    }
}

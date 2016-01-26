using UnityEngine;
using System;
using RelationshipGraph.Interfaces;
using Zombies;

namespace Zombies
{
    /// <summary>
    /// Base class for the player and citizens, plugs into the ZombieGraph
    /// </summary>
    public class Actor : MonoBehaviour, INode<Actor>
    {
        protected ZombieGraph _graph;
        protected GameController _gc;

        /// <summary>
        /// Tracks the number of Actors in the level,
        /// used to assign ActorIds
        /// </summary>
        protected static int _count = 0;
        /// <summary>
        /// Accessor for the Count
        /// </summary>
        public int Count
        {
            get
            {
                return _count;
            }
        }

        /// <summary>
        /// Unique ActorId's required for use with ZombieGraph
        /// </summary>
        protected int _ActorId;
        /// <summary>
        /// Accessor for the ActorId
        /// </summary>
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

        protected string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public bool isGroup = false;

        public virtual void Awake()
        {
            _name   = RandomName();
            _graph  = ZombieGraph.Instance;
            _gc     = GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameController>();

            // simple integer identifiers
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
            if (other == null) {
                return false;
            }

            if (this.ActorId == other.ActorId) {
                return true;
            }

            return false;
        }

        public override bool Equals(object o)
        {
            if (o == null) {
                return false;
            }

            Actor actor = o as Actor;

            if (actor == null) {
                return false;
            }

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
                relationships += conn.Relationship.RelationshipType + "'s " + conn.To + " ";
            }

            // print to the console
            Debug.Log(relationships);
        }

        // updates the UI with this Actors Direct Relationships
        public virtual void DisplayRelationships(bool player = false)
        {
            string relationshipsMsg = "";
            foreach (Connection conn in _graph.GetDirectConnections(this)) {
                relationshipsMsg += conn.Relationship.RelationshipType + "'s " + conn.To + "\n";
            }

            if (player) {
                _gc.SetRelationshipText(relationshipsMsg);
            }
            else {
                _gc.SetInfoText(relationshipsMsg);
            }
        }

        protected string StringifyActorType(ActorType aType)
        {
            switch (aType)
            {
                case ActorType.AUTHORITY:
                    return "AUTHORITY FIGURES";

                case ActorType.ANIMAL:
                    return "ANIMAL LOVERS";

                case ActorType.NEUTRAL:
                    return "NEUTRALS";

                case ActorType.FRIENDLY:
                    return "FRIENDLY PEOPLE";

                case ActorType.DANGEROUS:
                    return "DANGEROUS FOLK";

                default:
                    return aType.ToString();
            }
        }

        protected string StringifyRelationshipType(RelationshipType rType)
        {
            switch (rType)
            {
                case RelationshipType.TRUST:
                    return "TRUSTS";

                case RelationshipType.DISTRUST:
                    return "DISTRUSTS";

                case RelationshipType.SCARED:
                    return "SCARED OF";

                case RelationshipType.STRANGER:
                    return "NO OPINION OF";

                case RelationshipType.FOLLOWER:
                    return "FOLLOWS";

                case RelationshipType.MEMBER:
                    return "MEMBER OF";

                default:
                    return rType.ToString();
            }
        }

        string RandomName()
        {
            // Groups don't get names
            if (isGroup) {
                return actorType.ToString() + " types";
            }

            // returns a random name for the actor from the list
            string[] names = {
                "Brendan", "Kat", "Bob", "Jane", "Alice", "Charlie", "Marge"
            };

            return names[UnityEngine.Random.Range(0, names.Length)];
        }
    }
}

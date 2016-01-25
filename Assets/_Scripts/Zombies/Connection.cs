using UnityEngine;
using System;
using System.Collections.Generic;
using RelationshipGraph.Edges;
using Zombies;

namespace Zombies
{
    public class Connection : HistoryEdge<Actor, Relationship>
    {
        public Connection() : base() { }

        public Connection(Actor from, Actor to, Relationship relationship)
            : base(from, to, relationship) { }

        // accepts GameObjects for faster initialization
        public Connection(GameObject from, GameObject to, Relationship relationship)
            : base(from.GetComponent<Actor>(), to.GetComponent<Actor>(), relationship)
        {
            // ...
        }

        public Connection(Actor from, Actor to, RelationshipType relationshipType)
            : base(from, to, new Relationship(relationshipType))
        {
            // ...
        }

        public Connection(GameObject from, GameObject to, RelationshipType relationshipType)
            : base(from.GetComponent<Actor>(), to.GetComponent<Actor>(), new Relationship(relationshipType))
        {
            // ...
        }

        public override string ToString()
        {
            string history = From + " [";
            foreach (Relationship relationship in History)
            {
                history += relationship + " ";
            }
            history += "] " + To;

            return history;
        }
    }
}

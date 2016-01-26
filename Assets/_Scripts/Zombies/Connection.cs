using UnityEngine;
using RelationshipGraph.Edges;
using Zombies;

namespace Zombies
{
    /// <summary>
    /// Stores Relationships between Actors
    /// </summary>
    public class Connection : HistoryEdge<Actor, Relationship>
    {
        /// <summary>
        /// Default basic Constructor. Does nothing
        /// </summary>
        /// <returns></returns>
        public Connection() : base() { }

        /// <summary>
        /// Constructor; creates Connection between two Actors
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public Connection(Actor from, Actor to, Relationship relationship)
            : base(from, to, relationship) { }

        /// <summary>
        /// Alternative constructor that accepts GameObjects instead of Actors
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public Connection(GameObject from, GameObject to, Relationship relationship)
            : base(from.GetComponent<Actor>(), to.GetComponent<Actor>(), relationship) { }

        /// <summary>
        /// Alternative constructor that accepts a RelationshipType instead of Relationship.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationshipType"></param>
        /// <returns></returns>
        public Connection(Actor from, Actor to, RelationshipType relationshipType)
            : base(from, to, new Relationship(relationshipType)) { }

        /// <summary>
        /// Alternative constructor that accepts GameObjects and a RelationshipType.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="relationshipType"></param>
        /// <returns></returns>
        public Connection(GameObject from, GameObject to, RelationshipType relationshipType)
            : base(from.GetComponent<Actor>(), to.GetComponent<Actor>(), new Relationship(relationshipType)) { }

        /// <summary>
        /// Returns a string representation of this relationship.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The string returned is in the format
        /// "Actor.ToString() - Relationship.ToString() - Actor.ToString()"
        /// </remarks>
        public override string ToString()
        {
            string history = From + " - ";

            foreach (Relationship relationship in History) {
                history += relationship + " ";
            }
            history += " - " + To;

            return history;
        }
    }
}

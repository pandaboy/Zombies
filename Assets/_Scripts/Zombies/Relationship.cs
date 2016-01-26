using System;
using RelationshipGraph.Interfaces;
using Zombies;

namespace Zombies
{
    /// <summary>
    /// Models a relationship by wrapping the RelationshipType enum
    /// </summary>
    public class Relationship : IRelationship<Relationship>
    {
        /// <summary>
        /// Constructor accepts a RelationshipType
        /// </summary>
        public Relationship(RelationshipType type = RelationshipType.DISTRUST)
        {
            this.RelationshipType = type;
        }

        /// <summary>
        /// Type of Relationship. Uses Zombies.RelationshipType enum
        /// </summary>
        protected RelationshipType _RelationshipType;
        /// <summary>
        /// Accessor for the RelationshipType
        /// </summary>
        public RelationshipType RelationshipType
        {
            get
            {
                return _RelationshipType;
            }

            set
            {
                _RelationshipType = value;
            }
        }

        /// <summary>
        /// Method required for functionality with the Graph
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <remarks>
        /// This method compares only the RelationshipType's. That's all that's really needed
        /// for this demo.
        /// </remarks>
        public bool Equals(Relationship other)
        {
            if (other == null) {
                return false;
            }

            if (this.RelationshipType == other.RelationshipType) {
                return true;
            }

            return false;
        }
    }
}

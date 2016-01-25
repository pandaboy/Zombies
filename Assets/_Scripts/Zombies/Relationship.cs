using System;
using RelationshipGraph.Interfaces;
using Zombies;

namespace Zombies
{
    public class Relationship : IRelationship<Relationship>
    {
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
        /// This method compares only the RelationshipType's and ignores the weight value.
        /// This should be replaced with a special member so that normal IEquatable overloading
        /// can still be used
        /// </remarks>
        public bool Equals(Relationship other)
        {
            if (other == null)
                return false;

            if (this.RelationshipType == other.RelationshipType)
                return true;

            return false;
        }
    }
}

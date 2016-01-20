using System;
using RelationshipGraph.Interfaces;
using Zombies;

namespace Zombies
{
    /// <summary>
    /// Basic Actor implementation
    /// </summary>
    public class Actor : INode<Actor>
    {
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

            if (!this.Equals(other))
                return false;

            return true;
        }
    }
}

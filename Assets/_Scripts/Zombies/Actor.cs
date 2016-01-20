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

            if (!this.Equals(other))
                return false;

            return true;
        }

        void Start()
        {
            Debug.Log("Hello! I'm " + this.name + "! And I'm a " + this.actorType + " actor type!");
        }
    }
}

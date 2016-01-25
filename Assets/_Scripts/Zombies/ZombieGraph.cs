using UnityEngine;
using System.Collections.Generic;
using Zombies;
using RelationshipGraph.Graphs;
using RelationshipGraph.Interfaces;

namespace Zombies
{
    public sealed class ZombieGraph
    {
        private DeepGraph<Actor, Connection, Relationship> _graph;

        private static readonly ZombieGraph _instance = new ZombieGraph();

        private ZombieGraph()
        {
            _graph = new DeepGraph<Actor, Connection, Relationship>();
        }

        public static ZombieGraph Instance
        {
            get
            {
                return _instance;
            }
        }

        #region Connections Checkers
        public bool IsGraphed(Actor actor)
        {
            return _graph.IsGraphed(actor);
        }

        public bool HasConnection(Actor actor, Actor other)
        {
            return _graph.HasEdge(actor, other);
        }

        public bool ActorHasConnection(Actor actor, Connection connection)
        {
            return _graph.NodeHasEdge(actor, connection);
        }

        public bool ActorHasConnection(Actor actor, Actor from, Actor to)
        {
            return _graph.NodeHasEdge(actor, from, to);
        }

        /// <summary>
        /// Checks to see if the Actor knows of the other actor (is present in Indirect Connections)
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool KnowsConnectionsOf(Actor actor, Actor other)
        {
            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetIndirectConnections(actor))
                {
                    if (connection.To.Equals(other) || connection.From.Equals(other))
                        return true;
                }
            }

            return false;
        }
        #endregion

        #region Getting Connections
        public ICollection<Connection> GetConnections(Actor actor)
        {
            return _graph.GetEdges(actor);
        }

        public ICollection<Connection> GetDirectConnections(Actor actor)
        {
            return _graph.GetDirectEdges(actor);
        }

        public ICollection<Connection> GetIndirectConnections(Actor actor)
        {
            return _graph.GetInDirectEdges(actor);
        }

        public Connection GetConnection(Actor actor, Actor other)
        {
            return _graph.GetEdge(actor, other);
        }

        public Connection GetActorConnection(Actor actor, Connection connection)
        {
            return _graph.GetNodeEdge(actor, connection);
        }

        public Connection GetActorConnection(Actor actor, Actor from, Actor to)
        {
            return _graph.GetNodeEdge(actor, from, to);
        }

        public ICollection<Connection> GetConnectionsOf(Actor actor, Actor other)
        {
            IList<Connection> otherConnections = new List<Connection>();

            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetConnections(actor))
                {
                    if (connection.From.Equals(other) || connection.To.Equals(other))
                    {
                        otherConnections.Add(connection);
                    }
                }
            }

            return otherConnections;
        }

        public ICollection<Connection> GetKnownConnectionsOf(Actor actor, Actor other)
        {
            IList<Connection> known = new List<Connection>();

            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetIndirectConnections(actor))
                {
                    if (connection.From.Equals(other) || connection.To.Equals(other))
                    {
                        known.Add(connection);
                    }
                }
            }

            return known;
        }

        /// <summary>
        /// returns a collection of Actors from Direct Connections
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public ICollection<Actor> GetDirectActors(Actor actor)
        {
            ICollection<Actor> direct = new List<Actor>();

            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetDirectConnections(actor))
                {
                    direct.Add(connection.To);
                }
            }

            return direct;
        }

        public ICollection<Actor> GetIndirectActors(Actor actor)
        {
            ICollection<Actor> indirect = new List<Actor>();

            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetIndirectConnections(actor))
                {
                    if (!indirect.Contains(connection.From))
                    {
                        indirect.Add(connection.From);
                    }

                    if (!indirect.Contains(connection.To))
                    {
                        indirect.Add(connection.To);
                    }
                }
            }

            return indirect;
        }

        public ICollection<Connection> GetCommonDirectConnections(Actor actor, Actor other)
        {
            ICollection<Connection> common = new List<Connection>();

            if (_graph.IsGraphed(actor) && _graph.IsGraphed(other))
            {
                foreach (Connection i in GetDirectConnections(actor))
                {
                    foreach (Connection j in GetDirectConnections(other))
                    {
                        // if they both point to the same entity, 
                        // they share connections to that that entity
                        if (i.To.Equals(j.To))
                        {
                            // add both connections
                            common.Add(i);
                            common.Add(j);
                        }
                    }
                }
            }

            return common;
        }
        #endregion

        #region Adding Connections
        public void AddConnection(Actor actor, Connection connection)
        {
            _graph.AddEdge(actor, connection);
        }

        public void AddDirectConnection(Connection connection)
        {
            _graph.AddDirectEdge(connection);
        }

        /// <summary>
        /// Adds a Direct Connection both ways
        /// </summary>
        /// <param name="connection"></param>
        public void AddCommonConnection(Connection connection)
        {
            _graph.AddCommonEdge(connection);
        }
        #endregion

        #region Removing Connections
        public void RemoveConnections(Actor actor)
        {
            _graph.Remove(actor);
        }

        public void RemoveConnection(Actor actor, Connection connection)
        {
            _graph.RemoveEdge(actor, connection);
        }

        public void RemoveDirectConnection(Connection connection)
        {
            _graph.RemoveDirectEdge(connection);
        }

        public void RemoveCommonConnection(Connection connection)
        {
            _graph.RemoveCommonEdge(connection);
        }

        public void ClearConnections(Actor actor)
        {
            _graph.ClearEdges(actor);
        }

        public void ForgetConnections(Actor actor)
        {
            _graph.ClearEdges(actor);
        }

        public void ForgetDirectConnections(Actor actor)
        {
            _graph.ClearDirectEdges(actor);
        }

        public void ForgetIndirectConnections(Actor actor)
        {
            _graph.ClearIndirectEdges(actor);
        }

        public bool Remove(Actor actor)
        {
            return Forget(actor, true);
        }

        /// <summary>
        /// This method will remove all presence of the actor from the graph.
        /// </summary>
        /// <param name="actor">Actor to forget</param>
        /// <param name="complete">Also remove the Actor from the graph</param>
        /// <returns>true if successfully forgotten, false otherwise</returns>
        /// <remarks>
        /// This method will remove the graph record and ALL connections that have
        /// a reference to the entity
        /// </remarks>
        public bool Forget(Actor actor, bool complete = false)
        {
            // remove all connections other entities have involving this entity
            foreach (KeyValuePair<Actor, IList<Connection>> item in this._graph)
            {
                // skip the entity passed in
                if (item.Key.Equals(actor))
                {
                    continue;
                }

                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i].From.Equals(actor) || item.Value[i].To.Equals(actor))
                    {
                        item.Value.RemoveAt(i);
                    }
                }
            }

            // remove the connections stored for this entity
            if (complete)
            {
                _graph.Remove(actor);
            }

            return true;
        }

        /// <summary>
        /// Will remove all knowledge of the other Actor from actor's connections
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ForgetAbout(Actor actor, Actor other)
        {
            ICollection<Connection> otherConnections = GetConnectionsOf(actor, other);

            foreach (Connection connection in otherConnections)
            {
                _graph.RemoveEdge(actor, connection);
            }

            return true;
        }

        public void Clear()
        {
            _graph.Clear();
        }
        #endregion

        #region Relationships
        public IList<Actor> WithRelationship(Actor actor, Relationship relationship)
        {
            return _graph.WithRelationship(actor, relationship);
        }

        public IList<Actor> WithRelationshipTo(Actor actor, Relationship relationship)
        {
            return _graph.WithRelationshipTo(actor, relationship);
        }

        public IList<Actor> WithConnection(Actor actor, Connection connection)
        {
            return WithRelationship(actor, connection.Relationship);
        }

        public IList<Actor> WithConnectionTo(Actor actor, Connection connection)
        {
            return WithRelationshipTo(actor, connection.Relationship);
        }

        public IList<Actor> WithRelationshipHistory(Actor actor, Relationship relationship)
        {
            IList<Actor> matches = new List<Actor>();

            if (_graph.IsGraphed(actor))
            {
                foreach (Connection connection in GetDirectConnections(actor))
                {
                    foreach (Relationship rel in connection.History)
                    {
                        if (rel.Equals(relationship))
                        {
                            matches.Add(connection.To);
                        }
                    }
                }
            }

            return matches;
        }

        public IList<Actor> WithRelationshipHistoryTo(Actor actor, Relationship relationship)
        {
            IList<Actor> matches = new List<Actor>();

            // we have to check all the connections for each entity
            foreach (IList<Connection> connections in _graph.Values)
            {
                // go through each connection
                foreach (Connection connection in connections)
                {

                    // if the source of the connection is the same as the entity it will not work and can be skipped
                    // i.e. an entity can't have relationship history with itself.
                    if (connection.From.Equals(actor))
                    {
                        continue;
                    }

                    // in each connection look through the relationship history
                    foreach (Relationship rel in connection.History)
                    {
                        // if the connection is TO the entity,
                        // and the relationship matches
                        // and it hasn't already been catalogued - add it.
                        if (rel.Equals(relationship) && connection.To.Equals(actor) && !matches.Contains(connection.From))
                        {
                            matches.Add(connection.From);
                        }
                    }
                }
            }

            return matches;
        }

        public IList<Actor> WithConnectionHistory(Actor actor, Connection connection)
        {
            return WithRelationshipHistory(actor, connection.Relationship);
        }

        public IList<Relationship> GetRelationshipHistory(Actor actor, Actor other)
        {
            if (ActorHasConnection(actor, actor, other))
            {
                return GetConnection(actor, other).Relationships;
            }
            else
            {
                return new List<Relationship>();
            }
        }

        public bool HaveRelationship(Actor actor, Actor other, Relationship relationship)
        {
            if (ActorHasConnection(actor, actor, other))
            {
                Connection connection = GetActorConnection(actor, actor, other);

                if (connection.Relationship.Equals(relationship))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HaveRelationship(Actor actor, Actor other, RelationshipType relationshipType)
        {
            return HaveRelationship(actor, other, new Relationship(relationshipType));
        }

        public bool HaveRelationshipHistory(Actor actor, Actor other, Relationship relationship)
        {
            if (ActorHasConnection(actor, actor, other))
            {
                foreach (Relationship rel in GetConnection(actor, other).Relationships)
                {
                    if (rel.Equals(relationship))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HaveConnectionHistory(Actor actor, Actor other, Connection connection)
        {
            return HaveRelationshipHistory(actor, other, connection.Relationship);
        }

        public bool HaveSharedRelationshipHistory(Actor actor, Actor other, Relationship relationship)
        {
            return HaveRelationshipHistory(actor, other, relationship)
                && HaveRelationshipHistory(other, actor, relationship);
        }

        public bool HaveSharedConnectionHistory(Actor actor, Actor other, Connection connection)
        {
            return HaveSharedRelationshipHistory(actor, other, connection.Relationship);
        }
        #endregion

        #region Messages
        public void SendMessage(Actor actor, Actor other, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(actor, other, message, delay);
        }

        // sends message to entities with [relationship] from entity
        // i.e to all entities that [actor] points to with [relationship] "Bob's -> brothers -> Entities"
        public void SendMessage(Actor actor, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessage(actor, relationship, message, delay);
        }

        // sends message to entities with [relationship] to entity
        // i.e. to all entities that point to [actor] with [relationship] "Entities -> [Member] -> Group"
        public void SendMessageTo(Actor actor, Relationship relationship, IMessage message, double delay = 0.0)
        {
            _graph.SendMessageTo(actor, relationship, message, delay);
        }

        public void ForgetMessages(Actor actor)
        {
            _graph.ForgetMessages(actor);
        }
        #endregion

        /// <summary>
        /// Displays a list of known connections to the console
        /// </summary>
        public void PrintConnections()
        {
            if (_graph.Count > 0)
            {
                Debug.Log(">> CONNECTIONS:");

                foreach (KeyValuePair<Actor, IList<Connection>> item in _graph)
                {
                    Debug.Log(item.Key);

                    foreach (Connection connection in item.Value)
                    {
                        Debug.Log(connection);
                    }
                }
            }
            else
            {
                Debug.Log(">> NO CONNECTIONS");
            }
            Debug.Log("--");
        }
    }
}

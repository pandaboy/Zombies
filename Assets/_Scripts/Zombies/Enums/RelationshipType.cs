namespace Zombies
{
    /// <summary>
    /// Types of relationships that can be captured
    /// </summary>
    public enum RelationshipType
    {
        MEMBER,     // Actor belongs to another Actor Type e.g. can belong to the Authority type
        TRUST,      // Actor trusts the other actor
        DISTRUST,   // Actor distrusts the other actor
        FOLLOWER,   // Actor agreed to follow the other actor
        STRANGER,   // Actor considers the other actor a stranger
        SCARED      // Actor is scared of the other actor
    }
}

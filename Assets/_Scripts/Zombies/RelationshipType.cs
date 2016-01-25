namespace Zombies
{
    public enum RelationshipType
    {
        MEMBER,     // Actor belongs to another Actor Type e.g. can belong to the Authority type
        TRUST,      // Actor trusts the other actor
        DISTRUST,   // Actor distrusts the other actor
        STRANGER    // Actor considers the other actor a stranger
    }
}

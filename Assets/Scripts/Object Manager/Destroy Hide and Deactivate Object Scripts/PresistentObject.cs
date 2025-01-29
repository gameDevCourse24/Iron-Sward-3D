using UnityEngine;
// A non-virtual class for objects that remain active for the rest of the game. 
// Once activated, these objects will not be deactivated - like the Resources panel in the game.
// This does not mean that the object is activated from the start of the game. It just means that once activated it is not deleted.

public class PersistentObject : ObjectController
{
    public override void Activate()
    {
        base.Activate();
        pprint.p("The object will remain active for the entire game.");
    }
}

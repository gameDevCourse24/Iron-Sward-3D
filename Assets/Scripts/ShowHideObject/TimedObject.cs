using UnityEngine;
// A class that inherits from TemporaryObject. 
// The object will deactivate after a specified amount of time has passed since activation.

public class TimedObject : TemporaryObject
{
    [SerializeField] private float timeToDeactivate = 5f;

    public override void Activate()
    {
        base.Activate();
        Invoke(nameof(Deactivate), timeToDeactivate);
    }
}

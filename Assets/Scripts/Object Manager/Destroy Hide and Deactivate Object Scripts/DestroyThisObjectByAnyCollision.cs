using UnityEngine;

public class DestroyThisObjectByAnyCollision : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        pprint.p($"{gameObject.name} destroyed because it collided with {other.gameObject.name}", this);
    }
}

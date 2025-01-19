using UnityEngine;

public class TriggerSpawnObject : ObjectSpawner
{
    [SerializeField, Tooltip("The Objects that can activate the spawn by  trigger")]
    private GameObject[] collisionObjects;

    [SerializeField, Tooltip("Destroy the Trigger object after collision?")]
    private bool destroyAfterCollision = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name +" Triggered");
        foreach (GameObject obj in collisionObjects)
        {
            if (gameObject != null){

                if (other.gameObject == obj)
                {
                    Debug.Log("Object Collided with " + obj.name + " Spawning Object");
                    SpawnObject();
                    if (destroyAfterCollision)
                    {
                        Debug.Log("Destroying " + name);
                        Destroy(gameObject);
                        return;
                    }
                }
            }
           
        }
    }
}

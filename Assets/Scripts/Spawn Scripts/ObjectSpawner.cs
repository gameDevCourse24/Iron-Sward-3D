using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("The object to spawn")]
    protected GameObject spawnObject; // האובייקט ליצירה

    [SerializeField, Tooltip("The spawn position")]
    protected Transform spawnPosition; // מיקום היצירה
    [SerializeField, Tooltip("The parent object to organize the spawned objects")]
    private Transform objectFolder; // התיקייה (אובייקט ריק)

    // פונקציה בסיסית ליצירת אובייקט
    protected virtual void SpawnObject()
    {
        if (spawnObject == null){
            Debug.LogError("SpawnObject is not set!");
            return;
        }
        if (spawnPosition == null){
           spawnPosition = transform;
        }
        if (objectFolder == null)
        {
            objectFolder = transform;
        }
        GameObject newObject = Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation, objectFolder);
    }
}

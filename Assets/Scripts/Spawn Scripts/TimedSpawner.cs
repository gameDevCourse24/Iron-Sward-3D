using UnityEngine;

public class TimedSpawner : ObjectSpawner
{
    [SerializeField, Tooltip("Time interval for spawning")]
    protected float spawnInterval = 5f; // מרווח הזמן בין יצירות

    protected float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }
}

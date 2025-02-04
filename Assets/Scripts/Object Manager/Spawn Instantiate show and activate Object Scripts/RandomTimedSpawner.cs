using UnityEngine;

public class RandomTimedSpawner : TimedSpawner
{
    [SerializeField, Tooltip("Minimum spawn interval")]
    private float minInterval = 30f;

    [SerializeField, Tooltip("Maximum spawn interval")]
    private float maxInterval = 90f;

    

    [SerializeField, Tooltip("The maximum number of objects to spawn")]
    private float numberOfObjectsToSpawn = 5;
    private void Start()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        spawnInterval = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        if (numberOfObjectsToSpawn >= 1){
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnObject();
                numberOfObjectsToSpawn--;
                timer = 0f;
                ResetTimer();
            }
        }
    }
}

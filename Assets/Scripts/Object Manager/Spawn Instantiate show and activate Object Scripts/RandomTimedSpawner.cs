using UnityEngine;

public class RandomTimedSpawner : TimedSpawner
{
    [SerializeField, Tooltip("Minimum spawn interval")]
    private float minInterval = 30f;

    [SerializeField, Tooltip("Maximum spawn interval")]
    private float maxInterval = 90f;

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
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
            ResetTimer();
        }
    }
}

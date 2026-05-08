using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnRadius = 5f;

    [Header("Spawn Count")]
    [SerializeField] private int minSpawnCount = 1;
    [SerializeField] private int maxSpawnCount = 5;

    [Header("Spawn Timing")]
    [SerializeField] private float minSpawnInterval = 2f;
    [SerializeField] private float maxSpawnInterval = 8f;

    [Header("Gizmos")]
    [SerializeField] private Color gizmoColor = new Color(1f, 0f, 0f, 0.3f);

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies();
            ScheduleNextSpawn();
        }
    }

    private void SpawnEnemies()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("Spawner: No enemy prefabs assigned!");
            return;
        }

        int count = Random.Range(minSpawnCount, maxSpawnCount + 1);

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomPointOnCircle();
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointOnCircle()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(0f, spawnRadius);

        float x = Mathf.Cos(angle) * distance;
        float y = Mathf.Sin(angle) * distance;

        return transform.position + new Vector3(x, y, 0f);
    }

    private void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        DrawFilledCircle(transform.position, spawnRadius);

        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 1f);
        DrawWireCircle(transform.position, spawnRadius);
    }

    private void DrawWireCircle(Vector3 center, float radius, int segments = 64)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0f, 0f);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }

    private void DrawFilledCircle(Vector3 center, float radius, int segments = 64)
    {
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * angleStep * Mathf.Deg2Rad;
            float angle2 = (i + 1) * angleStep * Mathf.Deg2Rad;

            Vector3 p1 = center + new Vector3(Mathf.Cos(angle1) * radius, Mathf.Sin(angle1) * radius, 0f);
            Vector3 p2 = center + new Vector3(Mathf.Cos(angle2) * radius, Mathf.Sin(angle2) * radius, 0f);

            Gizmos.DrawLine(center, p1);
            Gizmos.DrawLine(p1, p2);
        }
    }
}
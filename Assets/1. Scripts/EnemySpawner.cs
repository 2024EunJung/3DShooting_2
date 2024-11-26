using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 스폰할 적 프리팹
    public int maxEnemies = 8; // 맵에 존재할 수 있는 최대 적 수
    public Transform[] spawnPoints; // 적이 스폰될 위치 배열

    private int currentEnemyCount = 0;

    void Start()
    {
        // 초기 스폰
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        // 랜덤 스폰 위치 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 생성
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemyCount++;

        // 적이 죽으면 카운트를 줄이도록 설정
        newEnemy.GetComponent<Enemy>().OnEnemyDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        currentEnemyCount--;

        // 새로운 적 스폰
        SpawnEnemy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoses;
    [SerializeField] private Transform enemiesParent;

    private void Start()
    {
        StartCoroutine(SpawnToRandomPoses());
    }

    private IEnumerator SpawnToRandomPoses()
    {
        int randomTime = Random.Range(3, 6);
        yield return new WaitForSeconds(randomTime);
        int randomNum = Random.Range(0, spawnPoses.Count);
        MyEnemy currentEnemy = Instantiate(enemyPrefab, spawnPoses[randomNum].position, Quaternion.identity).GetComponent<MyEnemy>();
        currentEnemy.transform.SetParent(enemiesParent);

        StartCoroutine(SpawnToRandomPoses());
    }
}

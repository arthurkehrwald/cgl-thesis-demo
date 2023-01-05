using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 5.0f;
    [SerializeField]
    private GameObject ballPrefab;

    private void OnEnable()
    {
        StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBall()
    {
        Instantiate(ballPrefab, transform);
    }
}

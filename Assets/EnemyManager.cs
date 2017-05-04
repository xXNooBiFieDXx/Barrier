using UnityEngine;
using System.Collections;

// This enemy only move up and down and fire at player. For testing
public class EnemyManager : MonoBehaviour {
    public float mSpawnTimer;
    public GameObject mEnemy;                // The enemy prefab to be spawned.
    public Transform[] mSpawnPoints;

    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", 2.5f, 5.0f);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void Spawn() {
        int spawnPointIndex = Random.Range(0, mSpawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(mEnemy, mSpawnPoints[spawnPointIndex].position, mSpawnPoints[spawnPointIndex].rotation);
    }
}

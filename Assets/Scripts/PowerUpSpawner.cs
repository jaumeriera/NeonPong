using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    // Spawn time related
    public float spawnDelay;
    private float timeFromLastSapwn;

    // Object pooling
    private List<GameObject> powerUpPool = new List<GameObject>();
    private int amountToPool = 10;
    [SerializeField] private GameObject powerUpPrefab;

    // To check where to spawn
    private enum SpawnZones {
        Player1,
        Player2
    }
    private SpawnZones lastSpawn;
    private float player1axis = -7.9f;
    private float player2axis = 7.9f;
    private float zBound = 3.85f;

    // To assign to power ups
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        timeFromLastSapwn = 0f;
        lastSpawn = SpawnZones.Player1;
        for(int i=0; i <amountToPool; i++) {
            GameObject obj = Instantiate(powerUpPrefab);
            obj.GetComponent<PowerUp>().SetPlayer1(player1);
            obj.GetComponent<PowerUp>().SetPlayer2(player2);
            obj.SetActive(false);
            powerUpPool.Add(obj);
        }
    }

    private GameObject GetFromPool() {
        for (int i = 0; i < amountToPool; i++) {
            if(!powerUpPool[i].activeInHierarchy) {
                return powerUpPool[i]; 
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.hasPowerUpsActive()){
            if(gameManager.getState() == GameManager.State.Playing){
                timeFromLastSapwn += Time.deltaTime;
                if (timeFromLastSapwn >= spawnDelay) {
                    timeFromLastSapwn = 0;
                    // spawn power up
                    switch(lastSpawn) {
                        case SpawnZones.Player1:
                            spawnPowerUp(player2axis);
                            lastSpawn = SpawnZones.Player2;
                            break;
                        case SpawnZones.Player2:
                            spawnPowerUp(player1axis);
                            lastSpawn = SpawnZones.Player1;
                            break;
                    } 
                }
            }
        }
    }

    void spawnPowerUp(float spawnAxis) {
        GameObject powerUp = GetFromPool();
        if (powerUp != null) {
            float randomZ = Random.Range(-zBound, zBound);
            powerUp.transform.position = new Vector3 (spawnAxis, 0, randomZ);
            powerUp.gameObject.GetComponent<PowerUp>().SetRandomPowerUpType();
            powerUp.SetActive(true);
        }
    }

    public void Restart() {
        for (int i = 0; i < amountToPool; i ++){
            powerUpPool[i].SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Game Manager
    [SerializeField] GameManager gameManager;

    // For IA player
    private GameObject ball;
    private int BONUSVELOCITYPLAYER = 2;

    public int velocity = 5;
    public bool isPlayer1 = false;
    private float zBound = 3.85f;
    public GameObject powerUp;

    // Update is called once per frame
    void Update()
    {
        // Check if player want pause
        if (Input.GetAxisRaw("Pause") != 0) {
            gameManager.Pause();
        }

        // Check for player movement
        float movement;
        if (isPlayer1) {
            movement = Input.GetAxisRaw("Vertical");
            velocity += BONUSVELOCITYPLAYER;
        } else if (!isPlayer1 && !gameManager.isSinglePlayer()) {
            movement = Input.GetAxisRaw("Vertical2") ;
            velocity += BONUSVELOCITYPLAYER;
        } else {
            if (!ball) {
                searchForBall();
            }
            // IA movement
            movement = 0;
            if (ball.transform.position.z < transform.position.z) {
                movement = -1;
            } else if (ball.transform.position.z > transform.position.z){
                movement = 1;
            }
        }
        Vector3 playerPosition = transform.position;
        float newPosition = playerPosition.z + movement * velocity * Time.deltaTime;
        playerPosition.z = Mathf.Clamp(newPosition, -zBound, zBound);
        transform.position = playerPosition;

        // check for player using a powerUp
        /*
        if(isPlayer1 && Input.GetAxisRaw("Fire1") != 0 && powerUp != null){
            usePowerUp();
        }
        if(!isPlayer1 && Input.GetAxisRaw("Fire2") != 0 && powerUp != null){
            usePowerUp();
        }
        */
    }

    private void searchForBall() {
        ball = GameObject.FindGameObjectWithTag("ball");
    }

    public bool isInFront(GameObject other){
        if (isPlayer1) {
            return this.transform.position.x < other.transform.position.x;
        } else {
            return this.transform.position.x > other.transform.position.x;
        }
    }

/*
    public void usePowerUp() {
        powerUp.execute();
        powerUp = null;
    }
*/

    public void SetPowerUp(GameObject obj) {
        powerUp = obj;
    }

/*
    void OnTriggerEnter(Collider collision){
        // get power up
        if (collision.gameObject.tag == "powerUp") {
            powerUp = collision.gameObject;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
        } 
    }
*/
}

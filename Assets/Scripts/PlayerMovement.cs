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
    private bool firstIteration = true;

    public int velocity = 8;
    public bool isPlayer1 = false;
    private float zBound = 3.85f;
    public GameObject powerUp;

    // Update is called once per frame
    void Update()
    {
        // This is a shitty code to set a different velocity between player and computer
        // I would like to set it into start function but object gameManager is null at
        // this point so with my experience I'm forced to do it into update function
        if (firstIteration){
            if ((gameManager.isSinglePlayer() && isPlayer1) || (!gameManager.isSinglePlayer())) {
                velocity += BONUSVELOCITYPLAYER;
            }
            firstIteration = false;
        }
        // Check if player want pause
        if (Input.GetAxisRaw("Pause") != 0) {
            gameManager.Pause();
        }

        // Check for player movement
        float movement;
        if (isPlayer1) {
            movement = Input.GetAxisRaw("Vertical");
        } else if (!isPlayer1 && !gameManager.isSinglePlayer()) {
            movement = Input.GetAxisRaw("Vertical2") ;
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
        if(isPlayer1 && Input.GetAxisRaw("PowUp1") != 0 && powerUp != null){
            usePowerUp1();
        }
        if(!isPlayer1 && Input.GetAxisRaw("PowUp2") != 0 && powerUp != null &&  !gameManager.isSinglePlayer()){
            usePowerUp2();
        }
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

    public void usePowerUp1() {
        //powerUp.execute();
        releasePowerUp();
        gameManager.PowerUpUsed1();
        powerUp = null;
    }

    public void usePowerUp2() {
        //powerUp.execute();
        releasePowerUp();
        gameManager.PowerUpUsed2();
        powerUp = null;
    }

    public void SetPowerUp(GameObject obj) {
        powerUp = obj;
    }


    void OnTriggerEnter(Collider collision){
        // get power up
        if (collision.gameObject.tag == "PowerUp") {
            if(hasOtherPowerUp()){
                releasePowerUp();
            }
            powerUp = collision.gameObject;
            collision.gameObject.GetComponent<SphereCollider>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            if(isPlayer1){
                gameManager.SetPlayer1PowerUp(collision.gameObject);
            } else {
                gameManager.SetPlayer2PowerUp(collision.gameObject);
            }
        } 
    }

    private bool hasOtherPowerUp(){
        return powerUp != null;
    }

    private void releasePowerUp(){
        powerUp.gameObject.GetComponent<SphereCollider>().enabled = true;
        powerUp.gameObject.GetComponent<MeshRenderer>().enabled = true;
        powerUp.gameObject.SetActive(false);
    }

}

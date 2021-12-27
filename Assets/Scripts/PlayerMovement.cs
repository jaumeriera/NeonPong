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

    // For restart players
    private Vector3 INITIALSCALE = new Vector3(0.33044f, 1, 2.2113f);
    [SerializeField] private int INITIALVELOCITY = 10;

    private int velocity;
    public bool isPlayer1 = false;

    // Bounds for bars
    private float zBound = 3.85f;
    private float bigZBound = 3.23f;
    private float smallZBound = 4.25f;

    // Toc check bar state
    public bool isBigBar = false;
    public bool isSmallBar = false;
    public bool powerUpActive = false;
    
    public GameObject powerUp;

    void Start() {
        velocity = INITIALVELOCITY;
    }

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

        // For checking bounds
        Vector3 playerPosition = transform.position;
        float newPosition = playerPosition.z + movement * velocity * Time.deltaTime;
        if (isBigBar){
            playerPosition.z = Mathf.Clamp(newPosition, -bigZBound, bigZBound);
        } else if (isSmallBar){
            playerPosition.z = Mathf.Clamp(newPosition, -smallZBound, smallZBound);  
        } else { 
            playerPosition.z = Mathf.Clamp(newPosition, -zBound, zBound);
        }
        transform.position = playerPosition;

        // check for player using a powerUp
        if (!powerUpActive){
            if(isPlayer1 && Input.GetAxisRaw("PowUp1") != 0 && powerUp != null){
                powerUpActive = true;
                usePowerUp1();
            }
            if(!isPlayer1 && Input.GetAxisRaw("PowUp2") != 0 && powerUp != null &&  !gameManager.isSinglePlayer()){
                powerUpActive = true;
                usePowerUp2();
            }
        }
    }

    public void resetPlayer(){
        this.gameObject.transform.localScale = INITIALSCALE;
        this.SetVelocity(INITIALVELOCITY);
        isBigBar = false;
        isSmallBar = false;
        powerUpActive = false;
        firstIteration = true;
    }

    public void SetVelocity(int vel){
        velocity = vel;
    }

    public int GetVelocity() {
        return velocity;
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
        powerUp.GetComponent<PowerUp>().execute1();
    }

    public void DeactivatePowerUp() {
        if (isPlayer1) {
            gameManager.PowerUpUsed1();
        } else {  
            gameManager.PowerUpUsed2();
        }
    }

    public void usePowerUp2() {
        powerUp.GetComponent<PowerUp>().execute2();
    }

    public void SetPowerUp(GameObject obj) {
        powerUp = obj;
    }


    void OnTriggerEnter(Collider collision){
        // get power up
        if (collision.gameObject.tag == "PowerUp") {
            if (powerUpActive) {
                collision.gameObject.SetActive(false);
            } else {
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
    }

    private bool hasOtherPowerUp(){
        return powerUp != null;
    }

    public void releasePowerUp(){
        if (powerUp){
            powerUp.gameObject.GetComponent<SphereCollider>().enabled = true;
            powerUp.gameObject.GetComponent<MeshRenderer>().enabled = true;
            powerUp.gameObject.SetActive(false);
            powerUp = null;
        }
    }

}

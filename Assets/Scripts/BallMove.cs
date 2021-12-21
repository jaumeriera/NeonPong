using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    // Game Manager
    [SerializeField] GameManager gameManager;

    // Ball fields
    private Rigidbody ballRb;
    private float initialVelocity = 4f;
    private Vector3 translation;

    // To manage ball speed
    [SerializeField] int INCREASEEACH = 10;
    [SerializeField] float INCREASEVELOCITY = 1.4F;
    private int currentHits = 0;

    // Constants for directions
    private int DIRECTIONPLAYER1 = 1;
    private int DIRECTIONPLAYER2 = -1;

    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        float xVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        float zVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        translation = new Vector3(xVelocity, 0, zVelocity) * initialVelocity;
    }

    void Update()
    {
        switch(gameManager.getState()) {  
            case GameManager.State.Playing:
                transform.position += translation * Time.deltaTime;
                break;
            case GameManager.State.Goal1:
                if(Input.GetAxisRaw("Fire1") > 0) {
                    serve(DIRECTIONPLAYER1);
                }
                break;
            case GameManager.State.Goal2:
                if (gameManager.isSinglePlayer()){
                    StartCoroutine(AIServe());
                } else if(Input.GetAxisRaw("Fire2") > 0) {
                    serve(DIRECTIONPLAYER2);
                }
                break;
        }
        
    }

    private void serve(int direction) {
        float zVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        translation = new Vector3(-1, 0, zVelocity) * initialVelocity;
        gameManager.served();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            translation.z = translation.z * -1;
        } else if (collision.gameObject.tag == "Player") {
            currentHits += 1;
            checkForVelocityIncrease();
            changeTranslation(collision);
        }
    }

    void changeTranslation(Collision collision) {
        if (this.transform.position.x == collision.gameObject.transform.position.x) {   
            // The ball hists one side of the bar
            translation.z = translation.z * -1;
        } else if (collision.gameObject.GetComponent<PlayerMovement>().isInFront(this.gameObject)){
            translation.x = translation.x * -1;
        }
    }

    void OnTriggerEnter(Collider collision){
        // Check for ball arriving to goals
        if (collision.gameObject.tag == "ScoreZonePlayer1") {
            stopBall();
            gameManager.player2Score();
        } else if (collision.gameObject.tag == "ScoreZonePlayer2") {
            stopBall();
            gameManager.player1Score();
        }
    }

    void stopBall() {
        // Stop counting hits and stop translation
        currentHits = 0;
        translation = new Vector3(0, 0, 0);
    }

    void checkForVelocityIncrease() {
        if (currentHits % INCREASEEACH == 0) {
            translation.x = translation.x * INCREASEVELOCITY;
            translation.z = translation.z * INCREASEVELOCITY;
        }
    }

    public IEnumerator AIServe(){
        gameManager.served();
        yield return new WaitForSeconds(2);
        serve(DIRECTIONPLAYER2);
    }
}

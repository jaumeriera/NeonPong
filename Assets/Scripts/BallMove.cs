using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    // Game Manager
    [SerializeField] GameManager gameManager;

    // Ball fields
    private Rigidbody ballRb;
    private float initialVelocity = 5f;
    private Vector3 translation;

    // To manage ball speed
    [SerializeField] int INCREASEEACH = 4;
    [SerializeField] float INCREASEVELOCITY = 1.5F;
    private int currentHits = 0;

    // Constants for directions
    private int DIRECTIONPLAYER1 = 1;
    private int DIRECTIONPLAYER2 = -1;

    // To manage music
    private MusicManager musicManager;

    // Collision particles
    [SerializeField] private ParticleSystem CollisionPSP1;
    [SerializeField] private ParticleSystem CollisionPSP2;
    [SerializeField] private ParticleSystem GoalPS;

    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        float xVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        float zVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        translation = new Vector3(xVelocity, 0, zVelocity) * initialVelocity;

        // Sound
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
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
            musicManager.HitSound();
            collisionParticle(collision);
            currentHits += 1;
            checkForVelocityIncrease();
            changeTranslation(collision);
        }
    }

    void collisionParticle(Collision collision) {
        ParticleSystem CollisionPS;
        Material playerMaterial = collision.gameObject.GetComponent<Renderer>().material;
        if(collision.gameObject.GetComponent<PlayerMovement>().isPlayer1){
            CollisionPS = CollisionPSP1;
        } else {
            CollisionPS = CollisionPSP2;
        }
        CollisionPS.transform.position = this.transform.position;
        CollisionPS.Play();

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
        GoalPS.transform.position = this.transform.position;
        if (collision.gameObject.tag == "ScoreZonePlayer1") {
            stopBall();
            musicManager.GoalSound();
            GoalPS.Play();
            gameManager.player2Score();
        } else if (collision.gameObject.tag == "ScoreZonePlayer2") {
            stopBall();
            musicManager.GoalSound();
            GoalPS.Play();
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

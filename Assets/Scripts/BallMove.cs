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

    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        float xVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        float zVelocity = Random.Range(0, 2) == 0 ? -1 : 1;
        translation = new Vector3(xVelocity, 0, zVelocity) * initialVelocity;
    }

    void Update()
    {
        transform.position += translation * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Wall") {
            translation.z = translation.z * -1;
        } else if (collision.gameObject.tag == "Player") {
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

        // TODO if is a power up chek later
    }

    void stopBall() {
        translation = new Vector3(0, 0, 0);
    }
}

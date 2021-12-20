using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Game Manager
    [SerializeField] GameManager gameManager;

    // For IA player
    private GameObject ball;

    public int velocity = 7;
    public bool isPlayer1 = false;
    private float zBound = 3.85f;

    // Update is called once per frame
    void Update()
    {
        float movement;
        if (isPlayer1) {
            movement = Input.GetAxisRaw("Vertical");
        } else if (!isPlayer1 && !gameManager.isSinglePlayer()) {
            movement = Input.GetAxisRaw("Vertical2");
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
}

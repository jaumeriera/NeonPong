using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // To manage players
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;

    // Ball
    [SerializeField] private GameObject ball;

    // To manage score display

    // To manage score
    private int score1 = 0;
    private int score2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void restartPlayers() {
        player1Transform.position = new Vector3(player1Transform.position.x, 0, 0);
        player2Transform.position = new Vector3(player2Transform.position.x, 0, 0);
    }

    public void player1Score() {
        score1 += 1;
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player1Transform.position + new Vector3(0.7f, 0, 0);
        ball.transform.parent = player1Transform;
    }

    public void player2Score() {
        score2 += 1;
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player2Transform.position - new Vector3(0.7f, 0, 0);
        ball.transform.parent = player2Transform;
    }
}

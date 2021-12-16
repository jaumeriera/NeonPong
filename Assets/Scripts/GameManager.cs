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
    [SerializeField] private BallMove ballMove;

    // Score display
    [SerializeField] private ScoreDisplay display1;
    [SerializeField] private ScoreDisplay display2;

    // To manage score display

    // To manage score
    private int score1 = 0;
    private int score2 = 0;

    // To manage states
    public enum State {
        Playing,  // Ball is moving and we are playing
        Pause,  // Game is on pause
        Goal1,  // Player 1 score
        Goal2,  // Player 2 score
        End  // One of the players won
    }
    private State state;

    void Start()
    {
        state = State.Pause;
    }

    public void startMatch() {
        state = State.Playing;
    }

    public State getState() {
        return state;
    }

    void restartPlayers() {
        player1Transform.position = new Vector3(player1Transform.position.x, 0, 0);
        player2Transform.position = new Vector3(player2Transform.position.x, 0, 0);
    }

    public void player1Score() {
        state = State.Goal1;
        score1 += 1;
        print("Foo");
        display1.updateScore(score1);
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player1Transform.position + new Vector3(0.7f, 0, 0);
        ball.transform.parent = player1Transform;
    }

    public void player2Score() {
        state = State.Goal2;
        score2 += 1;
        print("Foo");
        display2.updateScore(score2);
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player2Transform.position - new Vector3(0.7f, 0, 0);
        ball.transform.parent = player2Transform;
    }

    public void served(){
        state = State.Playing;
        ball.transform.parent = null;
    }
}

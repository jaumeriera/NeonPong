using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // To manage players
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private PlayerMovement player1State;
    [SerializeField] private PlayerMovement player2State;

    // Ball
    [SerializeField] private GameObject ball;
    [SerializeField] private BallMove ballMove;

    // Score display
    [SerializeField] private ScoreDisplay display1;
    [SerializeField] private ScoreDisplay display2;

    // To control pause
    [SerializeField] private UIManager uimanager;

    // To manage settings
    private Settings settings;

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
        settings = GameObject.Find("Settings").GetComponent<Settings>();
    }

    public bool isSinglePlayer() {
        return settings.GetSinglePlayer();
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
        //TODO Reset velocity and scale
        player1State.releasePowerUp();
        PowerUpUsed1();
        player2State.releasePowerUp();
        PowerUpUsed2();
    }

    public void player1Score() {
        state = State.Goal1;
        score1 += 1;
        if(matchIsFinished()){
            // TODO Go to final screen
            doNothing();
        }
        display1.updateScore(score1);
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player1Transform.position + new Vector3(0.7f, 0, 0);
        ball.transform.parent = player1Transform;
    }

    public void player2Score() {
        state = State.Goal2;
        score2 += 1;
        if (matchIsFinished()) {
            // TODO Go to final screen
            doNothing();
        }
        display2.updateScore(score2);
        restartPlayers();
        // place ball in front of player 1
        ball.transform.position = player2Transform.position - new Vector3(0.7f, 0, 0);
        ball.transform.parent = player2Transform;
    }

    private bool matchIsFinished(){
        return score1 == 10 || score2 == 10;
    }

    public void served(){
        state = State.Playing;
        ball.transform.parent = null;
    }

    public void Pause() {
        uimanager.PauseGame();
    }

    // TODO REMOVE
    private bool doNothing(){
        return true;
    }

    public void SetPlayer1PowerUp(GameObject obj){
        uimanager.SetPowerUp1(obj);
    }

    public void SetPlayer2PowerUp(GameObject obj){
        uimanager.SetPowerUp2(obj);
    }

    public void PowerUpUsed1(){
        uimanager.UnsetPowerUp1();
    }

    public void PowerUpUsed2(){
        uimanager.UnsetPowerUp2();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private enum PowerUpType {
        freeze,
        speedUp,
        bigBar,
        smallBar,
    }

    private PowerUpType type;

    private Color FREEZELCOLOR = new Color(0, 199f/255f, 255f/255f)*3f;
    private Color SPEEDUPCOLOR = new Color(0, 255f/255f, 6f/255f)*3f;
    private Color BIGBARCOLOR = new Color(188f/255f, 0, 255f/255f)*3f;
    private Color SMALLBARCOLOR = new Color(255f/255f, 98f/255f, 0)*3f;

    private int FREEZETIME = 2;
    private int SPEEDUPTIME = 2;
    private int BIGBARTIME = 5;
    private int SMALLBARTIME = 5;

    private int SPEEDUPVALUE = 15;
    private float BIGBARLEN = 3.5f;
    private float SMALLBARLEN = 1.5f;

    public GameObject player1;
    public GameObject player2;

    
    public void execute1()
    {
        execute(player1, player2);
    }

    public void execute2()
    {
        execute(player2, player1);
    }

    private void execute(GameObject playerUser, GameObject otherPlayer){
        switch(type) {
            case PowerUpType.freeze:
                StartCoroutine(FreezePlayer(playerUser, otherPlayer));
                break;
            case PowerUpType.speedUp:
                StartCoroutine(SpeedUpPlayer(playerUser));
                break;
            case PowerUpType.bigBar:
                StartCoroutine(BigBarPlayer(playerUser));
                break;
            case PowerUpType.smallBar:
                StartCoroutine(SmallBarPlayer(playerUser, otherPlayer));
                break;
        }
    }

    public void SetPlayer1(GameObject obj){
        player1 = obj;
    }

    public void SetPlayer2(GameObject obj){
        player2 = obj;
    }
    
    public void SetRandomPowerUpType() {
        int powerUpIndex = Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        switch(powerUpIndex) {
            case 0:
                type = PowerUpType.freeze;
                setColor(FREEZELCOLOR);
                break;
            case 1:
                type = PowerUpType.speedUp;
                setColor(SPEEDUPCOLOR);
                break;
            case 2:
                type = PowerUpType.bigBar;
                setColor(BIGBARCOLOR);
                break;
            case 3:
                type = PowerUpType.smallBar;
                setColor(SMALLBARCOLOR);
                break;
        }
    }

    private void setColor(Color color) {
        this.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
    }

    public string getName(){
        switch(type){
            case PowerUpType.freeze:
                return "Freeze";
            case PowerUpType.speedUp:
                return "Speed up";
            case PowerUpType.bigBar:
                return "Big Bar";
            case PowerUpType.smallBar:
                return "Small Bar";
        }
        return "";
    }

    public IEnumerator FreezePlayer(GameObject userPlayer, GameObject otherPlayer){
        PlayerMovement otherScript = otherPlayer.gameObject.GetComponent<PlayerMovement>();
        int velocity = otherScript.GetVelocity();
        otherScript.SetVelocity(0);
        yield return new WaitForSeconds(FREEZETIME);
        otherScript.SetVelocity(velocity);
        // return power up to pool
        PlayerMovement script = userPlayer.gameObject.GetComponent<PlayerMovement>();
        script.powerUpActive = false;
        script.DeactivatePowerUp();
        script.releasePowerUp();
    }

    public IEnumerator SpeedUpPlayer(GameObject userPlayer){
        PlayerMovement script = userPlayer.gameObject.GetComponent<PlayerMovement>();
        int velocity = script.GetVelocity();
        script.SetVelocity(SPEEDUPVALUE);
        yield return new WaitForSeconds(SPEEDUPTIME);
        script.SetVelocity(velocity);
        // return power up to pool
        script.powerUpActive = false;
        script.DeactivatePowerUp();
        script.releasePowerUp();
    }

    public IEnumerator BigBarPlayer(GameObject userPlayer){
        PlayerMovement script = userPlayer.gameObject.GetComponent<PlayerMovement>();
        Transform transform = userPlayer.gameObject.GetComponent<Transform>();
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, BIGBARLEN);
        script.isBigBar = true;
        yield return new WaitForSeconds(BIGBARTIME);
        transform.localScale = scale;
        script.isBigBar = false;
        // return power up to pool
        script.powerUpActive = false;
        script.DeactivatePowerUp();
        script.releasePowerUp();
    }

    public IEnumerator SmallBarPlayer(GameObject userPlayer, GameObject otherPlayer){
        PlayerMovement otherScript = otherPlayer.gameObject.GetComponent<PlayerMovement>();
        PlayerMovement script = userPlayer.gameObject.GetComponent<PlayerMovement>();
        Transform transform = otherPlayer.gameObject.GetComponent<Transform>();
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, SMALLBARLEN);
        otherScript.isSmallBar = true;
        yield return new WaitForSeconds(SMALLBARTIME);
        transform.localScale = scale;
        otherScript.isSmallBar = false;
        // return power up to pool
        script.powerUpActive = false;
        script.DeactivatePowerUp();
        script.releasePowerUp();
    }
}

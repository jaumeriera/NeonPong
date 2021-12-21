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

/*
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
*/


    public void execute()
    {
        
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
}

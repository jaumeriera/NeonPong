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

/*
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
*/

    // Start is called before the first frame update
    void Start()
    {
        type = PowerUpType.freeze;
    }

    public void execute()
    {
        
    }

    public void SetRandomPowerUpType() {
        int powerUpIndex = Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        switch(powerUpIndex) {
            case 0:
                type = PowerUpType.freeze;
                this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 199, 255));
                break;
            case 1:
                type = PowerUpType.speedUp;
                this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0, 255, 6));
                break;
            case 2:
                type = PowerUpType.bigBar;
                this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(188, 0, 255));
                break;
            case 3:
                type = PowerUpType.smallBar;
                this.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(255, 98, 0));
                break;
        }
    }
}

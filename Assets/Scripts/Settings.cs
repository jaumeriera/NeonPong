using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private bool OnePlayer;
    private bool powerUpsActive;
    
    public bool GetOnePlayer(){
        return OnePlayer;
    }

    public void SetOnePlayer(bool onePlayer){
        OnePlayer = onePlayer;
    }

    public bool GetPowerUpsActive(){
        return powerUpsActive;
    }

    public void SetPowerUpsActive(bool active){
        powerUpsActive = active;
    }
}

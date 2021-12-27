using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool SinglePlayer;
    public bool powerUpsActive;
    
    public bool GetSinglePlayer(){
        return SinglePlayer;
    }

    public void SetSinglePlayer(bool one){
        SinglePlayer = one;
    }

    public bool GetPowerUpsActive(){
        return powerUpsActive;
    }

    public void SetPowerUpsActive(bool active){
        powerUpsActive = active;
    }

}

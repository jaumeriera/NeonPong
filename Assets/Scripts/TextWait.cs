using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWait : MonoBehaviour
{
    [SerializeField] BallMove ballMove;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("StartMatch") > 0) {
            gameManager.startMatch();
            ballMove.enabled = true;
            this.gameObject.SetActive(false);
        }
        
    }
}

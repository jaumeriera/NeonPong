using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int velocity = 7;
    public bool isPlayer1 = false;
    private float zBound = 3.85f;

    // Update is called once per frame
    void Update()
    {
        float movement;
        if (isPlayer1) {
            movement = Input.GetAxisRaw("Vertical");
        } else {
            movement = Input.GetAxisRaw("Vertical2");
        }
        Vector3 playerPosition = transform.position;
        float newPosition = playerPosition.z + movement * velocity * Time.deltaTime;
        playerPosition.z = Mathf.Clamp(newPosition, -zBound, zBound);
        transform.position = playerPosition;
    }

    public bool isInFront(GameObject other){
        if (isPlayer1) {
            return this.transform.position.x < other.transform.position.x;
        } else {
            return this.transform.position.x > other.transform.position.x;
        }
    }
}

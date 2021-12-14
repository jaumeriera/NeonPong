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
}

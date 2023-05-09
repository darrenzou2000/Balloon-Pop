using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] GameObject Player;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        float newXPosition = Player.transform.position.x - offset.x;
        float newYPosition = Player.transform.position.y - offset.y;
        transform.position = new Vector3(newXPosition, newYPosition, -10);
    }
}

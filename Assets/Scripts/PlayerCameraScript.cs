using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public float distance = 10.0f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary vector
        Vector3 temp = player.transform.position;
        temp.x = temp.x;
        temp.y = temp.y;
        temp.z = temp.z - distance;
        // Assign value to Camera position
        transform.position = temp;
    }
}

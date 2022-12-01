using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{
    Transform ship;
    public float smoothing = 5f;
    Vector3 offSet;
    public float z;

    // Use this for initialization
    void Awake()
    {
        ship = GameObject.Find("UAV Trident").GetComponent<Transform>();
        offSet = transform.position - ship.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPos = ship.position + offSet;
        camPos.z = camPos.z - z;
        transform.position = Vector3.Lerp(transform.position, camPos, smoothing * Time.deltaTime);

    }
}
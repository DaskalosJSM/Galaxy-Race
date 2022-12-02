using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorGround : MonoBehaviour
{

    public GameObject currentPlatform;
    public GameObject PreviousPlatform;
    public GameManager Manager;
    public HoverMovement Movement;

    private void Start()
    {
        Movement = GameObject.Find("UAV Trident").GetComponent<HoverMovement>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            GameObject currentGO = hit.transform.gameObject;
            if (currentGO.tag == "Platform")
            {
                currentPlatform = currentGO;
            }
            if (currentGO.tag == "Deathzone")
            {
                Movement.hoverHight = 0;
            }
        }

    }

    private void LateUpdate()
    {
        if (PreviousPlatform != currentPlatform)
        {
            if (PreviousPlatform)
            {
                SpawnManager.Instance.DestroyPlatform(PreviousPlatform.transform.root.gameObject);
            }

            PreviousPlatform = currentPlatform;
        }
    }



    private void OnGUI()
    {
        Vector3 direction = transform.up - transform.position;

        Debug.DrawRay(transform.position, direction, Color.red);
    }



}

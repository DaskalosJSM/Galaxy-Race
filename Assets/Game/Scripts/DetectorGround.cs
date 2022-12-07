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

    private void Update()
    {
        Vector3 direction = transform.up - this.transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Vector3.down, out hit))
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
                Invoke("DestroypreviousPlatform", 3.0f);
            }

            PreviousPlatform = currentPlatform;
        }
    }

    private void DestroypreviousPlatform()
    {
        SpawnManager.Instance.DestroyPlatform(PreviousPlatform.transform.root.gameObject);
    }
}

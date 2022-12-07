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
        Debug.DrawRay(this.transform.position, Vector3.down, Color.red);

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
            if (currentPlatform)
            {
                PreviousPlatform = currentPlatform;
            }
            Invoke("DestroypreviousPlatform", 8.0f);
        }
    }

    private void DestroypreviousPlatform()
    {
        SpawnManager.Instance.DestroyPlatform(PreviousPlatform.transform.root.gameObject);
    }
}

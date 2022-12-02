using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorGround : MonoBehaviour
{

    public GameObject currentPlatform;
    public GameObject PreviousPlatform;
    
 
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


        }

    }

    private void LateUpdate()
    {
        if(PreviousPlatform != currentPlatform)
        {
            if(PreviousPlatform)
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

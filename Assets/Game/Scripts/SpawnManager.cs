using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> tracks;

    GameObject lastObject;
    Transform pivote;
    private float offset = 4f;

    public Transform startPoint;

    void Start()
    {
        if (tracks != null && tracks.Count > 0)
        {
            //  tracks = tracks.OrderBy(r => r.transform.position.z).ToList();
            foreach (var item in tracks)
            {
                item.transform.position = new Vector3(0,0,0);
            }

            lastObject = tracks[0];
            lastObject = Instantiate(lastObject, startPoint.position, startPoint.rotation);

        }
        InvokeRepeating("MoveRoad",1f,0.5f);
    }
    public void MoveRoad()
    {
        if(lastObject != null)
        {
           
        }

        Transform nextPoint = lastObject.transform.Find("Pivote");

        GameObject nextPlatform = tracks[Random.Range(0, tracks.Count)];

        lastObject = Instantiate(nextPlatform, nextPoint);

        lastObject.transform.SetParent(null);

        DestroyElement(lastObject.gameObject);

       /* if(lastObject != null)
             pivote = moveroad.transform.GetChild(2).GetComponent<Transform>();
                  
        Instantiate(moveroad, pivote.position +(transform.forward *2),pivote.rotation);
        
        tracks.Add(moveroad);
        lastObject = moveroad;*/

    }

    private void DestroyElement (GameObject element)
    {
        Destroy(element, 5f);
    }

}

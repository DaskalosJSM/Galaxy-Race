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

    private int currentTracks = 0;
    public int MaxTracks = 20;

    public static SpawnManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            SpawnManager.Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }



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

            for (int i = 0; i < 10; i++)
            {
                Transform nextPoint = lastObject.transform.Find("Pivote");
                lastObject = Instantiate(lastObject, nextPoint.position, nextPoint.rotation);

            }

        }
        InvokeRepeating("MoveRoad",1f,0.5f);
    }
    public void MoveRoad()
    {
        if(currentTracks >= 15)
        {
            return;
        }

        Transform nextPoint = lastObject.transform.Find("Pivote");

        GameObject nextPlatform = tracks[Random.Range(0, tracks.Count)];

        lastObject = Instantiate(nextPlatform, nextPoint);

        lastObject.transform.SetParent(null);

        currentTracks++;
       
    }

    public void DestroyPlatform (GameObject platform)
    {
        currentTracks--;
        Destroy(platform.transform.root.gameObject, 5);
    }

}

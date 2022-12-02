using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    public StatsManager StatsManager;
    public Rigidbody rb;
    [SerializeField] float hoverForce = 9f;
    [SerializeField] float hoverHight = 2f;
    public GameObject[] hoverPoints;
    private float deadZone = 0.1f;

    public float forwardAceleration = 100;
    [SerializeField] float BackwardAceleration = 40f;

    private float currentThrust;

    [SerializeField] float forceTurn = 10f;
    float currentforceTurn;

    bool isGrounded = true;
    Vector3 moveForward;

    [SerializeField] GameObject airBrakeLeft;
    [SerializeField] GameObject airBrakeRight;
    [SerializeField] GameObject currentPlatform;
    [SerializeField] GameObject previousPlatform;



    private int layerMask;
    float aclAxis;

    private void Start()
    {
        StatsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
        rb = GetComponent<Rigidbody>();

        layerMask = 10 << LayerMask.NameToLayer("Ground");
        layerMask = ~layerMask;
    }

    // private void OnDrawGizmos()
    // {
    //     RaycastHit hit;

    //     for (int i = 0; i < hoverPoints.Length; i++)
    //     {
    //         if (Physics.Raycast(hoverPoints[i].transform.position, Vector3.down, out hit, hoverHight, layerMask))
    //         {
    //             Gizmos.color = Color.red;
    //             Gizmos.DrawLine(hoverPoints[i].transform.position, hit.point);
    //             Gizmos.DrawSphere(hit.point, 0.3f);
    //         }
    //     }
    // }

    private void Update()
    {
        // UI conection
        StatsManager.speed = rb.velocity.z;
        if (StatsManager.speed < 0)
        {
            StatsManager.speed *= -1;
        }
        
        aclAxis = Input.GetAxis("Vertical");
        moveForward = (Vector3.forward * aclAxis).normalized;

        // Turn rpobar valor absoluto 
        currentforceTurn = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > deadZone)
        {
            currentforceTurn = turnAxis;
        }
    }


    private void FixedUpdate()
    {
        // Hover Force
        RaycastHit hit;

        for (int i = 0; i < hoverPoints.Length; i++)
        {
            if (Physics.Raycast(hoverPoints[i].transform.position, Vector3.down, out hit, hoverHight, layerMask))
            {
                rb.AddForceAtPosition(Vector3.up * hoverForce * (1f - (hit.distance / hoverHight)), hoverPoints[i].transform.position);
            }
            else
            {
                if (transform.position.y > hoverPoints[i].transform.position.y)
                    rb.AddForceAtPosition(
                      hoverPoints[i].transform.up * hoverForce,
                      hoverPoints[i].transform.position);
                else
                    rb.AddForceAtPosition(
                      hoverPoints[i].transform.up * -hoverForce,
                      hoverPoints[i].transform.position);
            }
        }

        // Forward

        rb.AddRelativeForce(Vector3.forward * forwardAceleration * aclAxis);

        // turn 
        if (currentforceTurn > 0)
        {
            rb.AddRelativeTorque(Vector3.up * currentforceTurn * forceTurn);
        }
        else if (currentforceTurn < 0)
        {
            rb.AddRelativeTorque(Vector3.up * currentforceTurn * forceTurn);
        }

    }



}

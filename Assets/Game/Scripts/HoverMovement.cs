using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float hoverForce = 9f;
    [SerializeField] float hoverHight = 2f;
    public GameObject[] hoverPoints;
    private float deadZone = 0.1f;

    [SerializeField] float forwardAceleration = 100;
    [SerializeField] float BackwardAceleration = 40f;

    private float currentThrust;

    [SerializeField] float forceTurn = 10f;
    float currentforceTurn;

    [SerializeField] GameObject airBrakeLeft;
    [SerializeField] GameObject airBrakeRight;

    private int layerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        layerMask = 10 << LayerMask.NameToLayer("Ground");
        layerMask = ~layerMask;
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        for (int i = 0; i < hoverPoints.Length; i++)
        {
            if (Physics.Raycast(hoverPoints[i].transform.position, Vector3.down, out hit, hoverHight, layerMask))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(hoverPoints[i].transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.3f);
            }
        }
    }

    private void Update()
    {

        // Aceleration  probar otros valores de zona muerta
        currentThrust = 0.0f;
        float aclAxis = Input.GetAxis("Vertical");
        if (aclAxis > deadZone)
            currentThrust = aclAxis * forwardAceleration;
        else if (aclAxis < -deadZone)
            currentThrust = aclAxis * BackwardAceleration;

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

        if (Mathf.Abs(currentThrust) > 0)
        {
            rb.AddForce((transform.forward * currentThrust) * -1);
        }

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

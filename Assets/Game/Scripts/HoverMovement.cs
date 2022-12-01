using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    Rigidbody Rb;
    [SerializeField] float hoverForce = 9f;
    [SerializeField] float hoverHight = 2f;
    public GameObject[] hoverPoints;
    private float deadZone = 0.1f;

    [SerializeField] float forwardAceleration = 100;
    [SerializeField] float BackwardAceleration = 40f;

    private float currentThrust = 0;

    [SerializeField] float forceTurn = 10f;
    [SerializeField] float currentforceTurn = 0f;

    [SerializeField] GameObject airBrakeLeft;
    [SerializeField] GameObject airBrakeRight;

    private int layerMask;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

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
        float acelerationAxis = Input.GetAxis("Vertical");

        if (acelerationAxis > deadZone)
        {
            currentThrust = acelerationAxis * forwardAceleration;
        }
        else if (acelerationAxis < -deadZone)
        {
            currentThrust = acelerationAxis * BackwardAceleration;
        }

        // Turn rpobar valor absoluto 
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(currentforceTurn) > deadZone)
        {
            currentforceTurn = turnAxis;
        }
    }


    private void FixedUpdate() {
        // Hover Force
        RaycastHit hit;
 
        for ( int i = 0; i < hoverPoints.Length; i++ ){
            if( Physics.Raycast(hoverPoints[i].transform.position, Vector3.down, out hit, hoverHight, layerMask )){
                Rb.AddForceAtPosition(Vector3.up * hoverForce * (1f - (hit.distance / hoverHight)), hoverPoints[i].transform.position );

            }else
            {
                if (transform.position.y > hoverPoints[i].transform.position.y)
                    Rb.AddForceAtPosition(
                      hoverPoints[i].transform.up * hoverForce,
                      hoverPoints[i].transform.position);
                else
                    Rb.AddForceAtPosition(
                      hoverPoints[i].transform.up * -hoverForce,
                      hoverPoints[i].transform.position);
            }
        }
    if(Mathf.Abs(currentThrust) > 0){
        Rb.AddForce(transform.forward * currentThrust);
    }
    }

    // Forward

}

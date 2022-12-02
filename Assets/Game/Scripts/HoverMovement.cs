using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMovement : MonoBehaviour
{
    public StatsManager StatsManager;

    public GameManager Manager;
    public Rigidbody rb;

    [SerializeField] float hoverForce = 9f;
    public float hoverHight = 4f;
    public GameObject[] hoverPoints;
    private float deadZone = 0.1f;
    public float forwardAceleration = 100;
    [SerializeField] float forceTurn = 10f;
    float currentforceTurn;
    Vector3 moveForward;


    private int layerMask;
    float aclAxis;

    private void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StatsManager = GameObject.Find("StatsManager").GetComponent<StatsManager>();
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

        if (Input.GetKey(KeyCode.Space) && StatsManager.turbo > 0)
        {
            forwardAceleration = 7000;
            StatsManager.turbo -= 10 * Time.deltaTime;
        }
        // UI conection
        StatsManager.speed = rb.velocity.z * 10;
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
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Points"))
        {
            Destroy(other.gameObject);

            StatsManager.score += 10;
        }
        if (other.gameObject.CompareTag("Turbo"))
        {
            Destroy(other.gameObject);
            StatsManager.turbo += 50;
        }
        if (other.gameObject.CompareTag("Deathzone"))
        {
            Manager.GameOver();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Guards"))
        {
            StatsManager.health -= 5;
        }
    }

}

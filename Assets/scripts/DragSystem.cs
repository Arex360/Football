using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSystem : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    private Vector3 _mouseReleasePos;
    private Rigidbody rb;
    private bool isShoot;
    private Vector3 direction;
    private Vector3 _direction;
    private int numOfTrajectoryPoints = 30;
    private List<GameObject> trajectoryPoints;
    public Transform point;
    public bool dragged;

    public Vector3 startMousePosition;
    public Vector3 endMousePosition;
    private Ray ray;
    private RaycastHit hit;
    private LineRenderer lineRenderer;
    private TrailRenderer trailRenderer;
    public PhysicMaterial physicMat;
    private SphereCollider sphereCollider;
    private Vector3 defaultPosition;
    private void Awake()
    {
        defaultPosition = this.transform.position;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = this.GetComponent<LineRenderer>();
        sphereCollider = this.GetComponent<SphereCollider>();
        trailRenderer = this.GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }
    private void Update()
    {
        if (dragged)
        {
            _mouseReleasePos = Input.mousePosition;
            _direction = mousePressDownPos - _mouseReleasePos;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                endMousePosition = hit.point;
                endMousePosition.y = this.transform.position.y;
                lineRenderer.SetPosition(1, endMousePosition);
            }
        }
       
    }
    private void OnMouseDown()
    {
        if (GameManger.instance.gameCompleted)
            return;
        rb.isKinematic = true;
        mousePressDownPos = Input.mousePosition;
        startMousePosition = Camera.main.ScreenToWorldPoint(mousePressDownPos);
        lineRenderer.SetPosition(0, this.transform.position);
        dragged = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, startMousePosition);
        if (!GameManger.instance.gameStarted)
        {
            GameManger.instance.gameStarted = true;
            Timer.instance.StartTimer();
        }
        
    }
    private void OnMouseUp()
    {
        rb.isKinematic = false;
        dragged = false;
        mouseReleasePos = Input.mousePosition;
        direction = mousePressDownPos - mouseReleasePos;
        Shoot(mousePressDownPos - mouseReleasePos);
        lineRenderer.enabled = false;
        sphereCollider.material = null;
    }
    private float forceMultiplier = 3;
    void Shoot(Vector3 Force)
    {
        trailRenderer.enabled = true;
        if (isShoot)
            return;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;
        Invoke(nameof(resetTransform), 5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endMousePosition);
    }
    private void resetTransform()
    {
        trailRenderer.enabled = false;
        GameManger.instance.ballCaught = false;
        isShoot = false;
        this.transform.position = defaultPosition;
        rb.velocity = Vector3.zero;
        sphereCollider.material = physicMat;
        GameManger.instance.colliadable = true;
        rb.isKinematic = false;
    } 
}

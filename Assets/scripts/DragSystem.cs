using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragSystem : MonoBehaviour
{
    public Slider forceAdjuster;
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

    // test
    private float touchTimeStart;
    private float touchTimeEnd;
    private float interval;
    private Vector3 startPos;
    private Vector3 endPos;

    public float throwForceinXY;
    public float throwForceinZ;
    private float forceMultiplier = 3;
    public float zMultiplayer = 2.5f;
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
        forceMultiplier = 1f * forceAdjuster.value;
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
        // if you touch the screen
        /*
        bool startCondition = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        bool endCondition = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        if (Input.GetMouseButtonDown(0))
        {
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
            rb.isKinematic = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            touchTimeEnd = Time.time;
            interval = touchTimeEnd - touchTimeStart;
            endPos = Input.mousePosition;
            Vector3 direction = endPos - startPos;
            rb.isKinematic = false;
            sphereCollider.material = null;
            rb.AddForce(-direction.x * throwForceinXY, -direction.y * throwForceinXY, throwForceinZ / interval);
            Invoke(nameof(resetTransform), 5f);
        }
       */
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
   
    void Shoot(Vector3 Force)
    {
        trailRenderer.enabled = true;
        if (isShoot)
            return;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.magnitude*zMultiplayer) * forceMultiplier);
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

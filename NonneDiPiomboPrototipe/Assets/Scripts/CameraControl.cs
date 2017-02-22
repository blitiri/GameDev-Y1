using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f; //Delay per spostamento della camera da un punto A ad un punto B (simile al movimento di camera del platformer)
    public float m_ScreenEdgeBuffer = 4f; //Fa in modo che i Targets non arrivino al bordo preciso del Aspect
    public Transform[] m_Targets; //Array dei Targets da inserire nel GameManager successivamente e nasconderlo con HideinInspector
    public float m_Minsize = 6.5f;

    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPoistion; //Media delle posizioni dei target(sposta il camerarig nella posizione media dei Targets)

    void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
    }
	// Use this for initialization
	void Start ()
    {
	
	}

    void FixedUpdate()
    {
        Move();
        Zoom();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    
    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPoistion, ref m_MoveVelocity, m_DampTime);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for(int i=0;i<m_Targets.Length;i++)
        {
            if (!m_Targets[i].gameObject.activeSelf)
            {
                continue;
            }
            averagePos += m_Targets[i].position;
            numTargets++;
        }
        if(numTargets>0)
        {
            averagePos /= numTargets;  //media
        }
        averagePos.y = transform.position.y; //per non cambiare la posizione della y del camerarig
        m_DesiredPoistion = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPoistion);

        float size = 0f;

        for(int i=0; i<m_Targets.Length;i++)
        {
            if(!m_Targets[i].gameObject.activeSelf)
            {
                continue;
            }
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;
        size = Mathf.Max(size, m_Minsize);
        return size;
    }

    public void SetStartPositionAndSize()
    {
        FindAveragePosition();
        transform.position = m_DesiredPoistion;
        m_Camera.orthographicSize = FindRequiredSize();
    }
}

using System.Collections;
using System.Collections.Generic;
using Tango;
using UnityEngine;

public class Control : MonoBehaviour, ITangoLifecycle, ITangoDepth {
    #region tango variables
    public TangoPointCloud m_pointCloud;
    private TangoARPoseController m_tangoARPoseController;
    private TangoApplication m_tangoApplication;
    private bool waitDepthAvailable;
    #endregion

    #region detect variables
    private int m_detectScreenPointCount = 5;
    private Vector2[] m_detectScreenPoint = new Vector2[5];
    private Vector3 m_previousPosition = new Vector3(0, 0, 0);
    #endregion

    #region game variables;
    public GameObject m_marker;
    public GameObject m_floorHole;
    public GameObject m_floorObstacle;
    public GameObject m_wallHole;
    public GameObject m_monster;
    private List<GameObject> m_floorHoles = new List<GameObject>();
    private List<GameObject> m_floorObstacles = new List<GameObject>();
    private List<GameObject> m_wallHoles = new List<GameObject>();
    private List<GameObject> m_monsters = new List<GameObject>();
    #endregion

    // Use this for initialization
    void Start () {
        m_tangoApplication = FindObjectOfType<TangoApplication>();
        m_tangoARPoseController = FindObjectOfType<TangoARPoseController>();
        m_tangoApplication.Register(this);
        m_tangoApplication.RequestPermissions();
        
        m_detectScreenPoint[0] = new Vector2(Screen.width / 2.0f, Screen.height / 6.0f);
        m_detectScreenPoint[1] = new Vector2(Screen.width / 6.0f, Screen.height / 2.0f);
        m_detectScreenPoint[2] = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        m_detectScreenPoint[3] = new Vector2(5 * Screen.width / 6.0f, Screen.height / 2.0f);
        m_detectScreenPoint[4] = new Vector2(Screen.width / 2.0f, 5 * Screen.height / 6.0f);
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            AndroidHelper.AndroidQuit();
        }
        if(Vector3.Distance(m_tangoARPoseController.m_tangoPosition, m_previousPosition) > 3)
        {
            m_previousPosition = m_tangoARPoseController.m_tangoPosition;
            StartCoroutine(WaitForDepthAndDetectPlanes());
        }
        //GetInput();
    }

    public void OnDestroy()
    {
        for(int i=m_floorHoles.Count-1; i>=0; i--)
        {
            Destroy(m_floorHoles[i]);
        }
        m_floorHoles.Clear();

        for(int i=m_floorObstacles.Count-1; i>=0; i--)
        {
            Destroy(m_floorObstacles[i]);
        }
        m_floorObstacles.Clear();

        for(int i=m_wallHoles.Count-1; i>=0; i--)
        {
            Destroy(m_wallHoles[i]);
        }
        m_wallHoles.Clear();

        m_tangoApplication.Unregister(this);
    }

    public void OnGUI()
    {
        
    }

    public void OnTangoPermissions(bool permission)
    {

    }

    public void OnTangoServiceConnected()
    {
        m_tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.DISABLED);
    }

    public void OnTangoServiceDisconnected()
    {

    }

    public void OnTangoDepthAvailable(TangoUnityDepth tangoDepth)
    {
        waitDepthAvailable = false;
    }

    private void GetInput()
    {
        if(Input.touchCount==1)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase!=TouchPhase.Began)
            {
                return;
            }
            StartCoroutine(WaitForDepthAndFindPlane());
        }
    }

    private IEnumerator WaitForDepth()
    {
        waitDepthAvailable = true;
        m_tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.MAXIMUM);
        while (waitDepthAvailable)
        {
            yield return null;
        }
        m_tangoApplication.SetDepthCameraRate(TangoEnums.TangoDepthCameraRate.DISABLED);
    }

    private IEnumerator WaitForDepthAndDetectPlanes()
    {
        StartCoroutine(WaitForDepth());
        Camera cam = Camera.main;
        Vector3 planeCenter;
        Plane findedPlane;
        for(int i=0; i<m_detectScreenPointCount; i++)
        {
            if(m_pointCloud.FindPlane(cam, m_detectScreenPoint[i], out planeCenter, out findedPlane))
            {
                Vector3 forward;
                if(Vector3.Angle(findedPlane.normal, cam.transform.forward) < 170)
                {
                    forward = Vector3.Cross(Vector3.Cross(findedPlane.normal, cam.transform.forward).normalized, findedPlane.normal).normalized;
                }
                else
                {
                    forward = Vector3.Cross(findedPlane.normal, cam.transform.right);
                }

                if(Vector3.Angle(findedPlane.normal, new Vector3(0, 1, 0)) < 80)
                {
                    if (Random.value < 0.5)
                    {
                        m_floorHoles.Add(Instantiate(m_floorHole, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                    }
                    else
                    {
                        m_floorObstacles.Add(Instantiate(m_floorObstacle, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                    }
                }
                else if(Vector3.Angle(findedPlane.normal, new Vector3(1, 0, 0))<80 || Vector3.Angle(findedPlane.normal, new Vector3(-1, 0, 0)) < 80)
                {
                    m_wallHoles.Add(Instantiate(m_wallHole, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                }
            }
            yield return null;
        }
    }

    private IEnumerator WaitForDepthAndFindPlane()
    {
        StartCoroutine(WaitForDepth());

        Camera cam = Camera.main;
        Vector2 screenPosition;
        Vector3 planeCenter;
        Plane findedPlane;
        screenPosition.x = Screen.width / 2;
        screenPosition.y = Screen.height / 2;
        if(!m_pointCloud.FindPlane(cam, screenPosition, out planeCenter, out findedPlane))
        {
            yield break;
        }
        Vector3 up = findedPlane.normal;
        Vector3 forward;
        if (Vector3.Angle(up, cam.transform.forward) < 175)
        {
            Vector3 right = Vector3.Cross(up, cam.transform.forward).normalized;
            forward = Vector3.Cross(right, up).normalized;
        }
        else
        {
            forward = Vector3.Cross(up, cam.transform.right);
        }
        Instantiate(m_marker, planeCenter, Quaternion.LookRotation(forward, up));
    }
}

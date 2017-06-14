using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Tango;
using UnityEngine;

public class Control : MonoBehaviour, ITangoLifecycle, ITangoDepth {
    #region tango variables
    public TangoPointCloud m_pointCloud;
    private TangoARPoseController m_tangoARPoseController;
    private TangoApplication m_tangoApplication;
    #endregion

    #region detect variables
    private bool waitDepthAvailable;
    private float m_time = 0.0f;
    private Vector3 m_previousPosition = new Vector3(0, 0, 0);
    #endregion

    #region game variables;
    public GameObject m_ZomBunnyHole;
    public GameObject m_ZomBearHole;
    public GameObject m_HellephantHole;

    public List<GameObject> m_ZomBunnyHoles = new List<GameObject>();
    public List<GameObject> m_ZomBearHoles = new List<GameObject>();
    public List<GameObject> m_HellephantHoles = new List<GameObject>();

    private Vector3 m_xAxis = new Vector3(1, 0, 0);
    private Vector3 m_xNagetiveAxis = new Vector3(-1, 0, 0);
    private Vector3 m_yAxis = new Vector3(0, 1, 0);
    #endregion

    // Use this for initialization
    void Start () {
        m_tangoApplication = FindObjectOfType<TangoApplication>();
        m_tangoARPoseController = FindObjectOfType<TangoARPoseController>();
        m_tangoApplication.Register(this);
        m_tangoApplication.RequestPermissions();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            AndroidHelper.AndroidQuit();
        }

        if(Vector3.Distance(m_tangoARPoseController.m_tangoPosition, m_previousPosition) > 1.0f)
        {
            m_previousPosition = m_tangoARPoseController.m_tangoPosition;

            foreach(var target in m_ZomBunnyHoles.Where(x => Vector3.Distance(m_previousPosition, x.transform.position) > 5.0f).ToArray())
            {
                m_ZomBunnyHoles.Remove(target);
                Destroy(target);
            }

            foreach (var target in m_ZomBearHoles.Where(x => Vector3.Distance(m_previousPosition, x.transform.position) > 5.0f).ToArray())
            {
                m_ZomBearHoles.Remove(target);
                Destroy(target);
            }

            foreach (var target in m_HellephantHoles.Where(x => Vector3.Distance(m_previousPosition, x.transform.position) > 5.0f).ToArray())
            {
                m_HellephantHoles.Remove(target);
                Destroy(target);
            }
        }

        m_time += Time.deltaTime;
        if (m_time > 15.0f)
        {
            m_time -= 15.0f;
            StartCoroutine(WaitForDepthAndDetectPlanes());
        }
        //GetInput();
    }

    public void OnDestroy()
    {
        for (int i = m_ZomBunnyHoles.Count - 1; i >= 0; i--) 
        {
            Destroy(m_ZomBunnyHoles[i]);
        }
        m_ZomBunnyHoles.Clear();

        for (int i = m_ZomBearHoles.Count - 1; i >= 0; i--) 
        {
            Destroy(m_ZomBearHoles[i]);
        }
        m_ZomBearHoles.Clear();

        for (int i = m_HellephantHoles.Count - 1; i >= 0; i--) 
        {
            Destroy(m_HellephantHoles[i]);
        }
        m_HellephantHoles.Clear();

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
        for(int i=0; i<2; i++)
        {
            for(int j=0; j<2; j++)
            {
                Vector2 point = new Vector2((i + Random.value) * Screen.width / 2, (j + Random.value) * Screen.height / 2);
                if(m_pointCloud.FindPlane(cam, point, out planeCenter, out findedPlane))
                {
                    Vector3 forward;
                
                    if (Vector3.Angle(findedPlane.normal, cam.transform.forward) < 175)
                    {
                        forward = Vector3.Cross(Vector3.Cross(findedPlane.normal, cam.transform.forward).normalized, findedPlane.normal).normalized;
                    }
                    else
                    {
                        forward = Vector3.Cross(findedPlane.normal, cam.transform.right);
                    }

                    float angle = Vector3.Angle(findedPlane.normal, m_yAxis);
                    if (angle < 80.0f)
                    {
                        if (Random.value < 0.5)
                        {
                            m_ZomBunnyHoles.Add(Instantiate(m_ZomBunnyHole, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                        }
                        else
                        {
                            m_ZomBearHoles.Add(Instantiate(m_ZomBearHole, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                        }
                    }
                    else if (angle < 110.0f) 
                    {
                        m_HellephantHoles.Add(Instantiate(m_HellephantHole, planeCenter, Quaternion.LookRotation(forward, findedPlane.normal)));
                    }
                }
                yield return null;
            }
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
        //Instantiate(m_marker, planeCenter, Quaternion.LookRotation(forward, up));
    }
}

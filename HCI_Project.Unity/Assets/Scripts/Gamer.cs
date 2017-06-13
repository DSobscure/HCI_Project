using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamer : MonoBehaviour
{

    public Camera m_camera;

	void Update ()
    {
        var arTransform = m_camera.transform;
        transform.position = new Vector3(arTransform.position.x, arTransform.position.y, arTransform.position.z);
    }
}

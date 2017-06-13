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
        //transform.rotation = arTransform.rotation;//Quaternion.Euler(0, arTransform.rotation.eulerAngles.y, arTransform.rotation.eulerAngles.z);
    }
}

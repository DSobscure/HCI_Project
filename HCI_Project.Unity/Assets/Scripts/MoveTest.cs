using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
	void Update ()
    {
        GetComponent<TangoPoseController>().
        //transform.Rotate(Vector3.up * (Time.deltaTime * 1));
        transform.position = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad), 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public GameObject firbolt;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Instantiate(firbolt, transform);
    }
}

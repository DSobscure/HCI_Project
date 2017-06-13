using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombunny : MonoBehaviour {
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (target.transform.position - this.transform.position);
        direction.y = 0.0f;
        direction.Normalize();
        this.transform.rotation = Quaternion.LookRotation(direction);
        this.transform.position += 0.05f * direction;
	}
}

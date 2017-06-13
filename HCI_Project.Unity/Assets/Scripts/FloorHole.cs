using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHole : MonoBehaviour {
    public GameObject m_monster;
    private float m_time = 0.0f;
	// Use this for initialization
	void Start () {
        Instantiate(m_monster, this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime;
        if (m_time > 3.0f)
        {
            Instantiate(m_monster, this.transform.position, Quaternion.identity);
            m_time -= 3.0f;
        }
    }        
}

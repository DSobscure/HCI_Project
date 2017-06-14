using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject m_Control;
        public GameObject m_Hellphant;
        public GameObject m_ZomBunny;
        public GameObject m_ZomBear;

        private float m_frequency = 0.1f;
        private float m_timeInterval = 5.0f;
        private float m_time = 0.0f;

        void Start ()
        {
            StartCoroutine(AddWave());   
        }

        void Update()
        {
            m_time += Time.deltaTime;
            if (m_time > m_timeInterval)
            {
                int index;
                if (m_Control.GetComponent<Control>().m_ZomBearHoles.Count > 0)
                {
                    index = Mathf.FloorToInt(Random.value * m_Control.GetComponent<Control>().m_ZomBearHoles.Count);
                    Instantiate(m_ZomBear, m_Control.GetComponent<Control>().m_ZomBearHoles[index].transform.position, Quaternion.identity);
                }

                if (m_Control.GetComponent<Control>().m_ZomBunnyHoles.Count > 0)
                {
                    index = Mathf.FloorToInt(Random.value * m_Control.GetComponent<Control>().m_ZomBunnyHoles.Count);
                    Instantiate(m_ZomBunny, m_Control.GetComponent<Control>().m_ZomBunnyHoles[index].transform.position, Quaternion.identity);
                }

                if (m_Control.GetComponent<Control>().m_HellephantHoles.Count > 0)
                {
                    index = Mathf.FloorToInt(Random.value * m_Control.GetComponent<Control>().m_HellephantHoles.Count);
                    Instantiate(m_Hellphant, m_Control.GetComponent<Control>().m_HellephantHoles[index].transform.position, Quaternion.identity);
                }
               
                m_time -= m_timeInterval;
            }
        }
        
        private IEnumerator AddWave()
        {
            while (true)
            {
                yield return new WaitForSeconds(20);
                Global.Game.Wave++;
                m_frequency *= 1.2f;
                m_timeInterval = 1.0f / m_frequency;
                m_Control.GetComponent<Control>().m_timeInterval *= 0.9f;
            }
        }
    }
}
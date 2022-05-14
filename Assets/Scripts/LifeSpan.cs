using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    private enum m_lifeState {Enfant, Adulte, Mort};
    private m_lifeState m_currentState = m_lifeState.Enfant;

    [SerializeField] private float m_lifeSpan = 10f;
    [SerializeField] private Animation m_animDeath;
    [SerializeField] [Range(1, 100)] private float m_pourcent;

    private float m_currentLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        m_currentLifeSpan = m_lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentState != m_lifeState.Mort)
        {      
            switch (m_currentLifeSpan)
            {
                case var value when value < 0:
                    m_currentState = m_lifeState.Mort;
                    m_animDeath.Play();
                    break;

                case var value when value < m_lifeSpan*(100-m_pourcent)/100:
                    m_currentState = m_lifeState.Adulte;
                    break;

                default:
                    break;
            }
            switch (m_currentState)
            {
                case m_lifeState.Enfant:
                    break;

                case m_lifeState.Adulte:
                    //transform.localScale = new Vector3(2, 4, 2);
                    break;

                case m_lifeState.Mort:
                    break;
            }
        }
        //Debug.Log(m_currentLifeSpan);
        //Debug.Log(m_lifeSpan * (100 - m_pourcent) / 100);
        m_currentLifeSpan -= Time.deltaTime;
        
    }
}

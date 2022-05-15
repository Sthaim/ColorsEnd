using System;
using UnityEngine;

[System.Serializable]
public class LaunchAnim : MonoBehaviour
{
    [SerializeField, Tooltip("l'animator controller du gameobject")] private Animator m_animator;

    [SerializeField] private bool IsActivated;
    public void ActivateAnim()
    {
        m_animator?.SetBool("Activated", true);
        Debug.Log(m_animator.GetBool("Activated"));
    }

    public void DeactivateAnim()
    {
        m_animator?.SetBool("Activated", false);
        Debug.Log(m_animator.GetBool("Activated"));
    }

    void Start()
    {
        if (!IsActivated) return;
        m_animator?.SetBool("Activated", true);
        Debug.Log(m_animator.GetBool("Activated"));

    }

    private void Update()
    {
        
    }

}
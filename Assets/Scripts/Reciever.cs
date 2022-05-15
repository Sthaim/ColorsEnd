using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public ClickMovement m_Leader;

    public bool canChangeLeader = true ;

    public void SetLeader(ClickMovement _ref)
    {
        //m_Leader = _ref;
        m_Leader.SetPlayerRef(_ref);
        Debug.Log("je suis cliqué");
        canChangeLeader = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayerMove : MonoBehaviour
{
    public ClickMovement player;
    public Follower fol;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(player != null)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("walk", true);
            }
            if (Vector3.Distance(player.m_agent.destination, player.transform.position) < 1)
            {
                anim.SetBool("walk", false);
            }
        }

        if(fol != null)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("walk", true);
            }
            if (Vector3.Distance(fol.m_agent.destination, fol.transform.position) < 1)
            {
                anim.SetBool("walk", false);
            }
        }
        
    }
}

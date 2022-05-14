using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBlobTest : MonoBehaviour
{

    public Animator anim;
    private bool run = false;
    private bool canChange = true;

    // Start is called before the first frame update
    void Start()
    {
        //anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (!canChange) return;
            run = !run;
            if (run)
            {
                
                anim.SetBool("IsMoving", true);
                Debug.Log("change anim");
                return ;
               
            }
            anim.SetBool("IsMoving", false);

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            canChange = true;
        }
    }
}

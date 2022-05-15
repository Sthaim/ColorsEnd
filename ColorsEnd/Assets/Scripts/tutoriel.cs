using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutoriel : MonoBehaviour
{
    public GameObject feuille;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject dernierePage;

    protected float etape = 1;

    // Start is called before the first frame update
    void Start()
    {
        dernierePage.SetActive(false);
        feuille.SetActive(true);
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
        page4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(etape == 2) {dernierePage.SetActive(false); page2.SetActive(true); feuille.SetActive(true); };
            if(etape == 3) {dernierePage.SetActive(false); page3.SetActive(true); feuille.SetActive(true); };
            if(etape == 4) {dernierePage.SetActive(false); page4.SetActive(true); feuille.SetActive(true); };
        }

        if (Input.GetKeyDown("backspace"))
        {
            etape = 2;
        }
    }

    public void TutoPage1()
    {
        Debug.Log("fef");
        feuille.SetActive(false);
        page1.SetActive(false);
        etape = 2;
        dernierePage.SetActive(true);
    }
    public void TutoPage2()
    {
        feuille.SetActive(false);
        page2.SetActive(false);
        etape = 3;
        dernierePage.SetActive(true);
    }
    public void TutoPage3()
    {
        feuille.SetActive(false);
        page3.SetActive(false);
        etape = 4;
        dernierePage.SetActive(true);
    }
    public void TutoPage4()
    {
        feuille.SetActive(false);
        page4.SetActive(false);
        etape = 0;
    }
}

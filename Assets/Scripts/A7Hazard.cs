using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A7Hazard : MonoBehaviour
{
    [HideInInspector]public GameObject HazardWorker1; //manually fill in hazard worker
    public GameObject HazardWorker2;
    public GameObject HazardWorker3;
    private Color m_oldColor = new Color(1f, 1f, 1f, 0f);
    private Color m_red = new Color(1f, 0f, 0f, 0.1f);


    private void OnTriggerEnter(Collider targetworker)
    {
        if (targetworker.gameObject.tag == "hazard")
        {
            if (targetworker.gameObject == HazardWorker1)
            {
                //(HazardWorker1);
                Debug.Log("Worker 1 in hazard");
                HazardWorker1.transform.Find("Cube").gameObject.SetActive(true);
            }
            if (targetworker.gameObject == HazardWorker2)
            {
                //switchTag(HazardWorker2);
                Debug.Log("Worker 2 in hazard"); 
                HazardWorker2.transform.Find("Cube").gameObject.SetActive(true);
            }
            if (targetworker.gameObject == HazardWorker3)
            {
                //switchTag(HazardWorker3);
                Debug.Log("Worker 3 in hazard");
                HazardWorker3.transform.Find("Cube").gameObject.SetActive(true);
            }

            //Debug.Log("Object entered the hazard zone");
            Renderer render = GetComponent<Renderer>();
            m_oldColor = render.material.color;
            render.material.color = m_red;
            //switchTag(hazard1);
            
        }
    }

    private void OnTriggerStay(Collider targetworker)
    {
        if (targetworker.gameObject.tag == "hazard")
        {
            //Debug.Log("Object is within the hazard zone");
        }
    }

    private void OnTriggerExit(Collider targetworker)
    {
        if (targetworker.gameObject.tag == "hazard")
        {
            if (targetworker.gameObject == HazardWorker1)
            {
                //switchTag(HazardWorker1);
                HazardWorker1.transform.Find("Cube").gameObject.SetActive(false);
            }
            if (targetworker.gameObject == HazardWorker2)
            {
                //switchTag(HazardWorker2);
                HazardWorker2.transform.Find("Cube").gameObject.SetActive(false);
            }
            if (targetworker.gameObject == HazardWorker3)
            {
                //switchTag(HazardWorker3);
                HazardWorker3.transform.Find("Cube").gameObject.SetActive(false);
            }

            //Debug.Log("Object left the hazard zone");
            Renderer render = GetComponent<Renderer>();
            render.material.color = m_oldColor;
            //switchTag(hazard1);
            
        }
    }

    private void switchTag(GameObject Tag)
    {
        if (Tag.transform.GetChild(0).gameObject.activeSelf)
            Tag.transform.GetChild(0).gameObject.SetActive(false);
        else
            Tag.transform.GetChild(0).gameObject.SetActive(true);
    }

}

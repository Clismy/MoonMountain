using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_dig_out_event : MonoBehaviour
{
    [SerializeField] private GameObject[] acctivate;
    [SerializeField] private GameObject[] deActivate;






  
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject a in acctivate)
            a.SetActive(true);
        foreach (GameObject a in deActivate)
            a.SetActive(false);
    }
}

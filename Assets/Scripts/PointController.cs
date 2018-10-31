using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ObeliskController.I.point++;
            gameObject.SetActive(false);
            
        }
    }
    // Update is called once per frame
    void Update () {

        
    }
}

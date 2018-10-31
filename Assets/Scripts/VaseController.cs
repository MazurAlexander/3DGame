using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour {

    public static VaseController I;

    [SerializeField] private GameObject Cloud;
    [SerializeField] private GameObject FireWorks;

    public int objNumber;
    public bool checkObject = false;
    public bool cloudParcial = false;
    public bool fireworksParcial = false;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        objNumber = 2;
    }
    // Use this for initialization

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.I.vase = true;

        }
    }


    public void CloudOrFire() {
        GameController.I.objActive = true;
        checkObject = true;
        GameController.I.ObjectActive();
        if (cloudParcial)
        {
            var cloudpart = Instantiate(Cloud);
            cloudpart.transform.position = gameObject.transform.position;
            cloudpart.GetComponent<ParticleSystem>().Play();

        }
        if (fireworksParcial)
        {
            var firePart = Instantiate(FireWorks);
            firePart.transform.position = gameObject.transform.position;
            firePart.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<BoxCollider>().isTrigger = false;

        }

    }
}

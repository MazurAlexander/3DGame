using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{

    public static SquareController I;

    [SerializeField] private GameObject Cloud;
    [SerializeField] private GameObject FireWorks;

    public bool cloudParcial = false;
    public bool fireworksParcial = false;
    public int objNumber;
    public bool checkObject = false;
    private bool checkAnim = true;


    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        objNumber = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            checkAnim = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerController.I.pl_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle2"))
            {
                if (checkAnim)
                {

                    GameController.I.objActive = true;
                    checkObject = true;
                    GameController.I.ObjectActive();
                    if (cloudParcial)
                    {
                        var cloudpart = Instantiate(Cloud);
                        cloudpart.transform.position = gameObject.transform.position;
                        cloudpart.GetComponent<ParticleSystem>().Play();
                        checkAnim = false;
                    }
                    if (fireworksParcial)
                    {
                        var firePart = Instantiate(FireWorks);
                        firePart.transform.position = gameObject.transform.position;
                        firePart.GetComponent<ParticleSystem>().Play();
                        gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
                        gameObject.GetComponent<CapsuleCollider>().enabled = false;

                    }

                }
            }
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskController : MonoBehaviour
{

    public static ObeliskController I;

    [SerializeField] private GameObject Point_1;
    [SerializeField] private GameObject Point_2;
    [SerializeField] private GameObject Point_3;
    [SerializeField] private GameObject Point_4;
    [SerializeField] private GameObject Point_5;
    [SerializeField] private GameObject Point_6;
    [SerializeField] private GameObject Point_7;
    [SerializeField] private GameObject Point_8;
    [SerializeField] private GameObject Cloud;
    [SerializeField] private GameObject FireWorks;

    public int objNumber;
    public bool showBoxes;
    public bool checkObject = false;
    public bool cloudParcial = false;
    public bool fireworksParcial = false;
    public int point;
    private bool boxesOn = true;
    private List<GameObject> Points = new List<GameObject>();

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        objNumber = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showBoxes = true;
            if (boxesOn)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i].gameObject.SetActive(showBoxes);
                }
            }
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showBoxes = false;
            point = 0;

                for (int i = 0; i < Points.Count; i++)
                {
                    Points[i].gameObject.SetActive(showBoxes);
                }

        }

    }



    void Start()
    {
        FillList();

    }


    // Update is called once per frame
    void Update()
    {
        SumPoints();
    }

    private void SumPoints()
    {
        if (point == 8)
        {
            GameController.I.objActive = true;
            checkObject = true;
            GameController.I.ObjectActive();
            if (cloudParcial)
            {
                var cloudpart = Instantiate(Cloud);
                cloudpart.transform.position = gameObject.transform.position;
                cloudpart.GetComponent<ParticleSystem>().Play();
                point = 0;
            }
            if (fireworksParcial)
            {
                var firePart = Instantiate(FireWorks);
                firePart.transform.position = gameObject.transform.position;
                firePart.GetComponent<ParticleSystem>().Play();
                point = 0;
                boxesOn = false;
            }

        }
    }

    private void FillList()
    {
        Points.Add(Point_1);
        Points.Add(Point_2);
        Points.Add(Point_3);
        Points.Add(Point_4);
        Points.Add(Point_5);
        Points.Add(Point_6);
        Points.Add(Point_7);
        Points.Add(Point_8);

    }
}

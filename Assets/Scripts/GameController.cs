using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public static GameController I;

    public GameObject player;
    public GameObject vase;
    public GameObject obelisk;
    public GameObject square;
    public GameObject banner;

    [SerializeField] public RectTransform rect;

    [SerializeField] private Image first;
    [SerializeField] private Image second;
    [SerializeField] private Image last;

    public List<Image> ListIcon;
    public bool objActive = false;
    private int iconCount = 0;
    private int[] iconArray;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        SpawnPlayer();
    }
    // Use this for initialization
    void Start()
    {

        SpawnVase();
        SpawnObelisk();
        SpawnBanner();
        RandomTurnOn();
    }


    private void SpawnPlayer()
    {

        int pl_x = Random.Range(-56, 56);
        int pl_z = Random.Range(-56, 56);

        player.transform.position = new Vector3(pl_x, 0, pl_z);
        Debug.Log(pl_x);
        Debug.Log(pl_z);
    }
    private void SpawnVase()
    {
        var Vase = Instantiate(vase);
        int pl_x = Random.Range(-50, 51);
        int pl_z = Random.Range(-56, 51);
        Vase.transform.position = new Vector3(pl_x, 0, pl_z);
    }
    private void SpawnObelisk()
    {
        var Obelisk = Instantiate(obelisk);
        int pl_x = Random.Range(-50, 51);
        int pl_z = Random.Range(-50, 51);
        Obelisk.transform.position = new Vector3(pl_x, 0, pl_z);
    }
    private void SpawnBanner()
    {
        var Square = Instantiate(square);
        var Banner = Instantiate(banner);
        int pl_x = Random.Range(-50, 51);
        int pl_z = Random.Range(-50, 51);
        Banner.transform.position = new Vector3(pl_x, 0, pl_z);
        Square.transform.position = new Vector3(pl_x + 1, 0, pl_z + 1);
    }

    private void RandomTurnOn()
    {
        var firstIcon = Instantiate(first);
        var secondIcon = Instantiate(second);
        var lastIcon = Instantiate(last);
        iconArray = new int[3];

        bool ind = false;
        for (int i = 0; i < iconArray.Length; i++)
        {
            iconArray[i] = Random.Range(0, 3);
            for (int j = i - 1; j >= 0; j--)
                if (iconArray[i] == iconArray[j]) ind = true;
            if (ind) i = i - 1;
            ind = false;
        }
        for (int i = 0; i < iconArray.Length; i++)
        {
            if (iconArray[i] == 0)
            {
                ListIcon.Add(first);
                firstIcon.transform.SetParent(rect);
                firstIcon.transform.position = new Vector3((Screen.width /2  -  (-50 * i)), Screen.height - 25);
            }
            if (iconArray[i] == 1)
            {
                ListIcon.Add(second);
                secondIcon.transform.SetParent(rect);
                secondIcon.transform.position = new Vector3((Screen.width /2 - (-50 * i)), Screen.height - 25);
            }
            if (iconArray[i] == 2)
            {
                ListIcon.Add(last);
                lastIcon.transform.SetParent(rect);
                lastIcon.transform.position = new Vector3((Screen.width/2  - (-50 * i)), Screen.height  - 25);
            }
        }
    }

    public void ObjectActive()
    {
        if (objActive)
        {
            if (ObeliskController.I.checkObject)
            {
                if (ObeliskController.I.objNumber == iconArray[iconCount])
                {
                    ObeliskController.I.fireworksParcial = true;
                    iconCount++;
                    ObeliskController.I.checkObject = false;
                    ObeliskController.I.cloudParcial = false;
                }
                else
                {
                    ObeliskController.I.cloudParcial = true;
                    ObeliskController.I.checkObject = false;
                }
            }

            if (SquareController.I.checkObject)
            {
                if (SquareController.I.objNumber == iconArray[iconCount])
                {
                    SquareController.I.fireworksParcial = true;
                    iconCount++;
                    SquareController.I.checkObject = false;
                    SquareController.I.cloudParcial = false;
                }
                else
                {
                    SquareController.I.cloudParcial = true;
                    SquareController.I.checkObject = false;
                }
            }

            if (VaseController.I.checkObject)
            {
                if (VaseController.I.objNumber == iconArray[iconCount])
                {
                    VaseController.I.fireworksParcial = true;
                    iconCount++;
                    VaseController.I.checkObject = false;
                    VaseController.I.cloudParcial = false;
                }
                else
                {
                    VaseController.I.cloudParcial = true;
                    VaseController.I.checkObject = false;
                }
            }

            objActive = false;
        }

    }

    private void RestartLvl()
    {
        
        SceneManager.LoadScene("Scene");
    }
    // Update is called once per frame
    void Update()
    {
        
        ObjectActive();
        Debug.Log(iconArray[0]);
        Debug.Log(iconArray[1]);
        Debug.Log(iconArray[2]);
        if (iconCount==3)
        {
            RestartLvl();
            iconCount = 0;
        }
        
    }
}

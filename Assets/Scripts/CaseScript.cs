using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using TMPro.EditorUtilities;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.Tilemaps;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CaseScript : MonoBehaviour
{
    public Inventory InventoryClass;



    public int countCaseOpener=0;
    public bool openCase = false;
    public GameObject[] prefabs;
    public GameObject sp; //scrollPanel
    public float scrollSpeed = -20;
    private float velocity = 0;
    public WSprites[] ws;
    public Image[] prefabsImages;
    public Image finalDrop;
    public bool dropPanel=false;
    public GameObject dropPan;
    public int currentCase;
    public int[] CasePrices = new int[] { 50, 100 };

    public GameObject noMoneyPanel;

    public Button SellBttn;

    public Button BackBttn;

    public Button openCaseAgain;

    public GameObject prefab;

 

    bool randed = false;
    public Text itemPrice;
    public static float currentPrice = 2f;

    public Text BalanceText;
    private float balance=10000;

    void Start()
    {
        for (int a = 0; a < prefabs.Length; a++)
        {
            prefabsImages[a].sprite = prefabs[a].GetComponent<Image>().sprite;
        }
        BalanceText.text = balance.ToString() + "$";
    }
    void DestroyChildren()
    {
        for (int i = 50; i >= 0; i--)
        {
            Destroy(sp.transform.GetChild(i).gameObject);

        }
    }
    void Update()
    {
        BalanceText.text = balance.ToString() + "$";
        if (openCase)
        {
            scrollSpeed=Mathf.MoveTowards(scrollSpeed,0,velocity*Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if ((scrollSpeed == 0) && (!dropPanel) && (hit.collider!=null))
            {
                dropPanel = true;
                dropPan.SetActive(true);
                finalDrop.sprite = hit.collider.gameObject.GetComponent<Image>().sprite;
                if ((hit.collider.gameObject.tag == "Blue")&&(!randed))
                {
                    randed = true;
                    currentPrice = (float)Random.Range(300, 1000) / 100;
                    itemPrice.text = currentPrice.ToString() + "$";
                }
                else if((hit.collider.gameObject.tag == "Purple") && (!randed))
                {
                    randed = true;
                    currentPrice = (float)Random.Range(1000, 10000) / 100;
                    itemPrice.text = currentPrice.ToString() + "$";
                }
                else if ((hit.collider.gameObject.tag == "Pink") && (!randed))
                {
                    randed = true;
                    currentPrice = (float)Random.Range(7000, 25000)/100;
                    itemPrice.text = currentPrice.ToString() + "$";
                }
                else if ((hit.collider.gameObject.tag == "Red") && (!randed))
                {
                    randed = true;
                    currentPrice = (float)Random.Range(20000, 50000)/100;
                    itemPrice.text = currentPrice.ToString()+"$";
                }
                else if ((hit.collider.gameObject.tag == "Gold") && (!randed))
                {
                    randed = true;
                    currentPrice = (float)Random.Range(45000, 100000)/100;
                    itemPrice.text = currentPrice.ToString() + "$";
                }
            }
            else if ((scrollSpeed == 0) && (!dropPanel))
            { 
                scrollSpeed = Mathf.MoveTowards(scrollSpeed, -5f, velocity * Time.deltaTime);
            }
        }
        sp.transform.Translate(new Vector2(scrollSpeed, 0) * Time.deltaTime);
        

        //Кнопка недоступна, пока крутится
        if (scrollSpeed != 0) openCaseAgain.interactable = false;
        else openCaseAgain.interactable = true;
        
    }
    public void caseBttn(int caseInt)
    {
        if (CasePrices[caseInt] > balance)
        {
            noMoneyPanel.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
            currentCase = caseInt;
            balance -= CasePrices[caseInt];
        }
        
    }
    public void noMoneyBackBttn()
    {
        noMoneyPanel.SetActive(false);
    }

    public void sellBttn()
    {
        balance += currentPrice;
        SellBttn.interactable = false;
        dropPan.SetActive(false);
    }
    public void backBttn()
    {
        dropPan.SetActive(false);
        //DestroyChildren();
        RectTransform rectTransform = sp.GetComponent<RectTransform>(); // получаем RectTransform объекта sp
        InventoryClass.InventroryChecker();
    }
    
    public void caseAgain()
    {
        openCase = true;
        simulateCases();
        velocity = Random.Range(2, 3f);
        scrollSpeed = -20;
        dropPanel = false;
        randed = false;
        SellBttn.interactable = true;
        countCaseOpener++;
    }

    void simulateCases()
    {
        for (int a = 0; a < 50; a++)
        {
            int rand = Random.Range(0, 1000);
            int randWeapon = 0;
            if(rand <= 600)
            {
                randWeapon = 0;
                prefabsImages[randWeapon].sprite = ws[currentCase].blueI[Random.Range(0, ws[currentCase].blueI.Length)];
            }
            else if (rand > 600 && rand<=800)
            {
                randWeapon = 1;
                prefabsImages[randWeapon].sprite = ws[currentCase].purpleI[Random.Range(0, ws[currentCase].purpleI.Length)];
            }
            else if (rand > 800 && rand <= 930)
            {
                randWeapon = 2;
                prefabsImages[randWeapon].sprite = ws[currentCase].pinkI[Random.Range(0, ws[currentCase].pinkI.Length)];
            }
            else if (rand > 930 && rand <= 990)
            {
                randWeapon = 3;
                prefabsImages[randWeapon].sprite = ws[currentCase].redI[Random.Range(0, ws[currentCase].redI.Length)];
            }
            else if (rand > 990 && rand <= 1000)
            {
                randWeapon = 4;
                prefabsImages[randWeapon].sprite = ws[currentCase].goldI[Random.Range(0, ws[currentCase].goldI.Length)];
            }
            GameObject obj = Instantiate(prefabs[randWeapon],new Vector2(0,0),Quaternion.identity) as GameObject;
            obj.transform.SetParent(sp.transform);
            obj.transform.localScale = new Vector2(1, 1);
        }
    }
}
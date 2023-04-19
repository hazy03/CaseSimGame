using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] SlotsImages = new Image[30];
    public TextMeshProUGUI[] SlotsPrice = new TextMeshProUGUI[30];

    public Image finalDrop;
    public Text ItemPrice;

    public string price;
    
    public void InventroryChecker()
    {
        price = ItemPrice.text;
        for(int i = 0; i < SlotsImages.Length; i++)
        {
            if ((SlotsImages[i].sprite == null) && (SlotsPrice[i].text == ""))
            {
                SlotsImages[i].sprite = finalDrop.sprite;
                SlotsPrice[i].text = price;
                break;
            }
        }
    }


}

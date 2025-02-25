using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem
{
    public ShopItem(int i, Sprite s, int p, bool u)
    {
        id = i;
        sprite = s;
        price = p;
        unlocked = u;
    }
    public int id;
    public Sprite sprite;
    public int price;
    public bool unlocked;
}

[System.Serializable]
public class Stock
{
    public Stock(List<ShopItem> s)
    {
        items = s;
    }
    public List<ShopItem> items;
}

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject itemTemplate;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject unlocked;

    private float xOffset = 0.8f;

    private int price;
    private Sprite spAux;

    List<GameObject> itemsToBuy = new List<GameObject>();

    void Start()
    {

        InfoPlayer.LoadInfo();
        if(InfoPlayer.sr==null)
            InfoPlayer.sr = InfoPlayer.items[0].sprite;

        for(int i = 0; i< itemsToBuy.Count; i++)
        {
            Destroy(itemsToBuy[i]);
        }
        for (int i = 0; i < InfoPlayer.items.Count; i++)
        {
            GameObject iTemp = Instantiate(itemTemplate);
            iTemp.GetComponent<Button>().onClick.RemoveAllListeners();
            switch (i)
            {
                case 0:
                    iTemp.GetComponent<Button>().onClick.AddListener(buyItem0);
                    break;
                case 1:
                    iTemp.GetComponent<Button>().onClick.AddListener(buyItem1);
                    break;
                case 2:
                    iTemp.GetComponent<Button>().onClick.AddListener(buyItem2);
                    break;
            }

            Transform child = iTemp.transform.Find("Image");
            if (child != null)
                child.GetComponent<Image>().sprite = InfoPlayer.sprites[i];

            iTemp.transform.SetParent(canvas.transform);
            iTemp.transform.position = new Vector3(-0.8f + xOffset*i, 1.9f);
            if (InfoPlayer.items[i].unlocked)
            {
                GameObject unlo =  Instantiate(unlocked);
                unlo.transform.position = new Vector2(iTemp.transform.position.x,iTemp.transform.position.y-0.4f);
                unlo.transform.SetParent(iTemp.transform);
                Debug.Log(InfoPlayer.sr);
                Debug.Log(InfoPlayer.items[i].sprite);
                if (InfoPlayer.sr == InfoPlayer.items[i].sprite)
                {
                    unlo.GetComponentInChildren<TMP_Text>().text = "Selected";
                }
            }

            itemsToBuy.Add(iTemp);

        }
    }

    void Update()
    {
       text.gameObject.GetComponent<TMP_Text>().text = "Coins: "+ InfoPlayer.coins;
    }
    public void buyItem0()
    {
        int price = 20;
        if (!InfoPlayer.items[0].unlocked)
            if (InfoPlayer.coins >= price)
            {
                InfoPlayer.coins -= price;
                InfoPlayer.sr = InfoPlayer.sprites[0];
                InfoPlayer.items[0].unlocked = true;
                InfoPlayer.SaveInfo();

                Start();
            }
            else
            {
                Debug.Log("NONONONO");
            }
        else
            InfoPlayer.sr = InfoPlayer.sprites[0];
        Start();
    }
    public void buyItem1()
    {
        int price = 5;
        if (!InfoPlayer.items[1].unlocked)
            if (InfoPlayer.coins >= price)
            {
                InfoPlayer.coins -= price;
                InfoPlayer.items[1].unlocked = true;
                InfoPlayer.sr = InfoPlayer.sprites[1];
                InfoPlayer.SaveInfo();

                Start();
            }
            else
            {
                Debug.Log("NONONONO");
                //Este else no se puede quitar sin comprometer la estructura del codigo. CUIDADO
            }
        else
            InfoPlayer.sr = InfoPlayer.sprites[1];

        Start();
    }
    public void buyItem2()
    {
        int price = 5;
        if(!InfoPlayer.items[2].unlocked)
            if (InfoPlayer.coins >= price)
            {
                InfoPlayer.coins -= price;
                InfoPlayer.sr = InfoPlayer.sprites[2];
                InfoPlayer.items[2].unlocked = true;
                InfoPlayer.SaveInfo();
                Start();
            }
            else
            {
                Debug.Log("NONONONO");
                //Más de lo mismo.
            }
        else
            InfoPlayer.sr = InfoPlayer.sprites[2];

        Start();
    }
}

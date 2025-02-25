using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InfoPlayer : MonoBehaviour
{
    [System.Serializable]
    public class ColorList
    {
        public List<LevelColors> colors;
        public ColorList(List<LevelColors> c)
        {
            colors = c;
        }
    }
    static int blue = 0, green = 1, red = 2, yellow = 3, purple = 4, orange = 5, brown = 6, darkgreen = 7, darkblue=8, darkpurple = 9, beige = 10;
    static public List<LevelColors> colors = new List<LevelColors>();
    // Start is called before the first frame update

    static public int coins = 200000;

    static public List<ShopItem> items;

    static public Sprite sr;

    static public bool colorDiscovered = false;
    static private string saveFilePath;
    static private string saveFilePath2;
    static private string saveFilePath3;

    static public List<Sprite> sprites;
    public List<Sprite> spriteAux;
    void Start()
    {
        sprites = spriteAux;
        saveFilePath = Application.persistentDataPath + "/dataGame.json";
        saveFilePath2 = Application.persistentDataPath + "/shopData.json";
        LoadInfo();

        if (colors == null)
        {
            colors = new List<LevelColors>();
        }
        Debug.Log(colors.Count);
        if (colors.Count < 5)
        {
            colors.Clear();
            colors.Add(new LevelColors("blue", true, new Color(152 / 255f, 186 / 255f, 213 / 255f), new Color(198 / 255f, 211 / 255f, 227 / 255f), new Color(178 / 255f, 203 / 255f, 222 / 255f)));
            colors.Add(new LevelColors("green", false, new Color(182 / 255f, 218 / 255f, 159 / 255f), new Color(242 / 255f, 250 / 255f, 233 / 255f), new Color(231 / 255f, 244 / 255f, 206 / 255f)));
            colors.Add(new LevelColors("red", false, new Color(226 / 255f, 80 / 255f, 76 / 255f), new Color(255 / 255f, 191 / 255f, 176 / 255f), new Color(255 / 255f, 150 / 255f, 136 / 255f)));
            colors.Add(new LevelColors("yellow", false, new Color(181 / 255f, 167 / 255f, 74 / 255f), new Color(255 / 255f, 255 / 255f, 183 / 255f), new Color(255 / 255f, 241 / 255f, 146 / 255f)));
            colors.Add(new LevelColors("purple", false, new Color(183 / 255f, 152 / 255f, 236 / 255f), new Color(247 / 255f, 192 / 255f, 229 / 255f), new Color(212 / 255f, 161/ 255f, 243 / 255f)));
            colors.Add(new LevelColors("orange", false, new Color(255 / 255f, 180 / 255f, 72 / 255f), new Color(255 / 255f, 245 / 255f, 189 / 255f), new Color(255 / 255f, 220 / 255f, 151 / 255f)));
            colors.Add(new LevelColors("brown", false, new Color(135 / 255f, 92 / 255f, 54 / 255f), new Color(233 / 255f, 226 / 255f, 215 / 255f), new Color(225 / 255f, 184 / 255f, 148 / 255f)));
            colors.Add(new LevelColors("darkgreen", false, new Color(72 / 255f, 104 / 255f, 86 / 255f), new Color(192 / 255f, 223 / 255f, 211 / 255f), new Color(124 / 255f, 182 / 255f, 157 / 255f)));
            colors.Add(new LevelColors("darkblue", false, new Color(0 / 255f, 60 / 255f, 98 / 255f), new Color(119 / 255f, 158 / 255f, 203 / 255f), new Color(92 / 255f, 132 / 255f, 175 / 255f)));
            colors.Add(new LevelColors("darkpurple", false, new Color(60 / 255f, 7 / 255f, 83 / 255f), new Color(145 / 255f, 10 / 255f, 103 / 255f), new Color(114 / 255f, 4 / 255f, 85 / 255f)));
            colors.Add(new LevelColors("beige", false, new Color(160 / 255f, 147 / 255f, 125 / 255f), new Color(246 / 255f, 230 / 255f, 203 / 255f), new Color(231 / 255f, 212 / 255f, 181 / 255f)));


            SaveInfo();
        }
        Debug.Log(colors.Count);


        if (InfoPlayer.items == null || InfoPlayer.items.Count < 2)
        {
            InfoPlayer.items = new List<ShopItem>();
            InfoPlayer.items.Add(new ShopItem(0, sprites[0], 20, true));
            InfoPlayer.items.Add(new ShopItem(1, sprites[1], 50, false));
            InfoPlayer.items.Add(new ShopItem(2, sprites[2], 1000, false));
            SaveInfo();
        }
        if (sr==null)
            sr = InfoPlayer.items[0].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    static public void SaveInfo() 
    {
        ColorList c = new ColorList(colors);
        string json = JsonUtility.ToJson(c);
        File.WriteAllText(saveFilePath, json);

        Stock s = new Stock(items);
        string json2 = JsonUtility.ToJson(s);
        File.WriteAllText(saveFilePath2, json2);

        PlayerPrefs.SetInt("Coins", coins);
        
    }
    static public void LoadInfo()
    {
        if (File.Exists(saveFilePath))
        {
            string content = File.ReadAllText(saveFilePath);
            ColorList c = JsonUtility.FromJson<ColorList>(content);
            colors.Clear();
            colors = c.colors;
        }
        if (File.Exists(saveFilePath2))
        {
            string content2 = File.ReadAllText(saveFilePath2);
            Stock s = JsonUtility.FromJson<Stock>(content2);
            if(items!=null) items.Clear();
            items = s.items;
        }
        coins = PlayerPrefs.GetInt("Coins");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveInfo();
        }
    }
    private void OnApplicationQuit()
    {
        SaveInfo();
    }

    public void resetData()
    {
        colors.Clear();
        colors.Add(new LevelColors("blue", true, new Color(152 / 255f, 186 / 255f, 213 / 255f), new Color(198 / 255f, 211 / 255f, 227 / 255f), new Color(178 / 255f, 203 / 255f, 222 / 255f)));
        colors.Add(new LevelColors("green", false, new Color(182 / 255f, 218 / 255f, 159 / 255f), new Color(242 / 255f, 250 / 255f, 233 / 255f), new Color(231 / 255f, 244 / 255f, 206 / 255f)));
        colors.Add(new LevelColors("red", false, new Color(226 / 255f, 80 / 255f, 76 / 255f), new Color(255 / 255f, 191 / 255f, 176 / 255f), new Color(255 / 255f, 150 / 255f, 136 / 255f)));
        colors.Add(new LevelColors("yellow", false, new Color(181 / 255f, 167 / 255f, 74 / 255f), new Color(255 / 255f, 255 / 255f, 183 / 255f), new Color(255 / 255f, 241 / 255f, 146 / 255f)));
        colors.Add(new LevelColors("purple", false, new Color(183 / 255f, 152 / 255f, 236 / 255f), new Color(247 / 255f, 192 / 255f, 229 / 255f), new Color(212 / 255f, 161 / 255f, 243 / 255f)));
        colors.Add(new LevelColors("orange", false, new Color(255 / 255f, 180 / 255f, 72 / 255f), new Color(255 / 255f, 245 / 255f, 189 / 255f), new Color(255 / 255f, 220 / 255f, 151/ 255f)));
        colors.Add(new LevelColors("brown", false, new Color(135 / 255f, 92 / 255f, 54 / 255f), new Color(233 / 255f, 226 / 255f, 215 / 255f), new Color(225 / 255f, 184 / 255f, 148 / 255f)));
        colors.Add(new LevelColors("darkgreen", false, new Color(72 / 255f, 104 / 255f, 86 / 255f), new Color(192 / 255f, 223 / 255f, 211 / 255f), new Color(124 / 255f, 182 / 255f, 157 / 255f)));
        colors.Add(new LevelColors("darkblue", false, new Color(0 / 255f, 60 / 255f, 98 / 255f), new Color(119 / 255f, 158 / 255f, 203 / 255f), new Color(92 / 255f, 132 / 255f, 175 / 255f)));
        colors.Add(new LevelColors("darkpurple", false, new Color(60 / 255f, 7 / 255f, 83 / 255f), new Color(145 / 255f, 10 / 255f, 103 / 255f), new Color(114 / 255f, 4 / 255f, 85 / 255f)));
        colors.Add(new LevelColors("beige", false, new Color(160 / 255f, 147 / 255f, 125 / 255f), new Color(246 / 255f, 230 / 255f, 203 / 255f), new Color(231 / 255f, 212 / 255f, 181 / 255f)));



        sr = null;

        items.Clear();
        items = new List<ShopItem>();
        items.Add(new ShopItem(0, sprites[0], 20, true));
        items.Add(new ShopItem(1, sprites[1], 50, false));
        items.Add(new ShopItem(2, sprites[2], 1000, false));

        SaveInfo();
    }

    public static int getColor(int points)
    {
        int index=0;
        bool randomColor = false;
        switch (points)
        {
            case 10:
                if (!colors[green].discovered)
                {
                    randomColor = true;
                    index = green;
                    discoverColor(green);
                }
                break;
            case 20:
                if (!colors[purple].discovered)
                {
                    randomColor = true;
                    index = purple;
                    discoverColor(purple);
                }
                break;
            case 35:
                if (!colors[yellow].discovered)
                {
                    randomColor = true;
                    index = yellow;
                    discoverColor(yellow);
                }
                break;
            case 50:
                if (!colors[red].discovered)
                {
                    randomColor = true;
                    index = red;
                    discoverColor(red);
                }
                break;
            case 75:
                if (!colors[orange].discovered)
                {
                    randomColor = true;
                    index = orange;
                    discoverColor(orange);
                }
                break;
            case 100:
                if (!colors[darkpurple].discovered)
                {
                    randomColor = true;
                    index = darkpurple;
                    discoverColor(darkpurple);
                }
                break;
            case 150:
                if (!colors[beige].discovered)
                {
                    randomColor = true;
                    index = beige;
                    discoverColor(beige);
                }
                break;
            default:
                if (coins > 500 && !colors[brown].discovered)
                {
                        randomColor = true;
                        index = brown;
                        discoverColor(brown);
                }
                else if(coins > 100 && !colors[darkgreen].discovered)
                {
                    randomColor = true;
                    index = darkgreen;
                    discoverColor(darkgreen);
                }
                else if (!colors[darkblue].discovered)
                {
                    int rand = Random.Range(0, 100);
                    Debug.Log(rand);
                    if (rand == 1)
                    {
                        randomColor = true;
                        index = darkblue;
                        discoverColor(darkblue);
                    }
                }
                break;
        }

        while (!randomColor) {
            index = Random.Range(0, colors.Count);
            randomColor = colors[index].discovered;
        }
      

        return index;
    }


    static public void discoverColor(int color)
    {
        colors[color].discovered = true;
        colorDiscovered = true;
        SaveInfo();

    }
}

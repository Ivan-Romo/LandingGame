using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievments : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> colors;
    void Start()
    {
        InfoPlayer.LoadInfo();
        for (int i = 0; i < InfoPlayer.colors.Count; i++)
        {
            if(InfoPlayer.colors[i].discovered)
                switch (InfoPlayer.colors[i].color)
                {
                    case "blue":
                        colors[0].active = true;
                        break;
                    case "green":
                        colors[1].active = true;
                        break;
                    case "red":
                        colors[2].active = true;
                        break;
                    case "yellow":
                        colors[3].active = true;
                        break;
                    case "orange":
                        colors[4].active = true;
                        break;
                    case "purple":
                        colors[5].active = true;
                        break;
                    case "brown":
                        colors[6].active = true;
                        break;
                    case "darkgreen":
                        colors[7].active = true;
                        break;
                    case "darkblue":
                        colors[8].active = true;
                        break;
                    case "darkpurple":
                        colors[9].active = true;
                        break;
                    case "beige":
                        colors[10].active = true;
                        break;

                }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Points : MonoBehaviour
{

    [SerializeField] GameObject player;


    public int points;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<TMP_Text>().text = player.GetComponent<Player>().points.ToString();
    }
}

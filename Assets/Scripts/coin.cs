using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    public ParticleSystem explosion;
    public ParticleSystem explosion2;
    // Start is called before the first frame update
    void Start()
    {
        explosion2.transform.position = this.transform.position;
        explosion2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            InfoPlayer.coins++;
            PlayerPrefs.SetInt("Coins", InfoPlayer.coins);

            explosion.transform.position = this.transform.position;
            explosion.Play();

            Destroy(this.gameObject);            

        }
    }

}

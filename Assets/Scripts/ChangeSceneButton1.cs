using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void goMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void goDiscovered()
    {
        SceneManager.LoadScene("Collection");
    }

    public void goShop()
    {
        SceneManager.LoadScene("Shop");
    }
}

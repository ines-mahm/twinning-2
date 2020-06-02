using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WinnerFeedback : MonoBehaviour
{
    //Winner Textfeld
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject GameControllerScript = GameObject.Find("GameControllerScript");
        winText.text = "PLAYER " + GameControllerScript.GetComponent<GameController>().winner + " HAS WON!";
    }

    // Update is called once per frame
    public void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class About : MonoBehaviour
{
    public GameObject Panel_About;
    int counter;

    public void ShowHidePanel_About()
    {
        counter++;
        if (counter%2 == 1)
        {
            Panel_About.gameObject.SetActive(false);
        } else
        {
            Panel_About.gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

    public void HackItFlow ()
    {
        SceneManager.LoadScene("HackItFlow");
    }

    public void PacketFlow()
    {
        SceneManager.LoadScene("HackItFlow");
    }

    void Update()
    {
        if (Input.GetKeyDown("down") || Input.GetKeyDown("up"))
        {
            if (GameObject.Find("PacketFlowArrow").GetComponent<Image>().IsActive() == true)
            {
                GameObject.Find("PacketFlowArrow").GetComponent<Image>().enabled = false;
                GameObject.Find("HackItFlowArrow").GetComponent<Image>().enabled = true;
            }
            else
            {
                GameObject.Find("PacketFlowArrow").GetComponent<Image>().enabled = true;
                GameObject.Find("HackItFlowArrow").GetComponent<Image>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameObject.Find("PacketFlowArrow").GetComponent<Image>().IsActive() == true)
            {
                PacketFlow();
            }
            else
            {
                HackItFlow();
            }
        }
    }

    public void ArrowPosition()
    {
        
    }
}

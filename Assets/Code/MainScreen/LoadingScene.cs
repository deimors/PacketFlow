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
}

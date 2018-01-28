using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.MainScreen
{
    public class LoadingScene : MonoBehaviour {

        void Update()
        {
            var packetFlowImage = GameObject.Find("PacketFlowArrow").GetComponent<Image>();
            var hackItFlowImage = GameObject.Find("HackItFlowArrow").GetComponent<Image>();

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (packetFlowImage.IsActive())
                {
                    packetFlowImage.enabled = false;
                    hackItFlowImage.enabled = true;
                }
                else
                {
                    packetFlowImage.enabled = true;
                    hackItFlowImage.enabled = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (packetFlowImage.IsActive())
                {
                    PacketFlow();
                }
                else
                {
                    HackItFlow();
                }
            }
        }

        private static void HackItFlow()
        {
            SceneManager.LoadScene("HackItFlow");
        }

        private static void PacketFlow()
        {
            SceneManager.LoadScene("PacketFlow");
        }
    }
}

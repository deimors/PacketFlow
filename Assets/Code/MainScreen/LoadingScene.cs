using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
	private const string PACKET_FLOW_ARROW = "PacketFlowArrow";
	private const string HACK_IT_FLOW_ARROW = "HackItFlowArrow";
	public NetworkManager NetworkManagerInstance;

	// Use this for initialization
	void Start()
	{
		var manager = NetworkManagerInstance;
		manager.StartMatchMaker();
		manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			bool packetFlowArrowIsActive = IsPacketFlowArrowActive();
			GetImageForGameObject(PACKET_FLOW_ARROW).enabled = !packetFlowArrowIsActive;
			GetImageForGameObject(HACK_IT_FLOW_ARROW).enabled = packetFlowArrowIsActive;
        }

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
		{
			if (IsPacketFlowArrowActive())
			{
				// TODO: make sure no games have been started??
				SceneManager.LoadScene("PacketFlow");
			}
			else
			{
				var manager = NetworkManagerInstance;
				manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);

				var firstAvailableMatch = manager.matches?.FirstOrDefault();
				if (firstAvailableMatch != null)
				{
					SceneManager.LoadScene("HackItFlow");
				}
				else
				{
					// TODO: show some UI element saying "no games available to join"
					Debug.Log("No games available to join!");
				}
			}
		}
	}

	void OnDestroy()
	{
		NetworkManagerInstance.StopMatchMaker();
	}

	private static bool IsPacketFlowArrowActive() => GameObject.Find(PACKET_FLOW_ARROW).GetComponent<Image>().IsActive();
	private static Image GetImageForGameObject(string name) => GameObject.Find(name).GetComponent<Image>();
}

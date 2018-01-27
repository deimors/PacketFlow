using Assets.Code.HackItFlow.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestButtonBehaviour : MonoBehaviour
{
	private Root _graphSystemRoot;

	[Inject]
	public void Initialize(Root graphSystemRoot)
		=> _graphSystemRoot = graphSystemRoot;

	private void Start()
		=> GetComponent<Button>().onClick.AddListener(OnClick);

	private void OnClick()
		=> _graphSystemRoot.Nodes.Add(new FirewallNode());
}
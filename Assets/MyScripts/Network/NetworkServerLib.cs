﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class NetworkServerLib : MonoBehaviour {


	public NetworkView networkview;
	private string CurrentRoomName = "";
	private int CurrentPlayerCount = 3;
	private const string typeName = "ARMafia";
	private StartServerGui startServerGui;
	List<int> usersIds = new List<int>();
	public void Start()
	{
		
	}
	public void findNetworkGui(){
		startServerGui  = GameObject.Find("NetworkV").GetComponent<StartServerGui>();
	}
	public bool IsServer(){
		return Network.isServer;
	}
	public void ChangePlayerCount(int value)
	{
		CurrentPlayerCount = value+3;
		Debug.Log(value+3);
	}

	public void ChangeRoomName(string text)
	{
		CurrentRoomName = text;
	}

	public void HostServer()
	{
		Network.InitializeServer(CurrentPlayerCount, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, CurrentRoomName);
		Debug.Log("Server Started!");
	}

	void OnServerInitialized()
	{
		Debug.Log("Server Initializied");
	}

	void OnConnectedToServer()
	{
		networkview = GameObject.Find("ClientScripts").GetComponent<NetworkView>();
		networkview.RPC("Change",RPCMode.AllBuffered, "VuforiaPositionsTest");
	}
	void listIds(){
		string str = "";
		foreach(int elem in usersIds ){
			str += elem +" and";
		}
		Debug.Log ("Now are connected "+str);
	}
	[RPC]
	public void AddClient(int id)
	{
		usersIds.Add(id);
		Debug.Log ("User with id "+id+ " has joined");
		listIds ();
	}
}

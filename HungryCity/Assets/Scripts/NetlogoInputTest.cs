using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;

public class NetlogoInputTest : MonoBehaviour {
	
	private Dictionary<string, ServerLog> servers;

	public Transform wolvesT;
	public Transform sheepT;

	int lastIndex = 0;
	
	// Script initialization
	void Start() {	
		Application.runInBackground = true;
		OSCHandler.Instance.Init(); //init OSC
		
		
	}
	
	// NOTE: The received messages at each server are updated here
	// Hence, this update depends on your application architecture
	// How many frames per second or Update() calls per frame?
	void Update() {
		
		OSCHandler.Instance.UpdateLogs();
		servers = OSCHandler.Instance.Servers;
		
		foreach( KeyValuePair<string, ServerLog> item in servers )
		{
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if(item.Value.log.Count > 0) 
			{
				int lastPacketIndex = item.Value.packets.Count - 1;
				
				/*UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}", 
				                                    item.Key, // Server name
				                                    item.Value.packets[lastPacketIndex].Address, // OSC address
				                                    item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value*/
				Debug.Log(item.Value.packets[lastPacketIndex].Address + ", " +  item.Value.packets[lastPacketIndex].Data[0]);
				if (item.Value.packets[lastPacketIndex].Address == "/variables/wolves")
					wolvesT.localScale = Vector3.one * (float)item.Value.packets[lastPacketIndex].Data[0] * 0.2f;
				if (item.Value.packets[lastPacketIndex].Address == "/variables/sheep")
					sheepT.localScale = Vector3.one * (float)item.Value.packets[lastPacketIndex].Data[0] * 0.2f;
			}
		}

		foreach( KeyValuePair<string, ClientLog> item in OSCHandler.Instance.Clients )
		{
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if(item.Value.log.Count > lastIndex) 
			{
				List<object> data = item.Value.messages[item.Value.messages.Count-1].Data;

				foreach(object o in data)
				{
					Debug.Log(o);
				}
				if (item.Value.log.Count > lastIndex-1)
				{
					for (int i = lastIndex; i < item.Value.log.Count; i++) {
						Debug.Log(item.Value.log[i]);
						lastIndex = i;
					}
				}
			}
		}
	}
}
using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialConnection : MonoBehaviour {
	
	//public static SerialPort sp;
	public static SerialPort sp = new SerialPort("COM2", 9600);
	//public static SerialPort sp = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
	//public static SerialPort sp = new SerialPort("COM2", 9600);
	public string message2;
	//float timePassed = 0.0f;
	// Use this for initialization
	void Start () {
		OpenConnection ();
	}
	
	// Update is called once per frame
	void Update () {
		//timePassed+=Time.deltaTime;
		//if(timePassed>=0.2f){
		
		//print("BytesToRead" +sp.BytesToRead);
		//message2 = sp.ReadLine();
		//print(message2);
		//	timePassed = 0.0f;
		//}
	}
	
	public void OpenConnection() 
	{
		//sp = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
		Debug.Log ("OpenConnection started");
		if (sp != null) 
		{
			if (sp.IsOpen) 
			{
				sp.Close();
				Debug.Log ("Closing port, because it was already open!");
			}
			else 
			{
				sp.Open();  // opens the connection
				sp.ReadTimeout = 16;
				print("Port Opened!");
				//		message = "Port Opened!";;
			}
		}
		else 
		{
			if (sp.IsOpen)
			{
				print("Port is already open");
			}
			else 
			{
				print("Port == null");
			}
		}
		Debug.Log ("Open Connection finished running");
	}
	
	public static void OnApplicationQuit()
	{
		if (sp != null)
			sp.Close();
	}
	
	public static void sendStart() {
		sp.Write ("m");
	}

	public static void sendStop() {
		sp.Write ("c");
	}
}

//SerialConnection.sendMercury();

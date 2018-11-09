using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;

public class TZ_TGSDK : MonoBehaviour {
	#if UNITY_IOS
	public static string doubleID = "k7txsmtFVwrjHf9OAzp";
	public static string reviveID = "6dDceGiZP4ZWYWKYlUo";
	public static string turnTableID = "b0DJBRqDTIEygm0ALBv";
	public static string turnAgainID = "Z6u73UsKb8RaPvW9gWr";
	public static string offlineID = "Iok1UpcGiwLIvql26Tj";

	string appID = "Ecn061LEN1087o5i312v";

	#elif UNITY_ANDROID
	public static string doubleID = "";
	public static string reviveID = "";
	public static string turnTableID = "";
	public static string turnAgainID = "";
	string appID = "";

	#endif

	void Awake(){

		TGSDK.Initialize (appID);
		TGSDK.PreloadAd ();
		TGSDK.PreloadAdFailedCallback = (string obj) => {
			TGSDK.PreloadAd ();
		};
	}		

}

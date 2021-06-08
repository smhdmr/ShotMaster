using UnityEngine;
using System.Collections;

/// <summary>
/// Implement your game events in this script
/// </summary>
public class Events : MonoBehaviour
{
		public void OnWorld1Click (Object ob)
		{
				Application.LoadLevel ("World1");
		}

		public void OnWorld2Click (Object ob)
		{
				Application.LoadLevel ("World2");
		}

		public void OnWorld3Click (Object ob)
		{
				Application.LoadLevel ("World3");
		}

		public void OnBackClick (Object ob)
		{
				Application.LoadLevel ("WorldsMenu");
		}

		public void OnExitClick (Object ob)
		{
				Application.Quit ();
		}
}
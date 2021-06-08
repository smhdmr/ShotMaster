using UnityEngine;
using System.Collections;

public class General : MonoBehaviour
{
		public static General instance;//general instance

		// Use this for initialization
		void Awake ()
		{
				if (instance == null) {
						instance = this;
						DontDestroyOnLoad (gameObject);
				} else {
						Destroy (gameObject);
				}
		}

}

using UnityEngine;
using System.Collections;

/// <summary>
/// You can store SFX refernces in this script
/// </summary>
/// 
public class SFX : MonoBehaviour
{
		public static SFX instance;//static instance
		public AudioClip basketballTrim;
		public AudioClip boardHit;
		public AudioClip yeah;
		public AudioClip groundHit;
		public AudioClip metalHit;
		public AudioClip throwball;
		public AudioSource []audioSources;

		// Use this for initialization
		void Awake ()
		{
				if (instance == null) {
					audioSources = GetComponents<AudioSource>();
						instance = this;
				}
		}
}
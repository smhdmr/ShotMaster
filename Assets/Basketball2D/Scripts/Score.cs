using UnityEngine;
using System.Collections;

/// <summary>
/// Your Score.
/// </summary>
public class Score : MonoBehaviour
{
		private static TextMesh storeTextMesh;//text mesh component
		private static int score;//your score

		//Use this for Initialization
		void Start ()
		{
				score = 0;
				if (storeTextMesh == null) {
						storeTextMesh = GetComponent<TextMesh> ();
				}
				ApplyScore ();
		}

		//increase the score by a given value
		public static void IncreamentScore (int value)
		{
				score += value;
				ApplyScore ();
		}

		//apply the score to the text mesh component
		private static void ApplyScore ()
		{
				storeTextMesh.text = "" + score;
		}

		//reset Your Score
		public static void ResetScore ()
		{
				score = 0;
				ApplyScore ();
		}
}

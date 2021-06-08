using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles the animation events
/// </summary>
public class AnimationEvents : MonoBehaviour
{
		private void Destroy ()
		{
				GameObject.Destroy (gameObject);
		}

		private IEnumerator LoadFirstScene ()
		{
				yield return new WaitForSeconds(3);
				Application.LoadLevel ("WorldsMenu");
		}
}
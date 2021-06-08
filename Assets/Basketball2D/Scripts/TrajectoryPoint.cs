using UnityEngine;
using System.Collections;

/// <summary>
/// Trajectory Point.
/// </summary>
public class TrajectoryPoint : MonoBehaviour
{
		private SpriteRenderer spriteRendererComponent;//sprite renderer component of the point
		private Animator animatorComponent;//animator component of the point
		[HideInInspector]
		public bool collidedWithSomthing;//does the point collide with something

		void Awake ()
		{
				collidedWithSomthing = false;
		}

		void OnTriggerEnter2D (Collider2D col)
		{
				if (col.tag == "Ground" || col.tag == "BasketBoard") {//if the point collider with ground or basket board
						collidedWithSomthing = true;
				}
		}

		void OnTriggerExit2D (Collider2D col)
		{
				if (col.tag == "Ground" || col.tag == "BasketBoard") {//if the point left the ground or the basket board
						collidedWithSomthing = false;
				}
		}
}

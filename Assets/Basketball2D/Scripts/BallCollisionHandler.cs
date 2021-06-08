using UnityEngine;
using System.Collections;

/// <summary>
/// This script handles Collision of Basketball with other GameObjects
/// </summary>
/// 
public class BallCollisionHandler : MonoBehaviour
{
		public Rectangle rect1, rect2;//rect1 and rect2  are an areas to detect if the ball entered the net in the valid direction
		private bool enteredRect1;//does the ball entered rect1
		private bool enteredRect2;//does the ball entered rect2
		private Transform ballTransform;
		private Vector3 ballPosition;
		public AudioSource[] audioSource;//audio sources
		private bool isDestroying;//is ball is destroying
		public static bool isCoolShot;//do you got cool shot
		public GameObject PlusFiftyEffectPrefab;
		public GameObject PlusTenEffectPrefab;
		private Vector3 screenPiviotPoint;//the (0px,0px) position in the world space

		//used for initialization
		void Start ()
		{
				isCoolShot = true;//by default true
				audioSource = GetComponentsInParent<AudioSource> ();//get audio sources

				if (rect1 == null) {
						rect1 = GameObject.Find ("Rect1").GetComponent<Rectangle> ();//get rect1
				}

				if (rect2 == null) {
						rect2 = GameObject.Find ("Rect2").GetComponent<Rectangle> ();//get rect2
				}

				screenPiviotPoint = Camera.main.ScreenToWorldPoint (Vector3.zero);//get the (0px,0px) point in the screen
		}

		// update is called once per frame
		void Update ()
		{
				ballPosition = transform.position;//get ball position

				//if the ball entered rect1
				if (ballPosition.x > rect1.tempRect.x && ballPosition.x < rect1.tempRect.width && ballPosition.y < rect1.tempRect.y && ballPosition.y > rect1.tempRect.height && !enteredRect1) {
						enteredRect1 = true;
				}

				//if the ball entered rect2
				if (ballPosition.x > rect2.tempRect.x && ballPosition.x < rect2.tempRect.width && ballPosition.y < rect2.tempRect.y && ballPosition.y > rect2.tempRect.height && !enteredRect2) {
						enteredRect2 = true;
				}

				//if the ball entered rect1 and rect2 , then it entered the net
				if (enteredRect1 && enteredRect2) {

						if (isCoolShot) {//cool shot
								audioSource [2].clip = SFX.instance.yeah;
								audioSource [2].Play ();
								Instantiate (PlusFiftyEffectPrefab, transform.position, Quaternion.identity);//create plus Fifty effect
								Score.IncreamentScore (50);//increament score bye 50
						} else {
								Instantiate (PlusTenEffectPrefab, transform.position, Quaternion.identity);//create plus Ten effect
								Score.IncreamentScore (10);//increament score by 10
						}

						//play the net SFX
						audioSource [0].Play ();
						//reset flags
						enteredRect1 = false;
						enteredRect2 = false;
						isCoolShot = true;//assume true by default
				}


				if (ballPosition.y < screenPiviotPoint.y) {
						DestroyImmediate (gameObject);
				}
		}

		//handling Collision with other GameObjects
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.collider.tag == "Ground") {//if the ball hit the grounds

						if (!isDestroying) {//if the ball is not destroying
								isDestroying = true;//set isDestroying flag true
								GetComponent<Animator> ().SetBool ("isDestroy", true);//fade out the ball
						}

						if (GetComponent<Rigidbody2D>().velocity.magnitude > 3) {//if the rigidbody velocity is greater than 3
								audioSource [3].clip = SFX.instance.groundHit;//play ground hit SFX
								audioSource [3].Play ();
						}
				} else if (col.collider.tag == "BounceCollider" && GetComponent<Rigidbody2D>().velocity.magnitude >= 5) {//if the ball hit the Bounce Colliders with the given rigidbody velocity
						audioSource [1].clip = SFX.instance.basketballTrim;
						audioSource [1].Play ();//play basket bounce SFX
						isCoolShot = false;
				} else if (col.collider.tag == "BasketBoard" && GetComponent<Rigidbody2D>().velocity.magnitude >= 5) {//if the ball hit the Basket Board with the given rigidbody velocity
						audioSource [1].clip = SFX.instance.boardHit;
						audioSource [1].Play ();//play board hit SFX
						isCoolShot = false;
				} else if (col.collider.tag == "BasketPipe" && GetComponent<Rigidbody2D>().velocity.magnitude >= 4) {//if the ball hit the Basket Metal with the given rigidbody velocity
						audioSource [1].clip = SFX.instance.metalHit;//play metal hit SFX
						audioSource [1].Play ();
				}
		}
}
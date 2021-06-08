using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script handles ball controls
/// </summary>
///
public class BallControls : MonoBehaviour
{
		public GameObject trajectoryPointPrefab; //trajectory point prefab
		public GameObject ballPrefab;//ball prefab
		private GameObject ball;//current ball
		public Camera cam;
		private bool isPressed = false, isWaitingBall = false;
		private int numOfTrajectoryPoints = 30;//number of trajectory points
		private float torqueDirection;//torque direction
		private float randomX, randomY;//random position values
		private float ballCreateDelayTime = 0.5f;//ball creation delay time
		private float numOfTrajectoryPointsFraction = 2.5f;//number of trajectory points fraction
		private float forcePower = 10;//power value
		private const float forceConstant = 88;//force constant
		private float torquePower = 500;//torque power
		private float draggOffset = 0.04f;//dragg offset
		private Vector2 firstPos = Vector2.zero;//the first click position
		private Vector2 distanceOffset = new Vector2 (0, 2);//distance offset
		private const string ballName = "Ball";//ball name
		private List<GameObject> trajectoryPoints;//trajectory points
		public AudioSource audioSource;
		public Transform trajectoryPointsTransform;//trajectory points holder
		public Transform ballsContainerTransform;//balls holder
		public Transform[] TwoAreaPoint;//used to get random position
		public bool showFullPath = false;//show full path flag
	
		//used for initialization
		void Start ()
		{
				//setting up the references
				if (cam == null) {
						cam = Camera.main;
				}
		
				if (TwoAreaPoint == null) {
						TwoAreaPoint = new Transform[2];
				}
		
				if (TwoAreaPoint.Length == 0) {
						TwoAreaPoint = new Transform[2];
				}
		
				if (TwoAreaPoint [0] == null) {
						TwoAreaPoint [0] = GameObject.Find ("Point0").transform;
				}
		
				if (TwoAreaPoint [1] == null) {
						TwoAreaPoint [1] = GameObject.Find ("Point1").transform;
				}
		
				if (trajectoryPointsTransform == null) {
						trajectoryPointsTransform = GameObject.Find ("TrajectoryPoints").transform;
				}
		
				if (ballsContainerTransform == null) {
						ballsContainerTransform = GameObject.Find ("BallsContainer").transform;
			
				}
		
				if (audioSource == null) {
						audioSource = GetComponent<AudioSource> ();
				}

				audioSource.clip = SFX.instance.throwball;
				trajectoryPoints = new List<GameObject> ();

				CreateTrajectoryPoints ();//create the trajectory points

				if (ball == null) {//if there is no any ball
						CreateBall ();//create new ball
				}
		}
	
		//executes every frame
		void Update ()
		{
				if (isWaitingBall) {
						return;
				}
		
				if (Input.GetMouseButtonDown (0)) {
						isPressed = true;
						firstPos = cam.ScreenToWorldPoint (Input.mousePosition);//get the first click position
			
				} else if (Input.GetMouseButtonUp (0)) {
						if (isPressed) {
								isPressed = false;
								isWaitingBall = true;
								ThrowBall ();
								ResetTrajectoryPoints (true, true);//reset trajectory points
						}
				}
		
				if (isPressed) {//if you are keeping pressing
						Vector3 vel = GetForce (firstPos, cam.ScreenToWorldPoint (Input.mousePosition));
						if (Vector2.Distance (firstPos, cam.ScreenToWorldPoint (Input.mousePosition)) > draggOffset) {
								SetTrajectoryPoints (transform.position, vel / ball.GetComponent<Rigidbody2D>().mass);
						}
				}
		}

		//create trajectory points
		private void CreateTrajectoryPoints ()
		{
				if (trajectoryPoints == null) {
						return;
				}
				for (int i = 0; i < numOfTrajectoryPoints; i++) {
						GameObject point = (GameObject)Instantiate (trajectoryPointPrefab);//create new point
						point.name = "Point" + (i + 1);//setting point name
						point.transform.parent = trajectoryPointsTransform;//setting point parent
						point.GetComponent<SpriteRenderer> ().enabled = false;//disable sprite renderer component
						point.GetComponent<Animator> ().enabled = false;//disable animator component
						point.GetComponent<Collider2D>().enabled = false;//disable collider component
						point.transform.position = Vector3.zero;//setting the initial position
						trajectoryPoints.Insert (i, point);//add point to the list
				}
		}

		//reset trajectory points
		private void ResetTrajectoryPoints (bool resetCollidedWithGroundFlag, bool resetPosition)
		{
				for (int i = 0; i < trajectoryPoints.Count; i++) {
						if (resetCollidedWithGroundFlag) {
								trajectoryPoints [i].GetComponent<TrajectoryPoint> ().collidedWithSomthing = false;
						}
						if (resetPosition) {
								trajectoryPoints [i].transform.position = Vector3.zero;
						}
						trajectoryPoints [i].GetComponent<SpriteRenderer> ().enabled = false;
						trajectoryPoints [i].GetComponent<Animator> ().enabled = false;
				}
		}
	
		//create new ball
		private void CreateBall ()
		{
				Vector3 randPos = getRandomPosition ();
				transform.position = randPos;
				ball = Instantiate (ballPrefab, randPos, Quaternion.identity) as GameObject;
				ball.name = ballName;
				ball.transform.parent = ballsContainerTransform;
				ball.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	
		//throw the ball by adding force and torque.
		private void ThrowBall ()
		{
				ball.SetActive (true);	
				ball.GetComponent<Collider2D>().isTrigger = false;
				ball.GetComponent<Rigidbody2D>().isKinematic = false;
				torqueDirection = Mathf.Sign (-firstPos.x + cam.ScreenToWorldPoint (Input.mousePosition).x);
				ball.GetComponent<Rigidbody2D>().AddForce (GetForce (firstPos, cam.ScreenToWorldPoint (Input.mousePosition)) * forceConstant);
				ball.GetComponent<Rigidbody2D>().AddTorque (torqueDirection * torquePower);
				audioSource.Play ();//play throw sfx
				StartCoroutine ("WaitToCreate");
		}
	
		//get the force
		private Vector2 GetForce (Vector2 fromPos, Vector2 toPos)
		{
				fromPos += distanceOffset;
				Vector2 distance = fromPos - toPos;
				if (showFullPath) {
						numOfTrajectoryPoints = trajectoryPoints.Count;
				} else {
						numOfTrajectoryPoints = Mathf.CeilToInt (Mathf.Clamp (Mathf.Abs (distance.magnitude * numOfTrajectoryPointsFraction), 0, trajectoryPoints.Count));
				}

				return (distance) * forcePower;
		}
	
		//display the projectile trajectory path
		private void SetTrajectoryPoints (Vector3 pStartPosition, Vector3 pVelocity)
		{
				ResetTrajectoryPoints (false, false);//reset trajectory points
				float velocity = Mathf.Sqrt ((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
				float angle = Mathf.Rad2Deg * (Mathf.Atan2 (pVelocity.y, pVelocity.x));
				float fTime = 0.05f;//ftime initial value
				bool skipRenderingNext = false;//skip rendering points

				//show the trajectoy points
				for (int i = 0; i < numOfTrajectoryPoints; i++) {
						if (trajectoryPoints [i].GetComponent<TrajectoryPoint> ().collidedWithSomthing || !Rectangle.InsideScreen (trajectoryPoints [i].transform.position)) {//if a point collided with somthing like (ground,basket board) or it's outside of screen
								skipRenderingNext = true;//skip showing or rendering the next points
						}

						float dx = velocity * fTime * Mathf.Cos (angle * Mathf.Deg2Rad);
						float dy = velocity * fTime * Mathf.Sin (angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
						Vector3 pos = new Vector3 (pStartPosition.x + dx, pStartPosition.y + dy, 2);
						if (!skipRenderingNext) {
								trajectoryPoints [i].GetComponent<SpriteRenderer> ().enabled = true;
								trajectoryPoints [i].GetComponent<Animator> ().enabled = true;
						}
						trajectoryPoints [i].GetComponent<Collider2D>().enabled = true;
						trajectoryPoints [i].transform.position = pos;
						trajectoryPoints [i].transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (pVelocity.y - (Physics2D.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
						fTime += 0.06f;//increase the time by 0.06 offset of time
				}
		}
	
		//get a random position in the world space
		private Vector3 getRandomPosition ()
		{
				randomX = Random.Range (TwoAreaPoint [0].transform.position.x, TwoAreaPoint [1].transform.position.x);//random x value
				randomY = Random.Range (TwoAreaPoint [0].transform.position.y, TwoAreaPoint [1].transform.position.y);//random y value
				return new Vector2 (randomX, randomY);
		}
	
		//wait to create new ball
		private IEnumerator WaitToCreate ()
		{
				yield return new WaitForSeconds (ballCreateDelayTime);
				CreateBall ();
				isWaitingBall = false;
		}
}
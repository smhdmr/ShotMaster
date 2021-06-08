using UnityEngine;
using System.Collections;

/// <summary>
/// Rectangle is an area created by two points p0 and p1
/// Note : po must be on the right side,p1 must me on the left side
/// </summary>
/// 
public class Rectangle : MonoBehaviour
{
		public bool showAreaBounds = true;//flag to drag area boundary or not
		[HideInInspector]
		public Rect tempRect;//area bounds
		private Color color;//lines color
		private Vector3 pos0;//position of p0 point
		private Vector3 pos1;//position of p1 point
		public Transform p0, p1;

		// Use this for initialization
		void Start ()
		{
				if (p0 == null) {
						p0 = transform.Find ("P0").transform;
				}

				if (p1 == null) {
						p1 = transform.Find ("P1").transform;
				}

				color = Color.blue;
				pos0 = p0.position;
				pos1 = p1.position;
				tempRect = new Rect (pos0.x, pos0.y, pos0.x + (pos1.x - pos0.x), pos0.y + (pos1.y - pos0.y));
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (showAreaBounds) {
						pos0 = p0.position;
						pos1 = p1.position;
						tempRect = new Rect (pos0.x, pos0.y, pos0.x + (pos1.x - pos0.x), pos0.y + (pos1.y - pos0.y));
						DrawBounds ();
				}
		}

		//draw rectangle area
		private void DrawBounds ()
		{
				//draw horizontal lines
				Debug.DrawLine (new Vector2 (tempRect.x, tempRect.y), new Vector2 (tempRect.width, tempRect.y), color);
				Debug.DrawLine (new Vector2 (tempRect.x, tempRect.height), new Vector2 (tempRect.width, tempRect.height), color);

				//draw vertical lines
				Debug.DrawLine (new Vector2 (tempRect.x, tempRect.y), new Vector2 (tempRect.x, tempRect.height), color);
				Debug.DrawLine (new Vector2 (tempRect.width, tempRect.y), new Vector2 (tempRect.width, tempRect.height), color);
		}

		//check if a point inside of screen or not
		public static bool InsideScreen (Vector2 pos)
		{
				Vector2 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint (Vector2.zero);
				Vector2 topRightScreenPoint = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));

				if (pos.x > bottomLeftScreenPoint.x && pos.x < topRightScreenPoint.x) {//x-position inside of screen
						if (pos.y > bottomLeftScreenPoint.y && pos.y < topRightScreenPoint.y) {//y-position inside of screen
								return true;
						}
				}

				return false;
		}
}
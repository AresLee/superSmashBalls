using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public float cellWidth;
	public float cellHeight;

	// Use this for initialization
	void Start () {
	
	}
	public void Modify(int x, int y, float w, float h)
	{
		cellWidth = w;
		cellHeight = h;
		transform.Translate (new Vector3 (x*w, 0, y*h));
		transform.localScale.Scale(new Vector3(w, 1, h));
		transform.localScale = new Vector3(w, 1, h);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

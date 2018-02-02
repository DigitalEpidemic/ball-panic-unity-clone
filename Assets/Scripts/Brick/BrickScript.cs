using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]

public class BrickScript : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AnimationClip clip;

	public float x, y;

	void Start () {
		transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, y, 5));
	}

	void Update () {
//		transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, y, 5));
	}

	public IEnumerator BreakTheBrick () {
		animator.Play ("BrickBreak");
		yield return new WaitForSeconds (clip.length);
		gameObject.SetActive (false);
	}

} // BrickScript
﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public GameObject player;

	public bool bounds;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	void Awake(){

		Screen.SetResolution (1920, 1080, true);
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate()
	{
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, transform.position.z);

		if (bounds){
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x), 
				Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y), 
				Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
		}
	}
}
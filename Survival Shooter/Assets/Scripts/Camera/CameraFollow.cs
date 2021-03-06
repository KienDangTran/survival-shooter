﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	public Transform target; // The position that that camera will be following.
	public float smoothing = 5f; // The speed with which the camera will be following.
	private Vector3 offset; // The initial offset from the target.

	// Use this for initialization
	void Start ()
	{
		// Calculate the initial offset.
		this.offset = this.transform.position - this.target.position;
	}

	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = this.target.position + this.offset;

		// Smoothly interpolate between the camera's current position and it's target position.
		this.transform.position = Vector3.Lerp (this.transform.position, targetCamPos, this.smoothing * Time.deltaTime);
	}
}

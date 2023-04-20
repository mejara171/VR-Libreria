﻿//----------------------------------------------
//            BCG Shared Assets
//
// Copyright © 2014 - 2021 BoneCracker Games
// https://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCG_TPSController : MonoBehaviour {

    #region Controller

    public float speed = 10.0f;

    private float inputMovementX;
    private float inputMovementY;

    #endregion

    #region Camera
    public Camera characterCamera;

    // Calculate the current rotation angles for TPS mode.
    private Quaternion wantedRotation = Quaternion.identity;
    private Quaternion orbitRotation = Quaternion.identity;     // Orbit rotation.

    // Target position.
    private Vector3 targetPosition = Vector3.zero;

    // Orbit X and Y inputs.
    private float orbitX = 0f;
    private float orbitY = 0f;

    // Minimum and maximum Orbit X, Y degrees.
    public float minOrbitY = -20f;
    public float maxOrbitY = 80f;

    public float distance = 5f;
    public float height = 1.5f;

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    #endregion

    public BCG_Inputs inputs;

    void Start() {

    }

    void Update() {

        Inputs();
        Camera();

    }

    void FixedUpdate() {

        Controller();

    }

    private void Inputs() {

        inputs = BCG_InputManager.GetInputs();

        if (!BCG_EnterExitSettings.Instance.mobileController) {

            //	X and Y inputs based "Vertical" and "Horizontal" axes.
            inputMovementY = inputs.verticalInput * speed * .02f;
            inputMovementX = inputs.horizonalInput * speed * .02f;

            orbitX += inputs.aim.x;
            orbitY -= inputs.aim.y;

            // Clamping Y.
            orbitY = Mathf.Clamp(orbitY, minOrbitY, maxOrbitY);

            if (orbitX < -360f)
                orbitX += 360f;
            if (orbitX > 360f)
                orbitX -= 360f;

        } else {

            inputMovementY = BCG_MobileCharacterController.move.y * speed * Time.deltaTime;
            inputMovementX = BCG_MobileCharacterController.move.x * speed * Time.deltaTime;

            orbitX += BCG_MobileCharacterController.mouse.x * 100f * .02f;
            orbitY -= BCG_MobileCharacterController.mouse.y * 100f * .02f;

            // Clamping Y.
            orbitY = Mathf.Clamp(orbitY, minOrbitY, maxOrbitY);

            if (orbitX < -360f)
                orbitX += 360f;
            if (orbitX > 360f)
                orbitX -= 360f;

        }

    }

    private void Controller() {

        transform.Translate(inputMovementX, 0, inputMovementY, characterCamera.transform);
        transform.rotation = Quaternion.AngleAxis(orbitX, transform.up);

    }

    private void Camera() {

        orbitRotation = Quaternion.Lerp(orbitRotation, Quaternion.Euler(orbitY, 0f, 0f), 50f * Time.deltaTime);

        wantedRotation = transform.rotation;

        targetPosition = transform.position;
        targetPosition -= (wantedRotation * orbitRotation) * Vector3.forward * distance;
        targetPosition += Vector3.up * height;

        characterCamera.transform.position = targetPosition;
        characterCamera.transform.LookAt(transform);

    }

}
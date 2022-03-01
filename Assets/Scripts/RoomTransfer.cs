using RPG.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    [SerializeField] Vector2Variable cameraMaxPosition;
    [SerializeField] Vector2Variable cameraMinPosition;

    [SerializeField] Vector2 newCameraMax;
    [SerializeField] Vector2 newCameraMin;

    [SerializeField] FloatVariable cameraSmoothing;

    [SerializeField] Vector3 playerChange;

    [SerializeField] bool needText = true;
    [SerializeField] string placeName = "";
    [SerializeField] StringVariable placeNameVariable;
    [SerializeField] VoidEvent onScreenTransition;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

        MoveCamera(other);

        if (needText) {
            placeNameVariable.Value = placeName;
            onScreenTransition.Raise();
        }
    }

    void MoveCamera(Collider2D other) {
        float smoothing = cameraSmoothing.Value;
        cameraSmoothing.Value = 0.01f;
        //yield return new WaitForSeconds(.1f);
        cameraMinPosition.Value.x = newCameraMin.x;
        cameraMaxPosition.Value.x = newCameraMax.x;
        cameraMinPosition.Value.y = newCameraMin.y;
        cameraMaxPosition.Value.y = newCameraMax.x;

        other.transform.position += playerChange;
        //yield return new WaitForSeconds(.1f);
        cameraSmoothing.Value = smoothing;
    }
}

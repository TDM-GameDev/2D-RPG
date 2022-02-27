using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfer : MonoBehaviour
{
    [SerializeField] int newXMin;
    [SerializeField] int newXMax;
    [SerializeField] int newYMin;
    [SerializeField] int newYMax;
    [SerializeField] Vector3 playerChange;

    [SerializeField] TMPro.TMP_Text roomNameText;
    [SerializeField] bool needText = true;
    [SerializeField] string placeName = "";

    private CameraMovement cameraMovement;

    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        float smoothing = cameraMovement.smoothing;
        cameraMovement.smoothing = 0.01f;

        cameraMovement.minPosition.x = newXMin;
        cameraMovement.maxPosition.x = newXMax;
        cameraMovement.minPosition.y = newYMin;
        cameraMovement.maxPosition.y = newYMax;

        other.transform.position += playerChange;

        cameraMovement.smoothing = smoothing;

        if (needText)
        {
            StartCoroutine(ShowPlaceName());
        }
    }

    private IEnumerator ShowPlaceName()
    {
        roomNameText.text = placeName;
        roomNameText.enabled = true;
        yield return new WaitForSeconds(3f);
        roomNameText.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] FloatVariable smoothing;
    [SerializeField] Vector2Variable maxPosition;
    [SerializeField] Vector2Variable minPosition;

    private void Start() {
        // Do something fancy here later

        maxPosition.Value.x = 13;
        maxPosition.Value.y = 2;
        minPosition.Value.x = -6;
        minPosition.Value.y = -5;
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.Value.x, maxPosition.Value.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.Value.y, maxPosition.Value.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing.Value);
        }
    }
}

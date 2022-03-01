using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceNameUpdater : MonoBehaviour
{
    [SerializeField] StringVariable placeName;
    [SerializeField] float timeToDisplayPlaceName = 3f;
    TMPro.TMP_Text tmpText;

    void Start()
    {
        tmpText = GetComponent<TMPro.TMP_Text>();
    }

    public void Transition() {
        StopAllCoroutines();
        UpdatePlaceName();
        StartCoroutine(ShowPlaceName());
    }

    private void UpdatePlaceName()
    {
        if (tmpText.text == placeName.Value) return;

        tmpText.text = placeName.Value;
    }

    private IEnumerator ShowPlaceName() {
        tmpText.enabled = true;
        yield return new WaitForSeconds(timeToDisplayPlaceName);
        tmpText.enabled = false;
    }
}

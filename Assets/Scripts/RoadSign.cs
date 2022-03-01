using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour
{
    [SerializeField] RPG.Events.VoidEvent onSignApproach;
    [SerializeField] RPG.Events.VoidEvent onSignDepart;
    [SerializeField] TMPro.TextMeshProUGUI readSignText;
    [SerializeField] GameObject signPost;
    [SerializeField] TMPro.TextMeshProUGUI signPostText;
    [SerializeField] StringVariable signText;
    [SerializeField] string placeName = "";

    [SerializeField] bool inRange = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        ChangeSignText();
        onSignApproach.Raise();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

        onSignDepart.Raise();
    }

    private void ChangeSignText() {
        signPostText.text = signText.Value + System.Environment.NewLine + placeName;
    }

    private void Update() {
        if (inRange) {
            if (Input.GetKey(KeyCode.E)) {
                signPost.SetActive(true);
                readSignText.gameObject.SetActive(false);
            }
            else {
                signPost.SetActive(false);
                readSignText.gameObject.SetActive(true);
            }
        }
    }

    public void SetInRange(bool inRange) {
        this.inRange = inRange;
    }
}

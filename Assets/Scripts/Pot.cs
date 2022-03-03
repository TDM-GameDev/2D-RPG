using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private const string breakString = "break";
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }



    public void Break() {
        anim.SetTrigger(breakString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

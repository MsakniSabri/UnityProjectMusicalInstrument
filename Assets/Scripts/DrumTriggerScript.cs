using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumTriggerScript : MonoBehaviour
{

    private AudioSource source;
    MainScript main;

    void Start()
    {
        source = GetComponent<AudioSource>();
        main = FindObjectOfType<MainScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DrumStickHead")
        {
            main.playDrum(this.gameObject);
        }
    }
}

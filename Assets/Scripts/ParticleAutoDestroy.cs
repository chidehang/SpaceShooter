using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour {

    private ParticleSystem[] particals;

    // Use this for initialization
    void Start () {
        particals = GetComponentsInChildren<ParticleSystem>();
        GetComponent<AudioSource>().Play();

    }
	
	// Update is called once per frame
	void Update () {
        bool allStopped = true;
        foreach(ParticleSystem item in particals)
        {
            if(!item.isStopped)
            {
                allStopped = false;
                break;
            }
        }
        if (allStopped)
            Destroy(gameObject);
	}
}

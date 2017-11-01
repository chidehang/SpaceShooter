using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shopSpwan;
    public float delay;
    public float fireRate;

    private AudioSource audioFire;

	// Use this for initialization
	void Start () {
        audioFire = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire()
    {
        Instantiate(shot, shopSpwan.position, shopSpwan.rotation);
        audioFire.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed; //飞行速度
    public Boundary boundary;   //飞行边界
    public GameObject shot;     //子弹预制体
    public Transform shotSpawn; //子弹产生位置
    public float fireRate;  //子弹间隔时间

    private Rigidbody rigidbody;
    private float nextFiewTime;

    private AudioSource audioShot;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioShot = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButton("Fire1") && Time.time>nextFiewTime)
        {
            nextFiewTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioShot.Play();
        }
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rigidbody.velocity = movement * speed;

        float x = Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax);
        float z = Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax);
        rigidbody.position = new Vector3(x, 0, z);
    }
}

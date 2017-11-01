using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public Boundary boundary;
    public float tilt;      //倾斜
    public float dodge;     //躲闪
    public float smoothing;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float currentSpeed;
    private float targetMeneuver;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = ((Mover)GetComponent<Mover>()).speed;
        StartCoroutine(Evade());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while(true)
        {
            targetMeneuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetMeneuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        //要移动到的目标位置
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, targetMeneuver, smoothing * Time.deltaTime);
        rigidbody.velocity = new Vector3(newManeuver, 0, currentSpeed);
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0, rigidbody.position.z);
        rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);
    }
}

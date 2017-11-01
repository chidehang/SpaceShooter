using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    public GameController gameController;

    public int scoreValue;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        } else
        {
            Debug.Log("Cannot find 'GameController' Scripts");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag=="Enemy")
            return;

        //爆炸效果
        if (explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        //加分
        gameController.addScore(scoreValue);
        //碰撞销毁
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}

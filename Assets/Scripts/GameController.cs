using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject[] hazards; //小行星数组
    public GameObject enemyShip; //敌机
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameoverText;

    private int score = 0;
    private bool gameover = false;
    private bool restart = false;


	// Use this for initialization
	void Start () {
        restartText.enabled = false;
        gameoverText.enabled = false;

        updateScore();

        StartCoroutine(spawnWave());
        StartCoroutine(spawnEnemy());
	}
	
	// Update is called once per frame
	void Update () {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                //加载当前已加载的关卡
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}

    //产生小行星
    public IEnumerator spawnWave()
    {
        yield return new WaitForSeconds(startWait);

        while(true)
        {
            for(int i=0; i<hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPos = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Instantiate(hazard, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameover)
            {
                break;
            }
        }
    }

    //产生敌机
    public IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for(int i=0; i<3; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Instantiate(enemyShip, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameover)
            {
                break;
            }
        }
    }

    //增加积分
    public void addScore(int addValue)
    {
        score += addValue;
        updateScore();
    }

    //刷新积分UI
    public void updateScore()
    {
        scoreText.text = "Score " + score;
    }

    public void GameOver()
    {
        gameoverText.enabled = true;
        gameover = true;

        restartText.enabled = true;
        restart = true;

        score = 0;
        updateScore();
    }
}

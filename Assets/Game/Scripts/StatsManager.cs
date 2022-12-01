using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Stats;

    [Header("Velocity Stats")]

    [Range(0.0f, 100.0f)]
    public float turbo;
    public float maxTurbo = 100;

    [Range(0.0f, 100.0f)]
    public float health;
    public float maxHealth = 100;

    [Range(0.0f, 300.0f)]
    public float speed;
    public float maxSpeed = 300;

    [Header("Score Stats")]
    public float score;
    public float timeScore;
    public float scoreTimeValue = 2;

    [Header("References")]
    public GameManager Manager;
    public bool isPlaying;
    private void Awake()
    {
        if (Stats != null && Stats != this)
        {
            Destroy(this);
        }
        else
        {
            Stats = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        score = 0;
        health = 100;
        turbo = 0;
        timeScore = 0;
    }
    void Update()
    {
        AddTime();
        if (health <= 0)
        {
            isPlaying = false;
            Manager.GameOver();
            health = 100;
        }
    }
    void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
    void AddTime()
    {
        if (isPlaying == true)
        {
            timeScore += 1 * Time.deltaTime;
            AddScore(scoreTimeValue * Time.deltaTime);
        }
    }

}

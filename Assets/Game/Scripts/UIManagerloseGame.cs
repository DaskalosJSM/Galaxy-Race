using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManagerloseGame : MonoBehaviour
{

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;

    [Header("References")]
    public StatsManager Stats;

    // Start is called before the first frame update
    void Start()
    {
        Stats = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "points : " + Stats.score.ToString("0");
        timeText.text = "time : " + Stats.timeScore.ToString("0");
    }
}

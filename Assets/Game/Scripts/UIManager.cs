using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Bars")]
    [SerializeField] Image boostBar;
    [SerializeField] Image speedBar;
    [SerializeField] Image healtBar;

    [Header("UI Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] TextMeshProUGUI healthText;

    [Header("References")]
    public StatsManager Stats;

    // Start is called before the first frame update
    void Start()
    {
        boostBar = GameObject.Find("Boost Bar").GetComponent<Image>();
        speedBar = GameObject.Find("Speed Bar").GetComponent<Image>();
        healtBar = GameObject.Find("lifebar").GetComponent<Image>();
        Stats = GameObject.Find("StatsManager").GetComponent<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        speedBar.fillAmount = Stats.speed / Stats.maxSpeed;
        boostBar.fillAmount = Stats.turbo / Stats.maxTurbo;
        healtBar.fillAmount = Stats.health / Stats.maxHealth;
        scoreText.text = "points : " + Stats.score.ToString("0");
        timeText.text = "time : " + Stats.timeScore.ToString("0");
        velocityText.text = Stats.speed.ToString("0");
        healthText.text = Stats.health.ToString("0") + " %";
    }
}

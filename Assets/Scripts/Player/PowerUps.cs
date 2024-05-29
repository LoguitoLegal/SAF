using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PowerUps : MonoBehaviour
{
    public static PowerUps instance;
    [SerializeField] private GameObject player;
    // private bool canUpgrade = false;
    private int score;
    private int upgradeProgress = 0;

    [Header("ICONS")]
    [SerializeField] private Image imageShootSpeed;
    [SerializeField] private Image imageShootArea;
    [SerializeField] private Image imageShield;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        score = player.GetComponent<Player>().points;

        if (score >= 25 && upgradeProgress == 0)
        {
            player.GetComponent<PlayerShoot>().force += 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.2f);
            IncreaseLevel();
            imageShootSpeed.color = Color.green;
        }
        else if (score >= 40 && upgradeProgress == 1)
        {
            player.GetComponent<Player>().canUseShield = true;
            IncreaseLevel();
            imageShield.color = Color.green;
        }
        else if (score >= 60 && upgradeProgress == 2)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force -= 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.4f);
            IncreaseLevel();
            imageShootArea.color = Color.green;
        }
        //else if (score == 85 && upgradeProgress == 3)
        //{
        //    player.GetComponent<PlayerShoot>().shootLevel++;
        //    player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.7f);
        //    IncreaseLevel();

        //}
    }

    public void IncreaseLevel()
    {
        upgradeProgress++;
        SoundManager.Instance.TocarSFX(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PowerUps : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // private bool canUpgrade = false;
    private int score;
    private int upgradeProgress = 0;

    [Header("ICONS")]
    [SerializeField] private Image imageShootSpeed;
    [SerializeField] private Image imageShootArea;
    [SerializeField] private Image imageShield;
    [SerializeField] private Image imageFlare;
    [SerializeField] private Image imageTripleShoot;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        score = player.GetComponent<Player>().points;
        OnCooldown();

        //Tiro rapido
        if (score >= 40 && upgradeProgress == 0)
        {
            player.GetComponent<PlayerShoot>().force += 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.2f);
            IncreaseLevel();
            imageShootSpeed.color = Color.green;
        }

        //Escudo
        else if (score >= 80 && upgradeProgress == 1)
        {
            player.GetComponent<Player>().canUseShield = true;
            IncreaseLevel();
            imageShield.color = Color.green;
        }

        //Tiro duplo
        else if (score >= 120 && upgradeProgress == 2)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force -= 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.4f);
            IncreaseLevel();
            imageShootArea.color = Color.green;
        }

        //Flare
        else if (score >= 160 && upgradeProgress == 3)
        {
            player.GetComponent<Player>().canUseFlare = true;
            IncreaseLevel();
            imageFlare.color = Color.green;
        }

        //Tiro triplo
        else if (score >= 200 && upgradeProgress == 4)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force += 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.3f);
            IncreaseLevel();
            imageTripleShoot.color = Color.green;
        }
        //else if (score == 85 && upgradeProgress == 3)
        //{
        //    player.GetComponent<PlayerShoot>().shootLevel++;
        //    player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.7f);
        //    IncreaseLevel();

        //}
    }

    public void OnCooldown()
    {
        //Flare
        if (player.GetComponent<PlayerShoot>().flareCooldown.IsCoolingDown() && upgradeProgress >= 4)
        {
            player.GetComponent<Player>().canUseFlare = false;
            imageFlare.color = Color.yellow;
        }
        else if (!player.GetComponent<PlayerShoot>().flareCooldown.IsCoolingDown() && upgradeProgress >= 4) 
        {
            player.GetComponent<Player>().canUseFlare = true;
            imageFlare.color = Color.green;
        }
        //Escudo
        if (!player.GetComponent<Player>().canUseShield && upgradeProgress >= 2)
        {
            imageShield.color = Color.yellow;
        }
        else if (player.GetComponent<Player>().canUseShield && upgradeProgress >= 2)
        {
            imageShield.color = Color.green;
        }
    }
    public void IncreaseLevel()
    {
        upgradeProgress++;
        SoundManager.Instance.TocarSFX(3);
    }
}

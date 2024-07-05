using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
public class PowerUps : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public Sprite[] sprites;
    // private bool canUpgrade = false;
    private int score;
    private int upgradeProgress = 0;
    public bool shieldUnlocked = false;

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
            imageShootSpeed.sprite = sprites[1];
        }

        //Escudo
        else if (score >= 80 && upgradeProgress == 1)
        {
            player.GetComponent<Player>().canUseShield = true;
            IncreaseLevel();
            shieldUnlocked = true;
            imageShield.sprite = sprites[1];
        }

        //Tiro duplo
        else if (score >= 120 && upgradeProgress == 2)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force -= 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.4f);
            IncreaseLevel();
            imageShootArea.sprite = sprites[1];
        }

        //Flare
        else if (score >= 160 && upgradeProgress == 3)
        {
            player.GetComponent<Player>().canUseFlare = true;
            IncreaseLevel();
            imageFlare.sprite = sprites[1];
        }

        //Tiro triplo
        else if (score >= 200 && upgradeProgress == 4)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force += 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.3f);
            IncreaseLevel();
            imageTripleShoot.sprite = sprites[1];
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
            imageFlare.sprite = sprites[2];
        }
        else if (!player.GetComponent<PlayerShoot>().flareCooldown.IsCoolingDown() && upgradeProgress >= 4) 
        {
            player.GetComponent<Player>().canUseFlare = true;
            imageFlare.sprite = sprites[1];
        }
        //Escudo
        if (player.GetComponent<Player>().shieldCooldown.IsCoolingDown() && upgradeProgress >= 2 || player.GetComponent<Player>()._isShieldActive)
        {
            imageShield.sprite = sprites[2];
        }
        else if (!player.GetComponent<Player>().shieldCooldown.IsCoolingDown() && upgradeProgress >= 2)
        {
            imageShield.sprite = sprites[1];
        }
    }
    public void IncreaseLevel()
    {
        upgradeProgress++;
        SoundManager.Instance.TocarSFX(3);
    }
}

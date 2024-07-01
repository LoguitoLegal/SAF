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
    public bool shieldUnlocked = false;

    [Header("ICONS")]
    [SerializeField] private Image imageShootSpeed;
    [SerializeField] private Image imageShootArea;
    [SerializeField] private Image imageShield;
    [SerializeField] private Image imageFlare;
    [SerializeField] private Image imageTripleShoot;
    [SerializeField] private Sprite enabledSprite;
    [SerializeField] private Sprite inCooldownSprite;
    [SerializeField] private Sprite disabledSprite;
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
            imageShootSpeed.sprite = enabledSprite;
        }

        //Escudo
        else if (score >= 80 && upgradeProgress == 1)
        {
            player.GetComponent<Player>().canUseShield = true;
            IncreaseLevel();
            shieldUnlocked = true;
            imageShield.sprite = enabledSprite;
        }

        //Tiro duplo
        else if (score >= 120 && upgradeProgress == 2)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force -= 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.4f);
            IncreaseLevel();
            imageShootArea.sprite = enabledSprite;
        }

        //Flare
        else if (score >= 160 && upgradeProgress == 3)
        {
            player.GetComponent<Player>().canUseFlare = true;
            IncreaseLevel();
            imageFlare.sprite = enabledSprite;
        }

        //Tiro triplo
        else if (score >= 200 && upgradeProgress == 4)
        {
            player.GetComponent<PlayerShoot>().shootLevel++;
            player.GetComponent<PlayerShoot>().force += 20;
            player.GetComponent<PlayerShoot>().Cooldown.SetCoolDownTime(0.3f);
            IncreaseLevel();
            imageTripleShoot.sprite = enabledSprite;
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
            imageFlare.sprite = inCooldownSprite;
        }
        else if (!player.GetComponent<PlayerShoot>().flareCooldown.IsCoolingDown() && upgradeProgress >= 4) 
        {
            player.GetComponent<Player>().canUseFlare = true;
            imageFlare.sprite = enabledSprite;
        }
        //Escudo
        if (player.GetComponent<Player>().shieldCooldown.IsCoolingDown() && upgradeProgress >= 2 || player.GetComponent<Player>()._isShieldActive)
        {
            imageShield.sprite = inCooldownSprite;
        }
        else if (!player.GetComponent<Player>().shieldCooldown.IsCoolingDown() && upgradeProgress >= 2)
        {
            imageShield.sprite = enabledSprite;
        }
    }
    public void IncreaseLevel()
    {
        upgradeProgress++;
        SoundManager.Instance.TocarSFX(3);
    }
}

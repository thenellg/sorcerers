using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Range(0, 40)]
    public int health = 20;
    public int maxHealth = 10;
    public List<Card.cardType> weakness;

    public int baseAttackDamage;
    public int dangerAttackDamage;
    public playerStats player;

    public TextMeshProUGUI healthInfo;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerStats>();
    }

    public void attack()
    {
        int attackDamage = baseAttackDamage;

        if (health < maxHealth / 4)
            attackDamage = dangerAttackDamage;

        if (player.shield == 1)
            attackDamage = attackDamage / 2;
        else if (player.shield == 2)
            attackDamage = attackDamage / 3;
        else if (player.shield == 3)
            attackDamage = attackDamage / 4;
        else if (player.shield == 4)
            attackDamage = 0;

        if (player.shield > 0)
            player.shield = 0;

        if (player.health - attackDamage > 0)
            player.health -= attackDamage;
        else
            player.health = 0;

        if (player.health > 0)
            endTurn();
    }

    public void endTurn()
    {
        FindObjectOfType<gameManager>().nextTurn(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthInfo.text = health.ToString() + "/" + maxHealth.ToString();
        healthBar.fillAmount = (float)health / (float)maxHealth;
    }
    
}

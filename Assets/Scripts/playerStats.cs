using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 10)]
    public int health = 10;
    public int maxHealth = 10;
    [Range(0, 10)]
    public int shield = 0;

    [Range(0, 3)]
    public int turnsLeft = 3;
    public List<Sprite> turnSprites = new List<Sprite>();
    public Image turnsLeftImage;

    public TextMeshProUGUI healthInfo;
    public Image healthBar;
    public Image healthBarOutline;

    // Update is called once per frame
    void Update()
    {
        turnsLeftImage.sprite = turnSprites[turnsLeft];
        healthInfo.text = health.ToString() + "/" + maxHealth.ToString();
        healthBar.fillAmount = (float)health / (float)maxHealth;

        if(shield > 0)
        {
            healthBarOutline.color = Color.cyan;
            healthInfo.outlineColor = Color.cyan;
        }
        else
        {
            healthBarOutline.color = Color.black;
            healthInfo.outlineColor= Color.black;
        }
    }
}

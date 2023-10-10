using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardReader : MonoBehaviour
{
    public Card cardInfo;

    [Header("Card Info")]
    public TextMeshProUGUI spellType;
    public Image image;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI description;

    [Header("Card Stats")]
    public TextMeshProUGUI attack;
    public TextMeshProUGUI boost;
    public TextMeshProUGUI shield;

    public TextMeshProUGUI attackType;
    public TextMeshProUGUI boostType;
    public TextMeshProUGUI shieldType;

    [Header("Card Background")]
    public Image background;
    public Image iconBottom;

    [Header("In Game Info")]
    public playerStats player;
    public Enemy enemy;
    public Vector3 initialLocation;
    public Quaternion initialRotation;
    public float initialScale;
    public bool moving = false;
    public bool showing = false;

    public Vector3 goalLocation;
    public Quaternion goalRotation;
    public float goalScale;
    public int childLocation = 0;

    private void Start()
    {
        setCardInfo();
        player = FindObjectOfType<playerStats>();
        enemy = FindObjectOfType<Enemy>();
        goalLocation = initialLocation = transform.position;
        goalRotation = initialRotation = transform.rotation;
        goalScale = initialScale = transform.localScale.x;
        childLocation = transform.GetSiblingIndex();
    }

    public void resetCard()
    {
        setCardInfo();
        player = FindObjectOfType<playerStats>();
        enemy = FindObjectOfType<Enemy>();
        goalLocation = transform.position = initialLocation;
        goalRotation = transform.rotation = initialRotation;
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        goalScale = initialScale;
        childLocation = transform.GetSiblingIndex();
    }

    private void Update()
    {
        if (moving)
        {
            if (transform.position != goalLocation || transform.rotation != goalRotation || transform.localScale.x != goalScale)
            {
                transform.position = Vector3.MoveTowards(transform.position, goalLocation, 1f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, 0.1f);
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(goalScale, goalScale, goalScale), 0.2f);
            }
            else
            {
                moving = false;
            }
        }
    }

    public void showCard()
    {
        if (!showing && player.turnsLeft > 0) 
        {
            goalLocation = new Vector3(initialLocation.x, initialLocation.y + 125, initialLocation.z);
            goalRotation.eulerAngles = Vector3.zero;
            goalScale = initialScale + 0.1f;
            moving = true;
            showing = true;
            transform.SetAsLastSibling();
        }
    }

    public void hideCard()
    {
        if (showing)
        { 
            goalLocation = initialLocation;
            goalRotation = initialRotation;
            goalScale = initialScale;
            moving = true;
            showing = false;
            transform.SetSiblingIndex(childLocation);
        }
    }

    public void useCard()
    {
        hideCard();
        if(player.turnsLeft > 0)
        {
            int attackDamage = cardInfo.attack;

            gameObject.SetActive(false);
            player.turnsLeft--;

            if(enemy.weakness.Contains(cardInfo.m_cardType))
            {
                attackDamage = (int)(cardInfo.attack * 1.5f);
            }

            if (enemy.health - attackDamage > 0)
                enemy.health -= attackDamage;
            else
                enemy.health = 0;

            player.shield += cardInfo.shield;

            if (enemy.health > 0 && player.turnsLeft < 0)
                endTurn();
        }
    }

    public void endTurn()
    {
        player.turnsLeft = 0;
        FindObjectOfType<gameManager>().nextTurn(true);
    }

    public void setCardInfo()
    {
        spellType.text = cardInfo.spellType.ToUpper() + " SPELL";
        image.sprite = cardInfo.image;

        attack.text = cardInfo.attack.ToString();
        boost.text = cardInfo.boost.ToString();
        shield.text = cardInfo.shield.ToString();

        attackType.text = cardInfo.m_cardType.ToString().ToUpper();
        boostType.text = cardInfo.m_cardType.ToString().ToUpper();
        shieldType.text = cardInfo.m_cardType.ToString().ToUpper();

        name.text = cardInfo.name.ToUpper();
        description.text = cardInfo.description;

        background.color = cardInfo.backgroundColor;
        iconBottom.color = cardInfo.iconBottomColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public enum cardType { Charming, Energy, Flying, Gross, Quick, Strong, Wishful}
    public cardType m_cardType;
    [Multiline(2)]
    public new string name;
    [Multiline(2)]
    public string description;

    public string spellType;
    public Sprite image;

    public int attack;
    public int boost;
    public int shield;



    public Color backgroundColor;
    public Color iconBottomColor;
}

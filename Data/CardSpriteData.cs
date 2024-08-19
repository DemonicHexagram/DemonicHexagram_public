using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardSpriteData
{
    string _name;
    Sprite _cardFront;
    Sprite _cardCost;
    Sprite _cardPower;
    Sprite _cardShield;
    Sprite _cardFrame;
    Sprite _cardMask;
    Sprite _cardBackGround;
    public string Name { get { return _name; } set { _name = value; } } 
    public Sprite CardFront {  get { return _cardFront; } set {  _cardFront = value; } }
    public Sprite CardCost { get { return _cardCost; } set { _cardCost = value; } }
    public Sprite CardDamage { get { return _cardPower; } set { _cardPower = value; } }
    public Sprite CardShield { get {  return _cardShield; } set { _cardShield = value; } }
    public Sprite CardFrame {  get { return _cardFrame; } set { _cardFrame = value; } }
    public Sprite CardMask {  get { return _cardMask; } set { _cardMask = value; } }
    public Sprite CardBackGround { get { return _cardBackGround; } set { _cardBackGround = value; } }
}

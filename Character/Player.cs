using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player : BaseCharacter
{
    public PlayerDeck playerDeck;
    public Animator animator;
    public int Gold;
    private bool CanMove = true;


    public bool canMove { get { return CanMove; } set { CanMove = value; } }

    private void Start()
    {
        base.Start();
        GameManager.Instance.Player = this;
    }

    public void ChangeCanMove(bool value)
    {
        canMove = value;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
    }
    public void SubGold(int gold)
    {
        Gold -= gold;
    }
}

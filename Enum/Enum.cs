using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatusEffect
{
    Weak,
    Strength,
    Poison
}
public enum Elemental
{
    None,
    Fire,
    Water,
    Thunder,
}
public enum Action
{
    Attack,
    Shield,
    Effect,
    SelfEffect,
    Elemental,
    BossAttack,
    BossSp
}
public enum CardAlignment
{
    Left,
    Center,
    Right,
}

public enum CardButtonType
{
    Idle,     
    Upgrade,  
    Buy,      
    Delete,   
    Select,   
    UI        
}

public enum CardGrade
{
    Commmod,
    UnCommon,
    Rare
}
public enum NodeType
{
    MinorEnemy,
    EliteEnemy,
    RestSite,
    Store,
    Boss,
    Mystery
}


public enum NodeStates
{
    Locked,
    Attainable,
    Visited,
    Covered
}

public enum ClipList
{
    MainBGM,
    Battle4,
    Battle3,
    Battle2,
    Battle1
}

public enum SFX
{
    CardDraw,
    AttackCard,
    ShieldCard,
    BuffCard,
    EnemyAttack,
    EnemyDebuff,
    EnemyBuff,
    ButtonClick,
    Win,
    Lose
}

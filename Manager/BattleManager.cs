using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleManager : Singleton<BattleManager>
{
    public int _curcost;
    public int _maxcost = 4;
    public int _turn = 0;
    public GameObject TurnEndBtn;
    public PlayerDeck playerDeck;
    public BattleDeck battleDeck;
    public TurnPanel turnPanel;
    public PlayerDeckUI playerDeckUI;

    private MapManager mapManager;

    public List<Enemy> EnemyList = new List<Enemy>();

    public int CardDrawCount = 5;
    private int count;

    public void Start()
    {
        StartCoroutine(StartBattleCoroutine());
        TurnEndBtn.SetActive(false);
    }

    private IEnumerator StartBattleCoroutine()
    {
        GameManager.Instance.Player.shield = 0;
        playerDeck = GameManager.Instance.Player.playerDeck;
        GameManager.Instance.Player.currentElementalEffect.Clear();
        if (playerDeck != null && battleDeck != null)
        {
            playerDeckUI.Initialize();
            playerDeck.InitializeBattleDeck(battleDeck);
            BattleUIManager.Instance.UpdateCostUI();
        }
        turnPanel.playerTurnText.text = KeyWordManager.str_PlayerTurnTxt;

        yield return StartCoroutine(turnPanel.TurnChangeCoroutine());

        battleDeck.DrawCard(CardDrawCount);

        yield return StartCoroutine(WaitTurnEndBtn());
    }

    private IEnumerator WaitTurnEndBtn()
    {
        yield return new WaitForSeconds(1.5f);
        TurnEndBtn.gameObject.SetActive(true);
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            battleDeck.DrawCard(1);
        }
    }
    public void PlayerTurn()
    {
        if (GameManager.Instance.Player.hp > 0)
        {
            turnPanel.playerTurnText.text = KeyWordManager.str_PlayerTurnTxt;
            StartCoroutine(turnPanel.TurnChangeCoroutine());
            _curcost = _maxcost;
            GameManager.Instance.Player.shield = 0;
            BattleUIManager.Instance.UpdateCostUI();
            DrawCard();
            StartCoroutine(WaitTurnEndBtn());
            GameManager.Instance.Player.StartTurn();
            _turn++;
        }
        else
        {
            PlayerDie();
        }
    }
    public void PlayerDie()
    {

        SoundManager.Instance.PlaySfx(SFX.Lose);
        BattleUIManager.Instance.OnLosePanel();
        GameManager.Instance.Player.currentElementalEffect = new List<Elemental>();
        GameManager.Instance.Player.activeEffects = new Dictionary<StatusEffect, int>();
    }


    public void UseThisCard(int tpye,int damage, int shield, int draw, int mana, int[] effect, Elemental elemental, Enemy targetEnemy, int count)
    {
        switch (tpye)
        {
            case 0:
                SoundManager.Instance.PlaySfx(SFX.AttackCard);
                if (elemental == Elemental.None)
                {
                    
                    GameManager.Instance.Player.animator.SetTrigger(KeyWordManager.int_AttackTrigger);
                    GameManager.Instance.Player.animator.SetTrigger(KeyWordManager.int_AttackTrigger);
                    GameManager.Instance.Player.CheckDamage(damage, targetEnemy);
                    GameManager.Instance.Player.AddShield(shield);
                    GameManager.Instance.Player.ApplyEffect(effect, targetEnemy);

                    //if (effect[1] > 0)
                    //{
                    //}
                    //targetEnemy.ApplyEffect(effect, targetEnemy);
                    targetEnemy.UpdateNextActionUI();
                    if (targetEnemy.hp <= 0)
                    {
                        targetEnemy.hp = 0; EnemyDieRemove(targetEnemy);
                    }

                }
                else
                {
                    GameManager.Instance.Player.ApplyElementalEffect(elemental, targetEnemy);
                    GameManager.Instance.Player.CheckDamage(damage, targetEnemy);
                    GameManager.Instance.Player.AddShield(shield);
                    GameManager.Instance.Player.ApplyEffect(effect, targetEnemy);
                    targetEnemy.UpdateNextActionUI();
                    if (targetEnemy.hp <= 0)
                    {
                        targetEnemy.hp = 0; EnemyDieRemove(targetEnemy);
                    }

                }
                break;
            case 1:
                GameManager.Instance.Player.animator.SetTrigger(KeyWordManager.int_ShieldTrigger);
                GameManager.Instance.Player.AddShield(shield);
                GameManager.Instance.Player.ApplyEffect(effect, null);
            break;
            case 2:
                SoundManager.Instance.PlaySfx(SFX.AttackCard);
                //if (effect[1] > 0)
                //{
                //    GameManager.Instance.Player.ApplyEffect(effect, null);
                //}
                for (int i = EnemyList.Count - 1; i >= 0; i--)
                {
                    if (elemental == Elemental.None)
                    {
                        //if (effect[1] > 0)
                        //{

                        //}
                        //else
                        //{
                        //    GameManager.Instance.Player.ApplyEffect(effect, EnemyList[i]);
                        //}
                        GameManager.Instance.Player.CheckDamage(damage, EnemyList[i]);
                        GameManager.Instance.Player.AddShield(shield);
                        GameManager.Instance.Player.ApplyEffect(effect, EnemyList[i]);
                        EnemyList[i].UpdateNextActionUI();
                        if (EnemyList[i].hp <= 0) EnemyDieRemove(EnemyList[i]);


                    }
                    else
                    {
                        GameManager.Instance.Player.ApplyElementalEffect(elemental, EnemyList[i]);
                        GameManager.Instance.Player.CheckDamage(damage, EnemyList[i]);
                        GameManager.Instance.Player.AddShield(shield);
                        GameManager.Instance.Player.ApplyEffect(effect, EnemyList[i]);
                        EnemyList[i].UpdateNextActionUI();
                        if (EnemyList[i].hp <= 0) EnemyDieRemove(EnemyList[i]);

                    }
                }
                break;
            case 3:
                StartCoroutine(RandomAttackRoute(tpye, damage, shield, draw, mana, effect, elemental, count));
                break;

        }
        battleDeck.DrawCard(draw);
        _curcost = _curcost + mana;
        BattleUIManager.Instance.UpdateCostUI();

    }
    public void DrawCard()
    {
        battleDeck.DrawCard(CardDrawCount);
    }
    public void Enemyturn()
    {
        turnPanel.playerTurnText.text = KeyWordManager.str_EnemyTurnTxt;
        battleDeck.DiscardAllCardsInHand();
        TurnEndBtn.SetActive(false);
        StartCoroutine(EnemyTurnRoute());
    }
    IEnumerator EnemyTurnRoute()
    {
        yield return StartCoroutine(turnPanel.TurnChangeCoroutine());

        for (int i = EnemyList.Count - 1; i >= 0; i--)
        {
            count = EnemyList.Count;
            EnemyList[i].StartTurn();

            if (EnemyList.Count == count)
            {
                while (EnemyList[i].IsAnimating)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(KeyWordManager.flt_EnemyActionInterval);
            }

        }
        if(EnemyList.Count != 0)
        {
            PlayerTurn();

        }
    }

    public void EnemyDieRemove(Enemy target)
    {
        EnemyList.Remove(target);
        target.gameObject.SetActive(false);
        GameManager.Instance.DataManager.CountingData.EnemyAddCounting();
        if (EnemyList.Count == 0)
        {
            Victory();
        }
    }

    private void Victory()
    {
        BattleUIManager.Instance.OnWinPanel();
        SoundManager.Instance.PlaySfx(SFX.Win);
        GameManager.Instance.Player.currentElementalEffect = new List<Elemental>();
        GameManager.Instance.Player.activeEffects = new Dictionary<StatusEffect, int>();
    }

    IEnumerator RandomAttackRoute(int tpye, int damage, int shield, int draw, int mana, int[] effect, Elemental elemental, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int random = UnityEngine.Random.Range(0, EnemyList.Count - 1);
            if (EnemyList.Count != 0)
            {
                yield return StartCoroutine(RandomAttack(tpye, damage, shield, draw, mana, effect, elemental, random));
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    IEnumerator RandomAttack(int tpye, int damage, int shield, int draw, int mana, int[] effect, Elemental elemental, int random)
    {

        SoundManager.Instance.PlaySfx(SFX.AttackCard);
        if (elemental == Elemental.None)
        {
            //if (effect[1] > 0)
            //{
            //    GameManager.Instance.Player.ApplyEffect(effect, null);
            //}
            //EnemyList[random].ApplyEffect(effect, EnemyList[random]);

            GameManager.Instance.Player.CheckDamage(damage, EnemyList[random]);
            GameManager.Instance.Player.AddShield(shield);
            GameManager.Instance.Player.ApplyEffect(effect, EnemyList[random]);
            EnemyList[random].UpdateNextActionUI();
            if (EnemyList[random].hp <= 0) EnemyDieRemove(EnemyList[random]);
        }
        else
        {
            GameManager.Instance.Player.ApplyElementalEffect(elemental, EnemyList[random]);
            GameManager.Instance.Player.CheckDamage(damage, EnemyList[random]);
            GameManager.Instance.Player.AddShield(shield);
            GameManager.Instance.Player.ApplyEffect(effect, EnemyList[random]);
            EnemyList[random].UpdateNextActionUI();
            if (EnemyList[random].hp <= 0) EnemyDieRemove(EnemyList[random]);
        }
        yield return new WaitForSeconds(0.2f);

    }



}

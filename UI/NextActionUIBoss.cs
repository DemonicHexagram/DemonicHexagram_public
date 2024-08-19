using TMPro;
using UnityEngine;

public class NextActionUIBoss : NextActionUI
{
    public GameObject bossAttackIcon;
    public GameObject bossSpIcon;
    public TextMeshProUGUI bossSpDamage;
    protected private Hexaghost _boss;


    public override void UpdateNextAction()
    {
        if (_enemy == null) return;
        int spDamage = DamageCheack(2);
        Action nextAction = _enemy.GetNextAction();
        ResetNextActionUI();
        finalDamage = DamageCheack(_enemy.attack);
        switch (nextAction)
        {
            case Action.Attack:
                AttackIcon.SetActive(true);
                DamageText.text = finalDamage.ToString();
                break;
            case Action.Shield:
                ShieldIcon.SetActive(true);
                ShieldPowerText.text = _enemy.shieldPower.ToString();
                break;
            case Action.Effect:
                EffectIcon.SetActive(true);
                break;
            case Action.SelfEffect:
                SelfEffectIcon.SetActive(true);
                _enemy.attack = _enemy.attack + 3;
                break;
            case Action.Elemental:
                ElementalIcon.SetActive(true);
                ElementalIconDamageText.text = finalDamage.ToString();
                break;
            case Action.BossAttack:
                bossAttackIcon.SetActive(true);
                ElementalIconDamageText.text = finalDamage.ToString();
                break;
            case Action.BossSp:
                bossSpIcon.SetActive(true);
                bossSpDamage.text = $"{spDamage} X 8";
                break;
        }
    }

    public override void ResetNextActionUI()
    {
        AttackIcon.SetActive(false);
        ShieldIcon.SetActive(false);
        EffectIcon.SetActive(false);
        SelfEffectIcon.SetActive(false);
        ElementalIcon.SetActive(false);
        bossAttackIcon.SetActive(false);
        bossSpIcon.SetActive(false);
        DamageText.text = KeyWordManager.str_nullTxt;
        ElementalIconDamageText.text = KeyWordManager.str_nullTxt;

    }
}
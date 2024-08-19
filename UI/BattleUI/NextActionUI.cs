using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NextActionUI : MonoBehaviour
{
    [Header("Next Action UI")]
    public GameObject AttackIcon;
    public GameObject ShieldIcon;
    public GameObject EffectIcon;
    public GameObject SelfEffectIcon;
    public GameObject ElementalIcon;
    public TextMeshProUGUI DamageText; 
    public TextMeshProUGUI ElementalIconDamageText;
    public TextMeshProUGUI ShieldPowerText;

    public int finalDamage;

    protected private Enemy _enemy;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        UpdateNextAction();

    }



    public virtual void UpdateNextAction()
    {
        if (_enemy == null) return;

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
                break;
            case Action.Elemental:
                ElementalIcon.SetActive(true);
                ElementalIconDamageText.text = finalDamage.ToString();
                break;
        }
    }
    public virtual void ResetNextActionUI()
    {
        AttackIcon.SetActive(false);
        ShieldIcon.SetActive(false);
        EffectIcon.SetActive(false);
        SelfEffectIcon.SetActive(false);
        ElementalIcon.SetActive(false);

        DamageText.text = KeyWordManager.str_nullTxt;
        ElementalIconDamageText.text = KeyWordManager.str_nullTxt;
    }

    public int DamageCheack(int Damage)
    {
        finalDamage = Damage;
        if (_enemy.activeEffects.ContainsKey(StatusEffect.Strength) && _enemy.activeEffects[StatusEffect.Strength] > 0)
        {
            finalDamage *= 2;
        }

        if (_enemy.activeEffects.ContainsKey(StatusEffect.Weak) && _enemy.activeEffects[StatusEffect.Weak] > 0)
        {
            finalDamage /= 2;
        }
        return finalDamage;
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseHoverUI : MonoBehaviour
{
    public GameObject infoPanel; // UI 패널을 할당
    public TextMeshProUGUI infoText; // 정보가 표시될 텍스트
    private bool check = true;

    public BaseCharacter thisCharacter;
    public Enemy enemy;

    private Action nextAction;

    private void Start()
    {
        if (this.gameObject.layer == KeyWordManager.int_PlayerLayer)
        {
            thisCharacter = GameManager.Instance.Player;
        }
        infoPanel = GameObject.FindWithTag("InfoPanel");
        infoText = infoPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (check)
        {
            infoPanel.SetActive(false);
            check = false;
        }
        if (infoPanel.activeSelf)
        {
            Vector3 mousePos = Input.mousePosition;
            //infoPanel.transform.position = new Vector3(mousePos.x - infoPanel.GetComponent<RectTransform>().rect.width / 2, mousePos.y, mousePos.z);
            infoPanel.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.isCardDragged == false) 
        {
            infoText.text = "";
            infoPanel.SetActive(true);

            if (enemy != null)
            {
                nextAction = enemy.GetNextAction();
                infoText.text += "다음턴에 ";

                switch (nextAction)
                {
                    case Action.Attack:
                        infoText.text += $"{enemy.attack}만큼 공격합니다\n";
                        break;
                    case Action.Shield:
                        infoText.text += $"방어도를 {enemy.shieldPower}만큼 갖습니다\n";
                        break;
                    case Action.Effect:
                        infoText.text += $"플레이어에게 디버프를 부여합니다\n";
                        break;
                    case Action.SelfEffect:
                        infoText.text += $"자신에게 버프를 부여합니다\n";
                        break;
                    case Action.Elemental:
                        infoText.text += $"{enemy.attack}만큼 원소공격합니다\n";
                        break;
                }
            }

            foreach (KeyValuePair<StatusEffect, int> effects in thisCharacter.activeEffects)
            {
                if (effects.Key == StatusEffect.Poison)
                {
                    infoText.text += $"감전 {effects.Value}스택 : 적 턴 시작시 스택만큼 피해를 받음\n";
                }
                if (effects.Key == StatusEffect.Strength)
                {
                    infoText.text += $"힘 {effects.Value}스택 : 가하는 피해 2배\n";
                }
                if (effects.Key == StatusEffect.Weak)
                {
                    infoText.text += $"취약 {effects.Value}스택: 받는 피해 2배\n";
                }
            }
            foreach (Elemental element in thisCharacter.currentElementalEffect)
            {
                if (element == Elemental.Water)
                {
                    infoText.text += $"물 원소 부착\n";
                }
                if (element == Elemental.Thunder)
                {
                    infoText.text += $"번개 원소 부착\n";
                }
                if (element == Elemental.Fire)
                {
                    infoText.text += $"불 원소 부착\n";
                }
            }
            if (infoText.text == "") infoPanel.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        infoPanel.SetActive(false);
    }
}
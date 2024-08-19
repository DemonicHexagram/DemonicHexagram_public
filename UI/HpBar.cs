using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    public Slider HpSlider;
    public TextMeshProUGUI HpText;
    public RectTransform HpBarTransform;
    private BaseCharacter _character;

    public Image HpFillAreaImage;
    [Header("Effect")]
    public GameObject Weak;
    public GameObject Strength;
    public GameObject Poison;
    [Header("EffectStack")]
    public TextMeshProUGUI WeakStack;
    public TextMeshProUGUI StrengthStack;
    public TextMeshProUGUI PoisonStack;
    [Header("Elemental")]
    public GameObject Fire;
    public GameObject Water;
    public GameObject Thunder;
    [Header("Shield")]
    public GameObject ShieldUIObject;
    public TextMeshProUGUI ShieldText;

    [Header("PositionOffset")]
    public float height = 1.7f;
    public float width = 1.0f;

    void Start()
    {
        _character = GetComponent<BaseCharacter>();
        if (this.gameObject.CompareTag("Player"))
        {
            _character = GameManager.Instance.Player;
        }
        ShieldUIObject.SetActive(false);
    }


    void Update() 
    {
        UpdateHp();
        Updateshield();
        UpdateElemental();
        UpdateEffect();
    }

    void UpdateHp()
    {
        if (HpSlider != null) HpSlider.value = (float)_character.hp / (float)_character.fullhp;
        if (HpText != null) HpText.text = $"{_character.hp} / {_character.fullhp}";
        if (ShieldText != null) ShieldText.text = _character.shield.ToString();
    }

    void Updateshield()
    {
        if (_character.shield > 0)
        {
            ShieldUIObject.SetActive(true);
            ShieldText.text = $"{_character.shield}";
        }
        else
        {
            ShieldUIObject.SetActive(false); 
        }
    }

    void UpdateEffect()
    {
        UpdateEffectUI(StatusEffect.Weak, Weak, WeakStack);
        UpdateEffectUI(StatusEffect.Strength, Strength, StrengthStack);
        UpdateEffectUI(StatusEffect.Poison, Poison, PoisonStack);
    }

    void UpdateEffectUI(StatusEffect effect, GameObject effectImage, TextMeshProUGUI effectStackText)
    {
        if (_character.activeEffects.ContainsKey(effect) && _character.activeEffects[effect] > 0)
        {
            effectImage.gameObject.SetActive(true);
            effectStackText.text = _character.activeEffects[effect].ToString();
        }
        else
        {
            effectImage.gameObject.SetActive(false);
            effectStackText.text = KeyWordManager.str_nullTxt;
        }
    }

    void UpdateElemental()
    {
        if (_character.currentElementalEffect.Count == 0)
        {
            Fire.gameObject.SetActive(false);
            Water.gameObject.SetActive(false);
            Thunder.gameObject.SetActive(false);
        }
        else
        {
            UpdateElementalUI(Elemental.Fire, Fire);
            UpdateElementalUI(Elemental.Water, Water);
            UpdateElementalUI(Elemental.Thunder, Thunder);
        }

    }
    void UpdateElementalUI(Elemental element, GameObject elementImage)
    {
        if (elementImage == null)
        {
            Debug.LogError($"{element} 이미지가 할당되지 않았습니다.");
            return;
        }

        if (_character.currentElementalEffect.Contains(element))
        {
            elementImage.gameObject.SetActive(true);
        }
        else
        {
            elementImage.gameObject.SetActive(false);
        }
    }
}
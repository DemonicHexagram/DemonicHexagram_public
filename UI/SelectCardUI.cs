using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectCardUI : MonoBehaviour
{

    [SerializeField] private Transform _targetTransform;
    private List<Transform> _selectCardPanelChildObject;
    private List<Transform> _targetTransformChild;
    private List<Card> _cards;
    public List<Card> Cards { get { return _cards; } }
    private void Awake()
    {
        _selectCardPanelChildObject = new List<Transform>();
        _targetTransformChild = new List<Transform>();
        _cards = new List<Card>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            _selectCardPanelChildObject.Add(gameObject.transform.GetChild(i));
        }

        for (int i = 0; i < _targetTransform.childCount; i++)
        {
            _targetTransformChild.Add(_targetTransform.GetChild(i));
        }

        Card[] ChildCard = _targetTransform.GetComponentsInChildren<Card>();
        for (int i = 0; i < ChildCard.Length; i++)
        {
            _cards.Add(ChildCard[i]);
        }
    }

    private void OnEnable()
    {

        for (int i = 0; i < _selectCardPanelChildObject.Count; i++)
        {
            if (_selectCardPanelChildObject[i].gameObject.activeSelf == false)
            {
                _selectCardPanelChildObject[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < _targetTransformChild.Count; i++)
        {
            if (_targetTransformChild[i].gameObject.activeSelf.Equals(false))
            {
                _targetTransformChild[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].ResetCard();
            for (int j = i + 1; j < _cards.Count; j++)
            {
                if (_cards[i].CardNumber == _cards[j].CardNumber)
                {
                    int newRandomCard = Random.Range(0, GameManager.Instance.DataManager.CardList.Count);
                    _cards[j].CardNumber = newRandomCard;
                    i = 0;
                }
            }
        }
    }

    public void OnPlayerDeckAddCard()
    {
        SoundManager.Instance.PlaySfx(SFX.CardDraw);
        for (int i = 0; i < _selectCardPanelChildObject.Count - 1; i++)
        {
            _selectCardPanelChildObject[i].gameObject.SetActive(false);
        }
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        Card card = clickObject.GetComponent<Card>();

        for (int i = 0; i < _cards.Count; i++)
        {
            if (card.transform.position == _cards[i].transform.position)
            {
                _cards[i].ButtonType = CardButtonType.Select;
                StartCoroutine("SelectCardWait", i);
                continue;
            }

            _cards[i].gameObject.SetActive(false);
        }
    }
    public void OnEndButton()
    {
        gameObject.SetActive(false);

        if(UIManager.Instance is StageUIManager)
        {
            StageUIManager.Instance.OnIncidnetOff();
            GameManager.Instance.Player.ChangeCanMove(true);
        }
        else if(UIManager.Instance is BattleUIManager)
        {
            BattleUIManager.Instance.OnWinPanel();
        }
    }

    IEnumerator SelectCardWait(int number)
    {
        yield return new WaitForSeconds(1f);
        _cards[number].gameObject.SetActive(false);
        _cards[number].OnButtonTypeCard();
        _selectCardPanelChildObject[2].gameObject.SetActive(false);
        BattleUIManager.Instance.GoToWinPanelAfterCardSelecttion();
        GameManager.Instance.ObjectPool.DisableObjectsInPool(KeyWordManager.str_PoolTagIncidentScene);
        GameManager.Instance.Player.ChangeCanMove(true);
    }
}

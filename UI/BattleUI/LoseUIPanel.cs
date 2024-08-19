using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoseUIPanel : MonoBehaviour
{
    [SerializeField] private GameObject _countingMapGameObject;
    [SerializeField] private GameObject _countEnemyGameObject;
    [SerializeField] private GameObject _countingStoreGameObject;
    [SerializeField] private GameObject _countingRestGameObject;
    [SerializeField] private GameObject _countingIncidnetGameObject;
    [SerializeField] private GameObject _countingBossGameObject;

    private TextMeshProUGUI _countingMap;
    private TextMeshProUGUI _countEnemy;
    private TextMeshProUGUI _countingStore;
    private TextMeshProUGUI _countingRest;
    private TextMeshProUGUI _countingIncidnet;
    private TextMeshProUGUI _countingBoss;

    private void OnEnable()
    {
        TextRendering();
    }

    public void TextRendering()
    {
        if (GameManager.Instance.DataManager.CountingData.EnemyCounting > 0)
        {
            _countEnemyGameObject.SetActive(true);
            _countEnemy = _countEnemyGameObject.GetComponent<TextMeshProUGUI>();
            StringBuilder countEnemey = new StringBuilder("죽은 적 수 : ");
            countEnemey.Append(GameManager.Instance.DataManager.CountingData.EnemyCounting);
            _countEnemy.text = countEnemey.ToString();
        }

        if (GameManager.Instance.DataManager.CountingData.MapCounting > 0)
        {
            _countingMapGameObject.SetActive(true);
            _countingMap = _countingMapGameObject.GetComponent<TextMeshProUGUI>();
            StringBuilder countMap = new StringBuilder("지나온 맵의 수 :");
            countMap.Append(GameManager.Instance.DataManager.CountingData.MapCounting);
            _countingMap.text = countMap.ToString();
        }

        if (GameManager.Instance.DataManager.CountingData.StoreCounting > 0)
        {
            _countingStoreGameObject.SetActive(true);
            _countingStore = _countingStoreGameObject.GetComponent<TextMeshProUGUI>();
            StringBuilder countStore = new StringBuilder("지나온 가게의 수");
            countStore.Append(GameManager.Instance.DataManager.CountingData.StoreCounting);
            _countingStore.text = countStore.ToString();
        }

        if (GameManager.Instance.DataManager.CountingData.RestCounting > 0)
        {
            _countingRestGameObject.SetActive(true);
            _countingRest = _countingRestGameObject.GetComponent<TextMeshProUGUI>();
            StringBuilder countRest = new StringBuilder("지나온 휴식처의 수");
            countRest.Append(GameManager.Instance.DataManager.CountingData.RestCounting);
            _countingRest.text = countRest.ToString();
        }

        if (GameManager.Instance.DataManager.CountingData.MysteryCounting > 0)
        {
            _countingIncidnetGameObject.SetActive(true);
            _countingIncidnet = _countingIncidnetGameObject.GetComponent<TextMeshProUGUI>();
            StringBuilder countIncidnet = new StringBuilder("지나온 사건의 수");
            countIncidnet.Append(GameManager.Instance.DataManager.CountingData.MysteryCounting);
            _countingIncidnet.text = countIncidnet.ToString();
        }

        if (GameManager.Instance.DataManager.CountingData.BossCounting > 0)
        {
            _countingBossGameObject.SetActive(true);
            _countingBoss = _countingBossGameObject.GetComponent<TextMeshProUGUI>();    
            StringBuilder countBoss = new StringBuilder("지나온 보스방의 수");
            countBoss.Append(GameManager.Instance.DataManager.CountingData.BossCounting);
            _countingBoss.text = countBoss.ToString();
        }
    }
    private void OnDisable()
    {
        _countingMapGameObject.SetActive(false);
        _countEnemyGameObject.SetActive(false);
        _countingStoreGameObject.SetActive(false);
        _countingRestGameObject.SetActive(false);
        _countingIncidnetGameObject.SetActive(false);
        _countingBossGameObject.SetActive(false);
    }
}

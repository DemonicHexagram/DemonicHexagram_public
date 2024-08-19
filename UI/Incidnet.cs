using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Incidnet : MonoBehaviour
{
    public GameObject SelectCardPanel;

    public Text TitleTxt;
    public Text DescriptionTxt;
    public Text SelectTxt1;
    public Text SelectTxt2;

    private StringBuilder stringBuilder = new StringBuilder();

    private List<Dictionary<string, object>> data_list;
    private int IncidentIndex;

    private void OnEnable()
    {
        SelectCardPanel.SetActive(false);

        InitializeIncident(Random.Range(0,3));
        //InitializeIncident(2);
    }

    public void InitializeIncident(int _incidentIdx)
    {
        data_list = CSVReader.Read(KeyWordManager.str_IncidentListSheetTxt);
        IncidentIndex = _incidentIdx;

        TitleTxt.text = KeyWordManager.str_nullTxt;
        DescriptionTxt.text = KeyWordManager.str_nullTxt;
        SelectTxt1.text = KeyWordManager.str_nullTxt;
        SelectTxt2.text = KeyWordManager.str_nullTxt;

        TitleTxt.DOText(data_list[_incidentIdx]["title"].ToString(), KeyWordManager.flt_IncidentTitleTextSpd)
            .OnComplete(() =>
            {
                if (DescriptionTxt != null)
                {
                    DescriptionTxt.DOText(StringBuildProcess(data_list[_incidentIdx]["text"].ToString()), KeyWordManager.flt_IncidentDescriptionTextSpd)
                        .OnComplete(() =>
                        {
                            if (SelectTxt1 != null && SelectTxt2 != null)
                            {
                                SelectTxt1.DOText(StringBuildProcess(data_list[_incidentIdx]["section1"].ToString()), KeyWordManager.flt_IncidentSelectTextSpd);
                                SelectTxt2.DOText(StringBuildProcess(data_list[_incidentIdx]["section2"].ToString()), KeyWordManager.flt_IncidentSelectTextSpd);
                            }
                        });
                }
            });
    }
    private void OnDisable()
    {
        DOTween.Kill(TitleTxt);
        DOTween.Kill(DescriptionTxt);
        DOTween.Kill(SelectTxt1);
        DOTween.Kill(SelectTxt2);
    }
    private string StringBuildProcess(string Txtstring)
    {
        string[] fixedTextArr;
        stringBuilder.Clear();
        fixedTextArr = Txtstring.Split(".");
        foreach (string str in fixedTextArr)
        {
            stringBuilder.Append(str);
            stringBuilder.AppendLine();
        }
        return stringBuilder.ToString();
    }

    private int[] IntParsingProcess(string Txtstring)
    {
        string[] fixedTextArr;
        fixedTextArr = Txtstring.Split("/");
        int[] returnIntArr = new int[fixedTextArr.Length];
        int count = 0;
        foreach (string str in fixedTextArr)
        {
            returnIntArr[count] = int.Parse(fixedTextArr[count]);
            count++;
        }
        return returnIntArr;
    }

    public void OnSelectBtnClicked(int idx)//선택지를 클릭했을때 동작, 1번선택지인지 2번선택지인지가 매개변수 idx 임
    {
        //Reward구조 : playerGold, GetCard
        int[] rewardList = IntParsingProcess(data_list[IncidentIndex][$"reward{idx}"].ToString());
        if(GameManager.Instance.Player.Gold + rewardList[0] < 0)
        {
            return;
        }

        GameManager.Instance.Player.Gold += rewardList[0];//0번째는 골드
        Debug.Log($"플레이어의 현재 골드 : {GameManager.Instance.Player.Gold}");
        if (rewardList[1] > 0) 
        {
            SelectCardPanel.SetActive(true);
        }
        else
        {
            GameManager.Instance.ObjectPool.DisableObjectsInPool(KeyWordManager.str_PoolTagIncidentScene);
            GameManager.Instance.Player.ChangeCanMove(true);

        }

        GameManager.Instance.mapManager.SaveMap();
    }

    //public void GetCard(int idx) //카드 획득시 동작..카드 오브젝트로 옮김
    //{
    //    GameManager.Instance.Player.playerDeck.playerDeck.Add(idx);
    //}
}

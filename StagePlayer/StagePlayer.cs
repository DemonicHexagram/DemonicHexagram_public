using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagePlayer : MonoBehaviour
{
    public GameObject Description;
    public TextMeshProUGUI DescriptionTxt;

    private StageBlockController _stageBlockController;

    private void Start()
    {
        transform.position = GameManager.Instance.DataManager.PlayerTransformVector3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckNodeIn(other))
        {
            switch (other.GetComponent<StageBlockController>().BlockProperties.Node.nodeType)
            {
                case NodeType.RestSite:
                    StageUIManager.Instance.DescriptionTextChange("[휴식]\r\n\"F\"를 눌러 진입");
                    break;
                case NodeType.Store:
                    StageUIManager.Instance.DescriptionTextChange("[상점]\r\n\"F\"를 눌러 진입");
                    break;
                case NodeType.Mystery:
                    StageUIManager.Instance.DescriptionTextChange("[사건]\r\n\"F\"를 눌러 진입");
                    break;
                case NodeType.Boss:
                    StageUIManager.Instance.DescriptionTextChange("[보스]\r\n\"F\"를 눌러 진입");
                    break;
                case NodeType.MinorEnemy:
                    StageUIManager.Instance.DescriptionTextChange("[일반 적]\r\n\"F\"를 눌러 진입");
                    break;
                case NodeType.EliteEnemy:
                    StageUIManager.Instance.DescriptionTextChange("[정예 적]\r\n\"F\"를 눌러 진입");
                    break;

            }
            EnableDescriptionTxt();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("StageIcon"))
        {

            switch (other.GetComponent<StageBlockController>().BlockProperties.Node.nodeType)
            {
                case NodeType.RestSite:
                case NodeType.Store:
                case NodeType.Mystery:
                    if (!CheckNodeIn(other))
                    {
                        StageUIManager.Instance.OnDescriptionUIOff();
                    }
                    break;
            }
        }
    }


    private bool CheckNodeIn(Collider other)
    {
        if (other.gameObject.CompareTag("StageIcon"))
        {
            _stageBlockController = other.GetComponent<StageBlockController>();
            if (MapPlayerTracker.Instance.CheckNode(_stageBlockController.BlockProperties.Mapnode))
            {
                return true;
            }
        }
        return false;
    }

    private void EnableDescriptionTxt()
    {
        StageUIManager.Instance.OnDescriptionUIOn();

        GameManager.Instance.DataManager.PlayerTransformVector3 = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StageIcon"))
        {
            StageUIManager.Instance.OnDescriptionUIOff();

        }
    }
}

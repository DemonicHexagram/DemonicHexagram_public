using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBtnScript : MonoBehaviour
{
    public void OnLoadSceneBtn(int i)
    {
        GameManager.Instance.CreatedBlockList.Clear();
        SceneManager.LoadScene(i);
    }
}

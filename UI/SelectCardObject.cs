using UnityEngine;

public class SelectCardObject : MonoBehaviour
{
    [SerializeField] GameObject SeletcardGameObject;

    private void Start()
    { 
        SeletcardGameObject.SetActive(false);
    }

}

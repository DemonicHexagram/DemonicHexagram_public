using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapSizeController : MonoBehaviour
{
    public RectTransform map;
    public Button sizeBtn;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isZoomed = false;

    void Start()
    {
        sizeBtn.onClick.AddListener(ToggleMapSize);
        

        originalScale = map.localScale;
        originalPosition = map.anchoredPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMapSize();
        }
    }

    void ToggleMapSize()
    {
        if (isZoomed)
        {
            map.localScale = originalScale;
            map.anchoredPosition = originalPosition;
            isZoomed = false;
        }
        else
        {
            map.localScale = KeyWordManager.Vec3_ZoomedScale;
            map.anchoredPosition = KeyWordManager.Vec3_ZoomedPosition;
            isZoomed = true;
        }
    }
}

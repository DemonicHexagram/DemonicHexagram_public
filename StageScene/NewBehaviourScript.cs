using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    public GameObject miniMapCanvas;
    public GameObject mainMapCanvas;
    public Camera mainMapCamera;
    public Camera mainCamera;
    public RenderTexture mainMapRenderTexture;
    public RawImage miniMapRawImage;

    private void Start()
    {
        miniMapCanvas.SetActive(true);
        mainMapCanvas.SetActive(false);
        mainMapCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(true);

        miniMapRawImage.texture = mainMapRenderTexture;
    }

    public void OnMiniMapClick()
    {
        miniMapCanvas.SetActive(false);
        mainMapCanvas.SetActive(true);
        mainMapCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false); 
    }

    public void OnMainMapClose()
    {
        miniMapCanvas.SetActive(true);
        mainMapCanvas.SetActive(false);
        mainMapCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true); 
    }
}

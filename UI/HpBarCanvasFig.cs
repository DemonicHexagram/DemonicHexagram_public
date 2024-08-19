using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarCanvasFig : MonoBehaviour
{
    public Canvas canvas;
    private void Update()
    {
        canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PausaCanvas pausaCanvas;
    private bool isPause;
    private void OnEnable()
    {
        pausaCanvas = GetComponent<PausaCanvas>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        playerInputActions.UI.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if (!isPause)
        {
            pausaCanvas.Pausar();          
        }
        else
        {
            pausaCanvas.Reanudar();
        }
        isPause = !isPause;
    }
}

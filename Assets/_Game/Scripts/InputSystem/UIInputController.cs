using System.Collections;
using System.Collections.Generic;
using _Game._Canvas._Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PausaCanvas pausaCanvas;
    private void OnEnable()
    {
        pausaCanvas = GetComponent<PausaCanvas>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.UI.Enable();
        playerInputActions.UI.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        pausaCanvas.Pausar();
        Debug.Log("Pause");
    }
}

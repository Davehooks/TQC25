using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

public class Morse : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private float pressStart;
    private bool isPressing = false;

    private string currentMorse = "";
    public float inputThreshold = 0.3f;
    public float morseTimeout = 2f;
    private Coroutine timeout;

    private Dictionary<string, char> morseDictionary = new Dictionary<string, char>()
    {
        {".-", 'A'}, {"-...", 'B'}, {"-.-.", 'C'}, {"-..", 'D'},
        {".", 'E'}, {"..-.", 'F'}, {"--.", 'G'}, {"....", 'H'},
        {"..", 'I'},{".---", 'J'}, {"-.-", 'K'}, {".-..", 'L'},

        {"--", 'M'}, {"-.", 'N'}, {"---", 'O'}, {".--.", 'P'},
        {"--.-", 'Q'}, {".-.", 'R'}, {"...", 'S'}, {"-", 'T'},
        {"..-", 'U'}, {"...-", 'V'}, {".--", 'W'}, {"-..-", 'X'},
        {"-.--", 'Y'}, {"--..", 'Z'},
    };

    private PlayerController.ModeState GetModeFromChar(char letter)
    {
        return letter switch
        {
            'N' => PlayerController.ModeState.Normal,
            'S' => PlayerController.ModeState.Agility,
            'D' => PlayerController.ModeState.Defense,
            'A' => PlayerController.ModeState.Attack,
            _ => PlayerController.ModeState.Normal
        };
    }

    void Update()
    {

        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.iKey.wasPressedThisFrame)
        {
            if(currentMorse.Length == 3)
            {
                currentMorse = "";
            }
            pressStart = Time.time;
            isPressing = true;

            if (timeout != null)
            {
                StopCoroutine(timeout);
            }
            timeout = StartCoroutine(WaitForNoPress());
        }

        IEnumerator WaitForNoPress()
        {
            yield return new WaitForSeconds(morseTimeout);
            Debug.Log($"{morseTimeout} segundos sem apertar, limpando codigo");
            UIManager.UImanagerInstance.ZerarMorse();
            currentMorse = "";
        }
        if (keyboard.iKey.wasReleasedThisFrame && isPressing)
        {
            float pressDuration = Time.time - pressStart;

            if (pressDuration < inputThreshold)
            {
                currentMorse += ".";
                UIManager.UImanagerInstance.MorseHud('.');
                Debug.Log("Ponto");
            }
            else
            {
                currentMorse += "-";
                UIManager.UImanagerInstance.MorseHud('-');
                Debug.Log("Traço");
            }

            isPressing = false;
            Debug.Log("Código atual: " + currentMorse);

        }

        if (!isPressing)
        {

        }

        if (keyboard.oKey.wasPressedThisFrame)
        {
            if (morseDictionary.TryGetValue(currentMorse, out char letter))
            {
                if (playerController == null)
                {
                    playerController = FindFirstObjectByType<PlayerController>();
                    if (playerController == null)
                    {
                        return;
                    }
                }

                PlayerController.ModeState mode = GetModeFromChar(letter);
                playerController.SwitchMode(mode);
            }

            currentMorse = "";
            UIManager.UImanagerInstance.ZerarMorse();
        }
    }
}
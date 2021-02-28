using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public static event System.Action OnGameStarted;

    [SerializeField]
    Canvas gameSceneCountDownCanvas;
    [SerializeField]
    Canvas gameStartCanvas;
    [SerializeField]
    Canvas gameUICanvas;
    [SerializeField]
    Button tapToPlayButton;
    [SerializeField]
    TextMeshProUGUI countDownText;

    void Awake()
    {
        tapToPlayButton.onClick.AddListener(() =>
        {
            StartCoroutine(StartGameCoroutine());
            gameSceneCountDownCanvas.enabled = true;
            gameStartCanvas.enabled = false;
        });
    }

    IEnumerator StartGameCoroutine()
    {
        float countDown = 3;

        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            countDownText.SetText(countDown.ToString("f0"));
            yield return null;
        }

        countDown = Mathf.Clamp(countDown, 0, countDown);

        gameSceneCountDownCanvas.enabled = false;
        gameUICanvas.enabled = true;
        OnGameStarted?.Invoke();
    }
}

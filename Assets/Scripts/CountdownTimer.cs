using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    float startingTime = 20f;
    public static event System.Action OnCountDownFinished;

    public float StartingTime => startingTime;
    public float CurrentTime => currentTime;

    TextMeshProUGUI countdownText;
    bool gameFinished = false;
    float currentTime = 0f; // şimdiki zaman
    bool isPlaying;

    void Awake()
    {
        GameUIController.OnGameStarted += GameUIController_OnGameStarted;
    }

    private void GameUIController_OnGameStarted()
    {
        isPlaying = true;
    }

    void Start()
    {
        currentTime = startingTime;
        countdownText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        countdownText.SetText(((int)currentTime).ToString());

        if (isPlaying && currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                OnCountDownFinished?.Invoke();
            }
        }
    }
}

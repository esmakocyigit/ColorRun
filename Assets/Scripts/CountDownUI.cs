using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    [SerializeField]
    CountdownTimer countdownTimer;
    Image countDownImage;

    void Awake()
    {
        countDownImage = GetComponent<Image>();
    }

    void Update()
    {
        //Normalde sayım 5 saniye ama biz fillAmount'da 0-1 arasında çalışıyoruz.
        // 5 / 5 = 1
        // 1 / 5 = 0.2
        // Şuanki Zaman / Maksimum Zaman = 0 ile 1 Arasında Normalize edilmiş değer.

        countDownImage.fillAmount = countdownTimer.CurrentTime / countdownTimer.StartingTime;
    }

}

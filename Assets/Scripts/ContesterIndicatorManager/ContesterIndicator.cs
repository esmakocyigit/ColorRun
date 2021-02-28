using UnityEngine;
using TMPro;

public class ContesterIndicator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI percentage;
    [SerializeField]
    float speed;

    ContesterTypeHolder contesterType;

    const float MAX_SCALE_AMOUNT_Y = 4F;
    const float PERCENTAGE_MAX = 100F;

    [SerializeField]
    Transform dancer;

    float goalScaleValueY = 0;
    float percentageAmount = 0;

    public void SetContesterType(ContesterTypeHolder contesterType)
    {
        this.contesterType = contesterType;

        GetComponentInChildren<Renderer>().material.color = contesterType.ContesterType.ChangingColor;
    }

    void Update()
    {
        //C1,C2,C3,P

        // C2N = C2/ C1+C2+C3+P;
        // C3N = C3/ C1+C2+C3+P;
        // PN = P/ C1+C2+C3+P;

        //2,2,2,2
        //C1N = 2/2+2+2+2

        // C1N = C1/ C1+C2+C3+P * 100; YÜZDELİK HESAP
        //C1N = C1/ C1+C2+C3+P * 2; İSTENİLEN ORANDAKİ HESAP

        if (BoxContainer.Instance.CurrentGameContesterTypeCountDict[contesterType] != 0) // bir sayıyı asla sıfıra bölemezsin
        {
            //Verdiğimiz Scale Değerine göre oranlama yapıyoruz.
            goalScaleValueY = ((float)BoxContainer.Instance.CurrentGameContesterTypeCountDict[contesterType] / BoxContainer.Instance.GetSumOfPressedBoxes()) * MAX_SCALE_AMOUNT_Y;
            // 100'delik değeri hesapladığımız yer :/
            percentageAmount = ((float)BoxContainer.Instance.CurrentGameContesterTypeCountDict[contesterType] / BoxContainer.Instance.GetSumOfPressedBoxes()) * PERCENTAGE_MAX;
            percentage.SetText($"{percentageAmount.ToString("f1")}%");
        }

        //Mathf.Lerp(CurrentValue, GoalValue, FadeTime);

        transform.localScale = new Vector3(2, Mathf.Lerp(transform.localScale.y, goalScaleValueY, speed), 2);
        dancer.localPosition = new Vector3(0, Mathf.Lerp(transform.localScale.y, goalScaleValueY, speed), 0);
    }
}
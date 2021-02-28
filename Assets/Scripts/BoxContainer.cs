using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : MonoBehaviour
{
    public static BoxContainer Instance { get; private set; }

    public static event System.Action<ContesterTypeHolder> OnContesterAdded;

    List<BoxObject> boxObjects;

    public Dictionary<ContesterTypeHolder, int> CurrentGameContesterTypeCountDict => currentGameContesterTypeCountDict;

    //Sol Taraf : Anahtar(Key)
    //Sağ Taraf : Değer(Value)
    //Anahtar-Oda ilişkisi.

    Dictionary<ContesterTypeHolder, int> endGameContesterTypeCountDict;  //anahtar tipi, value tipi
    Dictionary<ContesterTypeHolder, int> currentGameContesterTypeCountDict;  //anahtar tipi, value tipi

    void Awake()
    {
        Instance = this;
        boxObjects = new List<BoxObject>();

        endGameContesterTypeCountDict = new Dictionary<ContesterTypeHolder, int>();
        currentGameContesterTypeCountDict = new Dictionary<ContesterTypeHolder, int>();

        //childcount: bir objenin altında kaç tane child olduğunu bulur

        for (int i = 0; i < transform.childCount; i++)
        {
            boxObjects.Add(transform.GetChild(i).GetComponent<BoxObject>());
        }

        CountdownTimer.OnCountDownFinished += OnGameEnd;
        BoxObject.OnContesterTypeHolder += BoxObject_OnContesterTypeHolder;
    }

    public void AddCurrentGameContesterDict(ContesterTypeHolder contester)
    {
        if (!currentGameContesterTypeCountDict.ContainsKey(contester))
            currentGameContesterTypeCountDict.Add(contester, 0);

        OnContesterAdded?.Invoke(contester);
    }

    private void BoxObject_OnContesterTypeHolder(ContesterTypeHolder current, ContesterTypeHolder previous)
    {
        currentGameContesterTypeCountDict[current]++;

        if (previous != null)
            currentGameContesterTypeCountDict[previous]--;
    }

    public void OnGameEnd()
    {
        InitializeCountDict();
        FindWinner();
    }

    void InitializeCountDict()
    {
        for (int i = 0; i < boxObjects.Count; i++)
        {
            //Contester: Player, Contester1, Contester2, Contester3

            ContesterTypeHolder currentContesterType = boxObjects[i].ContesterTypeHolder;

            //Şuan baktığımız BoxObjenin üzerine birisi basmış mı onu kontrol ediyor
            //Eğer basılmışsa if içerisine giriyoruz
            if (currentContesterType != null)
            {
                //Şuan baktığımız Contester Type sözlüğün(Dictionary) içerisinde var mı?
                //Yoksa: Ekleme yapıyor
                //Varsa if'e girmez devam eder.
                if (!endGameContesterTypeCountDict.ContainsKey(currentContesterType))
                    endGameContesterTypeCountDict.Add(currentContesterType, 0);

                //Önceden kaydettiğimiz Contester Type'ımıza 1 ekliyoruz.
                endGameContesterTypeCountDict[currentContesterType] += 1;
            }
        }
    }

    void FindWinner()
    {
        ContesterTypeHolder winner = null;
        int count = 0;

        foreach (var contesterType in endGameContesterTypeCountDict)
        {
            if (winner == null)
            {
                winner = contesterType.Key;
                count = contesterType.Value;
            }
            else
            {
                if (contesterType.Value >= count)
                {
                    winner = contesterType.Key;
                    count = contesterType.Value;
                }
            }
            Debug.Log($"{contesterType.Key} Has {contesterType.Value} Points");
        }

        Debug.Log($"{winner.ContesterType} Won With {count} Points");

        winner.ActivateCrown();
        PlayFinishAnimations(winner);
    }

    void PlayFinishAnimations(ContesterTypeHolder winner)
    {
        foreach (var contesterType in endGameContesterTypeCountDict)
        {
            if (contesterType.Key != winner)
                contesterType.Key.GetComponent<AnimancerController>().PlayLoserClip();
            else
                winner.GetComponent<AnimancerController>().PlayWinnerClip();

        }
    }

    public int GetSumOfPressedBoxes()
    {
        int sum = 0;

        foreach (var contester in currentGameContesterTypeCountDict)
        {
            sum += contester.Value;
        }

        return sum;
    }
}

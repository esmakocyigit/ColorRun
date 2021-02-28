using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContesterIndicatorManager : MonoBehaviour
{
    [SerializeField]
    GameObject indicatorContainer;

    ContesterIndicator contesterIndicator;
    float offsetAmount = -3.5f;
    float createPosition = 1.5f;

    void Awake()
    {
        BoxContainer.OnContesterAdded += BoxContainer_OnCurrentContesterAdded;
    }

    public void BoxContainer_OnCurrentContesterAdded(ContesterTypeHolder contester)
    {
        GameObject tmpContainer = Instantiate(indicatorContainer, transform); // küplerin oluştuğu yer


        ContesterIndicator tmpIndicator = tmpContainer.GetComponentInChildren<ContesterIndicator>();

        tmpContainer.transform.position = new Vector3(createPosition, 8, 2);
        tmpIndicator.SetContesterType(contester);
        createPosition += offsetAmount;
    }

}

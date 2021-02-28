using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContesterTypeHolder : MonoBehaviour
{
    [SerializeField]
    ContesterTypeSO contesterType;
    //[SerializeField]
    //Transform crown;

    public ContesterTypeSO ContesterType => contesterType;

    void Start()
    {
        BoxContainer.Instance.AddCurrentGameContesterDict(this);
    }

    public void ActivateCrown()
    {
        //crown.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ContesterTypeList", fileName = "ContesterTypeList")]
public class ContesterTypeListSO : ScriptableObject
{
    [SerializeField]
    List<ContesterTypeSO> list;

    public List<ContesterTypeSO> List => list;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxObject : MonoBehaviour
{
    public static event System.Action<ContesterTypeHolder, ContesterTypeHolder> OnContesterTypeHolder;

    ContesterTypeHolder m_ContesterTypeHolder;

    public ContesterTypeHolder ContesterTypeHolder => m_ContesterTypeHolder;

    public void SetContesterTypeHolder(ContesterTypeHolder contesterTypeHolder)
    {
        ContesterTypeHolder previousContesterTypeHolder = m_ContesterTypeHolder;

        m_ContesterTypeHolder = contesterTypeHolder;

        OnContesterTypeHolder?.Invoke(m_ContesterTypeHolder, previousContesterTypeHolder);
    }
}

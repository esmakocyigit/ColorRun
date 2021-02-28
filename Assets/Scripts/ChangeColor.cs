using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Renderer myRenderer;
    BoxObject boxObject;

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        boxObject = GetComponent<BoxObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        ContesterTypeHolder contesterTypeHolder = other.GetComponent<ContesterTypeHolder>();

        if (contesterTypeHolder != null)
        {
            myRenderer.material.DOColor(Color.white, 0);
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(transform.DOMove(new Vector3(transform.position.x, -0.991f, transform.position.z), 0.2f));
            mySequence.Append(
                DOTween.Sequence()
                    .Append(transform.DOMove(new Vector3(transform.position.x, -0.90f, transform.position.z), 0.2f))
            );

            boxObject.SetContesterTypeHolder(contesterTypeHolder);
            myRenderer.material.DOColor(contesterTypeHolder.ContesterType.ChangingColor, 0.8f);
        }
    }
}

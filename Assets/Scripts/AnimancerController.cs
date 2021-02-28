using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimancerController : MonoBehaviour
{
    [SerializeField]
    AnimationClip winner, loser;
    AnimancerComponent animancer;

    void Awake()
    {
        //Get Almak demek
        //Biz bir Gameobject'in üzerindeki bir Script'i veya Component'ı çekmek için GetComponent kullanıyoruz.

        animancer = GetComponent<AnimancerComponent>(); //bunu yapmasak ne olurdu ? oyun devam ederdi 
    }

    public void PlayWinnerClip()
    {
        animancer.Play(winner, 0.3f);
    }

    public void PlayLoserClip()
    {
        animancer.Play(loser, 0.3f);
    }
}

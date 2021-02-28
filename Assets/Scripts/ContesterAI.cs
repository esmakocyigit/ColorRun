using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContesterAI : MonoBehaviour
{
    [SerializeField]
    ContestersBoundaries contestersBoundaries;

    [SerializeField]
    private ParticleSystem particleObject;

    //ParticleSystem smokeEffect;
    ContesterTypeHolder contesterTypeHolder;

    NavMeshAgent navMeshAgent;

    BoxObject target;

    Animator animator;

    bool isSmokeParticleStarted = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        contesterTypeHolder = GetComponent<ContesterTypeHolder>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        GameUIController.OnGameStarted += GameUIController_OnGameStarted;
    }

    private void GameUIController_OnGameStarted()
    {
        enabled = true;
    }

    void Start()
    {
       // smokeEffect = GetComponentInChildren<ParticleSystem>();

        FindClosestObject();

        animator.SetBool("Running", true);
        //smokeEffect.Play();


        if (!isSmokeParticleStarted)
        {
            isSmokeParticleStarted = true;
            particleObject.Play();
        }
        else
        {
            animator.SetBool("Running", false);
            isSmokeParticleStarted = false;
            particleObject.Stop();
        }
        CountdownTimer.OnCountDownFinished += OnFinished;
    }

    void OnFinished()
    {
        navMeshAgent.speed = 0f;
        //smokeEffect.Stop();
        particleObject.Stop();
        animator.SetBool("Running", false);
        particleObject.Stop();
    }

    void Update()
    {
        //Eğer istediğimiz noktaya vardıysak yeni bir box buluyoruz.
        if (Vector3.Distance(transform.position, target.transform.position) <= 2f)
        {
            FindClosestObject();
        }
    }

    void FindClosestObject()
    {
        //Öncelikle çevremizde olan tüm Colliderları, belirli radius(yarıçap), içerisinde bir diziye çekiyoruz.
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);

        //İlk başta targetimiz olmadığı için collider içerisinden ilk olanı seçiyoruz ve atıyoruz.
        if (target == null)
            target = colliders[0].GetComponent<BoxObject>();

        //Collider dizimizden bir döngü başlatıyoruz.
        for (int i = 1; i < colliders.Length; i++)
        {
            //Aktif olan Box'u alıyoruz
            BoxObject currentBox = colliders[i].GetComponent<BoxObject>();

            //Eğer bulduğumuz collider BoxObject ve Contesterlarımız farklı ise buraya giriyor.
            if (currentBox != null && contesterTypeHolder != currentBox.ContesterTypeHolder)
            {
                //Eğer şuan ki aktif BoxObject'imizin mesafesi daha az ise buraya giriyor.
                if (Vector3.Distance(transform.position, currentBox.transform.position) <= Vector3.Distance(transform.position, target.transform.position))
                {
                    target = currentBox;
                }
                else if (Vector3.Distance(transform.position, target.transform.position) <= 2f)
                {
                    target = currentBox;
                }
                //Target'e ulaşmışsak ve aynı döngü içinde kalmak istemiyorsak. Pozisyonumuz ve target için bir sınır koyuyoruz.
            }
        }
        //navMeshAgent'ın Varış Noktasını(Destination) ayarlıyoruz.
        navMeshAgent.destination = target.transform.position;
    }

    void RandomPath()
    {
        navMeshAgent.destination = contestersBoundaries.GetRandomPosition();
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField] float moveSpeed;

    Animator animator;

    #endregion

    #region MonoBehaviour Callbacks 

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Running();

    }

    #endregion

    #region Other Methods
    
    void Running()
    {

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Running", true);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
       
        else
            animator.SetBool("Running", false);

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Running", true);
            transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Running", true);
            transform.Translate(transform.right * -moveSpeed * Time.deltaTime);
        }
    }

    #endregion
}
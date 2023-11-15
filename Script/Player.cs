using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 7;
    float turnSpeed = 300;
    float horizontalmovement;
    float verticalmovement;

    public Transform spawn;
    public Animator animator;
    public NPCDecision npc;
    public NPCBehavior npc1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (npc == null)
        {
            if (npc1.runFromPlayer && npc1 != null)
            {
                animator.SetTrigger("Run");
                animator.ResetTrigger("Walk");
            }
            else
            {
                animator.ResetTrigger("Run");
            }
        }
        
        if (npc1 == null)
        {
            if (npc.chaseCharacter && npc != null)
            {
                animator.SetTrigger("Run");
                animator.ResetTrigger("Walk");
            }
            else
            {
                animator.ResetTrigger("Run");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            gameObject.transform.position = spawn.position;
            gameObject.SetActive(true);
        }
    }

    public void Movement()
    {
        horizontalmovement = Input.GetAxis("Horizontal");
        verticalmovement = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalmovement);
        //transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalmovement);

        transform.Rotate(Vector3.up * horizontalmovement * turnSpeed * Time.deltaTime);

        if (verticalmovement == 0)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
        }
    }
}

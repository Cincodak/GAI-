using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public List<Transform> point;

    public bool walk;
    public bool runFromPlayer;

    public LayerMask layer;
    public Player player;
    public Animator animator;

    int index;
    float speed = 5;
    Transform path;

    // Start is called before the first frame update
    void Start()
    {
        path = point[index];
        walk = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool hitColi = Physics.CheckSphere(transform.position + Vector3.up * 0.5f, 10f, layer);

        if (hitColi)
        {
            walk = false;
            runFromPlayer = true;
        }
        else
        {
            walk = true;
            runFromPlayer = false;
        }

        ApplyPath(walk);
        RunFromPlayer(runFromPlayer);
    }

    public void ApplyPath(bool check)
    {
        if (check)
        {
            if (transform.position == path.position)
            {
                GetNumberIndex();
                path = point[index];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, path.position, speed * Time.deltaTime);
                transform.LookAt(path.position);
                animator.SetTrigger("Walk");
            }
        }
    }

    public void RunFromPlayer(bool check)
    {
        if (check)
        {
            Vector3 normDir = (player.transform.position - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, -(normDir * 7f), speed * Time.deltaTime);
            transform.LookAt(-(normDir * 40f));
        }
    }

    public void GetNumberIndex()
    {
        int randVal = UnityEngine.Random.Range(0, point.Count - 1);
        index = randVal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 7f);
        Gizmos.color = Color.black;
    }
}

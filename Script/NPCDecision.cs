using System.Collections.Generic;
using UnityEngine;

public class NPCDecision : MonoBehaviour
{
    public List<Transform> path;
    public List<Transform> point1;
    public List<Transform> point2;
    public List<Transform> point3;

    public Transform path1;
    public Transform path2;
    public Transform path3;

    public bool stayPath;
    public bool chaseCharacter;

    public LayerMask layer;

    int index;
    float speed = 5;
    public Transform point;
    public Player player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        path = point1;
        point = path[0];
        stayPath = true;
    }

    // Update is called once per frame
    void Update()
    {
        bool hitColi = Physics.CheckSphere(transform.position + Vector3.up * 0.5f, 10f, layer);

        if (hitColi)
        {
            stayPath = false;
            chaseCharacter = true;
        }
        else
        {
            chaseCharacter = false;
            CheckClosePath();
            stayPath = true;
        }

        ApplyPath(stayPath);
        ChasePlayer(chaseCharacter);
    }

    public void ApplyPath(bool check)
    {
        if (check)
        {
            float dist = Vector3.Distance(transform.position, point.position);
            if (dist < 1f)
            {
                index++;
                if (index == path.Count)
                {
                    index = 0;
                }
                point = path[index];
                animator.ResetTrigger("Walk");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
                transform.LookAt(point.position);
                animator.SetTrigger("Walk");
            }
        }
    }

    public void ChasePlayer(bool check)
    {
        if (check)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            animator.SetTrigger("Run");
        }
    }

    public void CheckClosePath()
    {
        float dist1 = Vector3.Distance(transform.position, path1.position);
        float dist2 = Vector3.Distance(transform.position, path2.position);
        float dist3 = Vector3.Distance(transform.position, path3.position);

        if (dist1 < dist2 && dist1 < dist3)
        {
            path = point1;
            point = path[index];
        }
        else if (dist2 < dist1 && dist2 < dist3)
        {
            path = point2;
            point = path[index];
        }
        else if (dist3 < dist1 && dist3 < dist2)
        {
            path = point3;
            point = path[index];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 10f);
        Gizmos.color = Color.black;
    }
}
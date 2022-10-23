using UnityEngine;
using System.Collections;

public class Camera_Follow : MonoBehaviour
{
    public GameObject followObject;
    public Vector3 followOffset;
    private Vector3 threshold;
    public float speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        /*followOffset.x = transform.position.x - followObject.transform.position.x;
        followOffset.y = transform.position.y - followObject.transform.position.y;
        threshold = transform.position;*/
        //not taking y as we won't update y position.
        threshold = CalculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference)>= threshold.x)
        {
            newPosition.x = follow.x;

        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;

        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed*Time.deltaTime);
    }
    void LateUpdate()
    {
       /* threshold.x= followObject.transform.position.x + followOffset.x;
        threshold.y= followObject.transform.position.y + followOffset.y;
        transform.position = threshold;*/
    }
    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

}

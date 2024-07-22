using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    public float speed = 10f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        transform.position = new Vector2(0, -2);
        rigidbody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-1f, 1f), -1f);
        rigidbody.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }
}

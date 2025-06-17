using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public int score = 10;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
         //   GameManager.Instance.OnEnemyKilled(this);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
          //  GameManager.Instance.OnBoundaryReached();
        }
    }
}

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool entered = false;
    private Animator _animator;
    [SerializeField] int CheckPointOrdem;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !entered)
        {
            _animator.SetTrigger("Entered");
            PlayerPrefs.SetInt("CheckPoint", CheckPointOrdem);
            entered = true;
        }
    }
}

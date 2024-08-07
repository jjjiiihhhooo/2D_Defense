using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float time;

    private void Update()
    {
        if (time > 0f) time -= Time.deltaTime;
        else Destroy(this.gameObject);
    }
}

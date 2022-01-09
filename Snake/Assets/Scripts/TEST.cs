using System.Collections;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public GameObject Body;

    private void Awake()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Body.transform.position = new Vector3(
            Body.transform.position.x,
            Body.transform.position.y - Body.GetComponent<SpriteRenderer>().bounds.extents.y * 2,
            Body.transform.position.z);
        yield return new WaitForSeconds(3f);
    }
}
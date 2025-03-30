using System.Collections;
using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class FloorSymbol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ModelPlayer model))
        {
            GetComponent<AudioSource>().Play();

            Inventory.Instance.MakeNewItem(SymbolManager.GetType(""));
            StartCoroutine(WaitAndDestroy());
            
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(GetComponent<SpriteRenderer>());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }
}

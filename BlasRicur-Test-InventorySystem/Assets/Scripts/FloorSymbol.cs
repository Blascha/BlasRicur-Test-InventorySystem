using UnityEngine;

[RequireComponent(typeof (BoxCollider2D))]
public class FloorSymbol : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ModelPlayer model))
        {
            Inventory.Instance.MakeNewItem(SymbolManager.GetType(""));
            Destroy(gameObject);
        }
    }
}

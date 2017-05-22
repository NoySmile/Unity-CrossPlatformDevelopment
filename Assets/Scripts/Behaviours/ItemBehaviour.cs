using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemBehaviour : MonoBehaviour
{
    public Item item_config;
    private Item item_runtime;
    
    public int Timer;
    private bool _initialized = false;

    private SpriteRenderer _spriteRenderer;

    public void Initialize(Item item)
    {
        item_config = item;
        Initialize();
    }

    public void Initialize()
    {
        if (_initialized)
        {
            Debug.LogWarning("Attempting to initialize an already initialized item behaviour");
            return;
        }

        _spriteRenderer = this.GetComponent<SpriteRenderer>();

        item_runtime = Instantiate(item_config);
        item_runtime.Initialize(null);
        item_runtime.ItemSprite = item_config.ItemSprite;
        _spriteRenderer.sprite = item_runtime.ItemSprite;
        _initialized = true;
        var col = GetComponent<BoxCollider2D>();
        col.size = _spriteRenderer.sprite.bounds.size;
        col.offset = _spriteRenderer.sprite.bounds.center;

    }

    public void AddToBackpack(GameObject go)
    {
        Debug.Log("add to pack");
        go.GetComponentInParent<BackPackBehaviour>().AddToPack(item_runtime);
    }

    public void DestroyItemGameObject()
    {
        Destroy(gameObject, Timer);
    }

    /// <summary>
    /// The scale function.
    /// Scales the current object based on its size.
    /// </summary>
    public void Scale()
    {
        if (_spriteRenderer.sprite.rect.size.x < 200)
        {
            transform.localScale = new Vector3(5, 5, 1);
        }
    }
}
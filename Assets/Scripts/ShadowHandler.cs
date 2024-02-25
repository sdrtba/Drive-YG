using UnityEngine;

public class ShadowHandler : MonoBehaviour
{
    [SerializeField] private Material material;
    private Vector3 offset = new Vector3(-0.1f, -0.1f, 0);
    private SpriteRenderer sprRndCaster;
    private SpriteRenderer sprRndShadow;
    private Transform transShadow;

    void Start()
    {
        transShadow = new GameObject().transform;
        transShadow.parent = transform;
        transShadow.localScale = Vector2.one;
        transShadow.localRotation = Quaternion.identity;

        sprRndCaster = GetComponent<SpriteRenderer>();
        sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();
        sprRndShadow.sprite = sprRndCaster.sprite;
        sprRndShadow.color = new Color(0, 0, 0, 0.5f);
        sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
        sprRndShadow.material = material;
    }

    void LateUpdate()
    {
        transShadow.position = transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pozo : MonoBehaviour
{
    Interaction sign;
    [SerializeField] GameObject chicken;
    Animator chicAnim;
    Animator pozAnim;
    bool bucketIsUp;
    [SerializeField] bool isChicGift;
    bool chicGifted;
    Collider2D chicColl;
    Collider2D pozColl;
    Rigidbody2D chicRb;
    SpriteRenderer chicSprite;

    void Awake()
    {
        sign = transform.Find("Interaction Sign").GetComponent<Interaction>();
        pozAnim = this.GetComponent<Animator>();
        pozColl = this.GetComponent<Collider2D>();
    }

    private void Start()
    {
        chicGifted = false;
        bucketIsUp = false;
    }

    void Update()
    {
        if (sign.wantInteract && !bucketIsUp)
        {
            pozAnim.SetBool("B_Up", true);
            if (isChicGift && !chicGifted)
            {
                chicken = Instantiate(chicken, this.transform.position - new Vector3(0.2f,0.25f, 0), Quaternion.identity);
                chicAnim = chicken.GetComponent<Animator>();
                chicRb = chicken.GetComponent<Rigidbody2D>();
                chicColl = chicken.GetComponent<Collider2D>();
                chicSprite = chicken.GetComponent<SpriteRenderer>();
                StartCoroutine(chickGiftProcess());
                chicGifted = true;
            }
            bucketIsUp = true;
        }
        else if (sign.wantInteract)
        {
            pozAnim.SetBool("B_Up", false);
            bucketIsUp = false;
        }
    }

    IEnumerator chickGiftProcess()
    {
        chicSprite.sortingLayerName = "Anykind";
        chicAnim.SetTrigger("gift");
        yield return new WaitForSeconds(0.3f);
        chicRb.velocity = Vector2.up * 1;
        yield return new WaitForSeconds(0.3f);
        chicRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(chicColl, pozColl, true);
        chicColl.isTrigger = false;
        chicRb.AddForce(new Vector2(3.5f, 5), ForceMode2D.Impulse);
        chicRb.gravityScale = 1;
        yield return new WaitForSeconds(1f);
        chicColl.isTrigger = true;
        chicSprite.sortingLayerName = "Entities";
        chicRb.velocity = Vector2.zero;
        chicRb.gravityScale = 0;
    }
}

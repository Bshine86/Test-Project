using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Bullet : MonoBehaviour{

    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private CircleCollider2D circleCollider;
    
    private void Awake() 
    {
         circleCollider = GetComponent<CircleCollider2D>(); 
    }
    void Update()
    {
        if (hit) return; //if bullet hit something then return and do not execute rest of code
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0); // move object on X axis by movement speed

    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        hit = true;
        circleCollider.enabled = false;
        /*if (hit = true && GetComponent<CircleCollider2D>().enabled != true)
        {
            gameObject.SetActive(false);
        }*/
    }


    
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        circleCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)// Check If the sign of local scale x is not = to direction. Means bullet is not facing the right direction.
            localScaleX = -localScaleX; // if localscale x does not = direction flip it

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    
}    
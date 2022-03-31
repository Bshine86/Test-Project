using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    
    [SerializeField]Transform shootPoint;
    [SerializeField]public Transform target;   
    [SerializeField]float shootRange;
    [SerializeField] private GameObject [] bullets;
    public float fireRate;
    private float _lastShot= 0;

    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {  
    
      if (CanSeeEnemy(shootRange))
      {
          //pewpew

      }
      else
      {
          //no pewpew
      }
    } 

    public bool CanSeeEnemy (float distance) 
    {
        bool seeEnemy = false;
        var castDist = distance;

        //Vector2 endPos = target.position;
        Vector2 endPos = shootPoint.position + Vector3.right * distance;
        RaycastHit2D rayInfo = Physics2D.Linecast(shootPoint.position, endPos, 1 << LayerMask.NameToLayer("Enemy"));

        if(rayInfo.collider != null)
        {
            if (rayInfo.collider.gameObject.CompareTag("Enemy"))
            {
                seeEnemy = true;
                //Debug.Log ("You can see the enemy.");
            }
            else
            {
                seeEnemy = false;
                //Debug.Log ("You can not see the enemy.");
            }

            Debug.DrawLine(shootPoint.position, endPos, Color.red);
        }

        if (seeEnemy == true)
        {
            if(Time.time > _lastShot + fireRate) 
            {
                _lastShot = Time.time;
                Shoot();
            }
        }

        return seeEnemy;        
    }

    void Shoot ()
    {
        bullets[FindBullets()].transform.position = shootPoint.position;
        bullets[FindBullets()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
        Debug.Log ("I'm shooting");
    }

    private int FindBullets ()
    {
        for (int i = 0; i < bullets.Length; i++) // for loop ** need to research these more**
        {
           if(!bullets[i].activeInHierarchy) // check if bullet with the index i is not active in hierarchy
           return i; //if not active return index to shoot method
        }
        return 0;
    }



    /*void OnDrawGizmos()   
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine (transform.position,target.position);
        }*/
}

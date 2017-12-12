using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class troopsWarrior : MonoBehaviour {
    public string text = "Воин";

    public float curHealth;
    public int price = 200;
    public LayerMask mask;

    private float moveSpeed = 3f;
    private float startHealth = 200;
    private float timeBetweenAtaks = 1f;
    private float timer;
    private int damage = 25;
    private bool isMoving = false;
    private bool attacked;
    private bool enemyFind;
    private NavMeshAgent nav;
    private GameObject enemy;

    private void OnEnable()
    {
        curHealth = startHealth;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = moveSpeed;
        attacked = true;
        timer = 0;
        enemyFind = false;
        if(gameObject.tag != "Enemy")
        {
            BackGroundUI.currentSoldierAmmount++;
        }
        

    }
	
	// Update is called once per frame
	void Update () {
        if (attacked)
        {
            timer += Time.deltaTime;
        }
        if(timer >= timeBetweenAtaks)
        {
            attacked = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
           isMoving = false;

        }
        if ( isMoving)
        {
            Ray ray;
            RaycastHit hit;

            ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000f, mask) && Input.GetMouseButtonDown(0) )
            {
                Moving(hit.point);
            }

        }
        if (enemy != null)
        {
            AtackEnemy(enemy);
        }
    }
    private void OnMouseDown()
    {
        if (gameObject.tag != "Enemy")
        {
            isMoving = true;
        }
    }



    public void TakeDamage(float damage)
    {
        curHealth -=  damage;
        if (curHealth <= 0)
        {
            if(gameObject.tag != "Enemy")
            {
               BackGroundUI.currentSoldierAmmount--;
                
            }
               Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Enemy")
        {
            if (other.gameObject.tag == "Player")
            {
                Moving(other.transform.position);
                enemy = other.gameObject;
            }
        }
        else if (gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Enemy")
            {
                enemyFind = true;
                enemy = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemy = null;
        enemyFind = false;
    }
    private void Moving(Vector3 target)
    {
        if (gameObject.tag == "Player" && isMoving)
        {
            nav.SetDestination(target);
            if (transform.position == target)
            {
                nav.enabled = false;
                nav.enabled = true;
            }
        }
        else if (gameObject.tag == "Enemy")
        {
            nav.SetDestination(target);
            if (transform.position == target)
            {
                nav.enabled = false;
                nav.enabled = true;
            }
        }

    }

    private int EnemyIdentifire(GameObject enemy)
    {
        if (enemy != null)
        {
            TroopsWorker worker = enemy.GetComponent<TroopsWorker>();
            troopsWarrior warrior = enemy.GetComponent<troopsWarrior>();
            troopsBowMan bowMan = enemy.GetComponent<troopsBowMan>();

            if(worker != null)
            {
                return 1;
            }else if( warrior != null)
            {
                return 2;
            }else if( bowMan != null)
            {
                return 3;
            }
            
        }
        return 0;

    }

    private void AtackEnemy(GameObject enemy)
    {
        enemyFind = true;
        int enemvy = EnemyIdentifire(enemy);
        if(!attacked)
        {
            switch (enemvy)
            {
                case 1:
                    enemy.GetComponent<TroopsWorker>().TakeDamage(damage);
                    break;
                case 2:
                    enemy.GetComponent<troopsWarrior>().TakeDamage(damage);
                    break;
                case 3:
                    enemy.GetComponent<troopsBowMan>().TakeDamage(damage);
                    break;
            }
            attacked = true;
            timer = 0;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] Animator anim;
    [HideInInspector] public float currHealth;

    [SerializeField] TMPro.TextMeshProUGUI HealthText;
    [SerializeField] Image filler;
    public bool isDeath;
    
    private void Start()
    {
        currHealth = maxHealth;
    }

    private void Update()
    {
        if (!isDeath)
        {
            filler.fillAmount = currHealth / maxHealth;
            HealthText.text = currHealth.ToString();
        }
    }

    public void decreaseHealth(float decreasehealth)
    {
        if (currHealth - decreasehealth >= 0)
        {
            currHealth -= decreasehealth;
            if (currHealth == 0)
            {
                death();
            }
        }
        else
        {
            death();
        }
    }

    void death()
    {
        currHealth = 0;
        filler.fillAmount = currHealth / maxHealth;
        HealthText.text = currHealth.ToString();
        isDeath = true;
        anim.SetTrigger("Dead");
        anim.SetInteger("DeadRand",Random.Range(0,1));
        GetComponent<CapsuleCollider>().enabled = false;
        var player = GetComponent<PlayerController>();
        var AI = GetComponent<AIController>();
        if (player != null) player.enabled = false;
        if (AI != null) AI.enabled = false;
             
    }
}

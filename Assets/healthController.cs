using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthController : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] Animator anim;
    [HideInInspector] public float currHealth;
    [SerializeField] GameObject fireEffect;
    [SerializeField] TMPro.TextMeshProUGUI HealthText;

    internal void damageWithSpear(float damage)
    {
        throw new System.NotImplementedException();
    }

    [SerializeField] Image filler;
    public bool isDeath;
    [SerializeField] TimerController timer;
    [SerializeField] GameObject damageVFX;
    private bool burning;
    gamemanager manager;
    private void Start()
    {
        currHealth = maxHealth;
        manager = FindObjectOfType<gamemanager>();
    }

    public void fireDamage(float damage)
    {
        StartCoroutine(fire(damage));
    }
    IEnumerator fire(float damage)
    {
        if(!burning)
        {
            burning = true;
            fireEffect.SetActive(true);
            yield return new WaitForSeconds(1);
            decreaseHealth((int)(damage / 3));
            yield return new WaitForSeconds(1);
            decreaseHealth((int)(damage / 3));
            yield return new WaitForSeconds(1);
            decreaseHealth((int)(damage / 3));
            yield return null;
            fireEffect.SetActive(false);
            burning = false;
        }
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
        if(!isDeath && !manager.isfinis)
        {
            if (currHealth - decreasehealth >= 0)
            {
                currHealth -= decreasehealth;
                StartCoroutine(dmgVFX());
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
       
    }
    IEnumerator dmgVFX()
    {
        damageVFX.SetActive(true);
        yield return new WaitForSeconds(1);
        damageVFX.SetActive(false);
    }

    void death()
    {
        currHealth = 0;
        filler.fillAmount = currHealth / maxHealth;
        HealthText.text = currHealth.ToString();
        isDeath = true;
        anim.SetTrigger("Dead");
        anim.SetInteger("DeadRand",Random.Range(0,1));
        var player = GetComponent<PlayerController>();
        var AI = GetComponent<AIController>();
        if (player != null) player.enabled = false;
        if (AI != null) AI.enabled = false;
        manager.finishGame();
    }
}

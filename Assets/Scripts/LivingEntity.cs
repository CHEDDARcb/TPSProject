using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth = 100f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    
    public event Action OnDeath;
    
    private const float minTimeBetDamaged = 0.1f;
    private float lastDamagedTime;

    //攻撃を受けて十分な時間がすぎているか確認
    protected bool IsInvulnerabe
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged) return false;

            return true;
        }
    }

    //LivingEntity活性化の時初期化
    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }

    //外部がらLivingEntityに影響(攻撃)を与える
    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        //無敵状態、自分自身を攻撃、死んでいる場合は、攻撃を受けない
        if (IsInvulnerabe || damageMessage.damager == gameObject || dead) return false;

        lastDamagedTime = Time.time;
        health -= damageMessage.amount;
        
        if (health <= 0) Die();

        return true;
    }

    //回復
    public virtual void RestoreHealth(float newHealth)
    {
        //死んでいる時は回復しない
        if (dead) return;
        
        health += newHealth;
    }

    //死亡
    public virtual void Die()
    {
        //OnDeathに登録しておいたEventを実行する
        if (OnDeath != null) OnDeath();
        
        dead = true;
    }
}
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private Animator animator;
    private AudioSource playerAudioPlayer;

    public AudioClip deathClip;
    public AudioClip hitClip;


    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    ////PlayerHealthが活性化の時初期化
    protected override void OnEnable()
    {
        base.OnEnable();
        //初期HPをUIに適用
        UpdateUI();
    }

    //回復
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        //回復したHPをUIに適用
        UpdateUI();
    }

    //HPのUI表示
    private void UpdateUI()
    {
        UIManager.Instance.UpdateHealthText(dead ? 0f : health);
    }

    //ダメージ適用
    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage)) return false;

        //ダメージを受けた時のエフェクト再生
        EffectManager.Instance.PlayHitEffect(damageMessage.hitPoint, damageMessage.hitNormal, transform, EffectManager.EffectType.Flesh);
        playerAudioPlayer.PlayOneShot(hitClip);

        UpdateUI();
        return true;
    }

    //死亡
    public override void Die()
    {
        base.Die();

        playerAudioPlayer.PlayOneShot(deathClip);
        animator.SetTrigger("Die");

        UpdateUI();
    }
}
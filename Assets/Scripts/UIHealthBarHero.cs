using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarHero : MonoBehaviour
{
    [SerializeField] private Image bar;

    private void Awake()
    {
        SignalBus.Subscribe<SignalHeroDamage>(OnHeroDamage);    
    }

    private void OnHeroDamage(SignalHeroDamage signal)
    {
        bar.fillAmount = (float)signal.health / signal.maxHealth;
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<SignalHeroDamage>(OnHeroDamage);
    }
}
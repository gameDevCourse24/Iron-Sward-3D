using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField, Tooltip("The amount of intelligence the player has when the game starts")]
    private int intelligence = 0;
    [SerializeField, Tooltip("The amount of health kits the player has when the game starts")]
    private int healthKits = 0;
    [SerializeField, Tooltip("The amount of box magazines the player has when the game starts")]
    private int boxMagazine = 0;
    [SerializeField, Tooltip("The amount of ammo the player has when the game starts")]
    private int ammo = 0;
    [SerializeField, Tooltip("The amount of life the player has when the game starts")]
    private int life = 100;

    [SerializeField, Tooltip("The maximum life the player can have")]
    private int maxLife = 100;

    [SerializeField, Tooltip("The minimum life the player can have")]
    private int minLife = 0;

    [SerializeField, Tooltip("The amount of bullets in the current magazine the player has when the game starts")]
    private int bulletInCurrentMagazine = 0;
    [SerializeField, Tooltip("The default amount of bullets in a magazine")]
    private const int defaultBulletInMagazine = 30;
    public void AddIntelligence()
    {
        intelligence++;
    }

    public void AddHealthKit()
    {
        healthKits++;
    }
    private void AddAmmo(int amount)
    {
        ammo += amount;
    }
    public void AddBoxMagazine(int amountOfBullet = defaultBulletInMagazine)
    {
        boxMagazine ++;
        AddAmmo(amountOfBullet);
    }
    
    public void AddLife(int amount = 10)
    {
        life += amount;
        if (life > maxLife)
        {
            life = maxLife;
        }
    }
    public void RemoveLife(int amount = 10)
    {
        life-= amount;
        if (life < minLife)
        {
            life = minLife;
        }
    }
    public void RemoveHealthKit()
    {
        healthKits--;
    }
    public void RemoveAmmo(int amount = 1)
    {
        ammo-= amount;
    }
    public void RemoveIntelligence()
    {
        intelligence--;
    }
    public void RemoveBoxMagazine(int amountOfBullet = defaultBulletInMagazine)
    {
        boxMagazine--;
        SetBulletInCurrentMagazine(amountOfBullet);
    }
    public int GetIntelligence()
    {
        return intelligence;
    }
    public int GetHealthKits()
    {
        return healthKits;
    }
    public int GetAmmo()
    {
        return ammo;
    }
    public int GetLife()
    {
        return life;
    }
    public int GetBoxMagazine()
    {
        return boxMagazine;
    }
    public int GetBulletInCurrentMagazine()
    {
        return bulletInCurrentMagazine;
    }
    public void SetBulletInCurrentMagazine(int amount = defaultBulletInMagazine)
    {
        bulletInCurrentMagazine = amount;
    }
    public void RemoveAmmoFromCurrentMagazine(int amount = 1)
    {
        bulletInCurrentMagazine -= amount;
        ammo -= amount;
    }
    public int GetDefaultBulletInMagazine()
    {
        return defaultBulletInMagazine;
    }
}
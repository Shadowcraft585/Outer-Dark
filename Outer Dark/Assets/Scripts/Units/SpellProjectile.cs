using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public Spell Data; // Daten kommen aus der Spell-Klasse

    void OnTriggerEnter2D(Collider2D other)
    {
        // Treffer-Logik
    }
}
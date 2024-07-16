using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;

    [SerializeField] private Transform holder;

    private Dictionary<int, Magic> projectiles = new Dictionary<int, Magic>();

    private void Awake()
    {
        instance = this;
    }

    public void Spawn(AbstractCharacter character)
    {
        if (!projectiles.ContainsKey(character.id))
        {
            Magic magic = Instantiate(character.magic, character.shootPoint.position, character.characterTransform.rotation, holder);

            magic.transform.right = (character.target.position - character.shootPoint.position).normalized;

            projectiles.Add(character.id, magic);
        }
        else
        {
            projectiles[character.id].Spawn(character.shootPoint.position);
            projectiles[character.id].transform.right = (character.target.position - character.shootPoint.position).normalized;
        }
    }
}

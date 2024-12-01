using UnityEngine;

public class Puzzle_LoopEndPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private Puzzle_Loop loop;

    private void Start()
    {
        loop = GetComponentInParent<Puzzle_Loop>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null && loop.startLoop)
        {
            PlayerManager.instance.player.playerMovement.Teleport(spawnPoint.position);

            if (loop.startLoop)
            {
                loop.ProcessLoop(1);
                loop.IncreaseLoopCount();
            }
        }
    }
}

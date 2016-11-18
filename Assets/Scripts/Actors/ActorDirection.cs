using UnityEngine;

public class ActorDirection : MonoBehaviour {

    [SerializeField]
    private bool _isGoingForward;

    public bool IsGoingForward { get; set; }
    public int Direction { get { return (_isGoingForward ? 1 : -1); } }

    public void FlipDirection()
    {
        _isGoingForward = !_isGoingForward;
    }
}

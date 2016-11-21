using UnityEngine;

public class ActorDirection : MonoBehaviour {

    [SerializeField]
    private bool _isGoingForward = true;

    public bool IsGoingForward { get { return _isGoingForward; } set { _isGoingForward = value; } }

    public int Direction { get { return (_isGoingForward ? 1 : -1); } }

    public void FlipDirection()
    {
        _isGoingForward = !_isGoingForward;
    }
}

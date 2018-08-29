using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _verticalLimit;
    [SerializeField] private GameObject _tooltip;

    private const float TooltipDelay = 3f;

    private bool _tooltipActive = true;

    private void Update()
    {
        var yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(yInput) > 0)
        {
            if (_tooltipActive)
            {
                StartCoroutine(DisableTooltip());
                _tooltipActive = false;
            }

            var yPosition = transform.position.y;
            yPosition += yInput * _speed * Time.deltaTime;
            yPosition = Mathf.Clamp(yPosition, _verticalLimit.x, _verticalLimit.y);

            transform.position = new Vector2(0, yPosition);
        }
    }

    private IEnumerator DisableTooltip()
    {
        yield return new WaitForSeconds(TooltipDelay);
        _tooltip.SetActive(false);
    }
}
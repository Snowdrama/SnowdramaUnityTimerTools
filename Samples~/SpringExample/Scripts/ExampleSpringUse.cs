using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSpringUse : MonoBehaviour
{
    public Snowdrama.Spring.SpringConfiguration springConfig;
    public List<Snowdrama.Spring.SpringConfiguration> springConfigurations;
    private int springConfigIndex;
    private int animationCount;
    private Snowdrama.Spring.Spring3D _positionSpring;
    public Vector3 start;
    public Vector3 end;
    private bool positionToggle;
    private float _timer;

    private string displayText;
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        _positionSpring = new Snowdrama.Spring.Spring3D(springConfig, start);
        _positionSpring.SetPositionTarget(end);
        positionToggle = true;
        _timer = 0;
        displayText = text.text;

        text.text = string.Format(displayText, springConfig.Tension, springConfig.Mass, springConfig.Friction);
    }

    // Update is called once per frame
    void Update()
    {
        _positionSpring.Update(Time.deltaTime);
        _timer += Time.deltaTime;

        if (_timer > 2.0f)
        {
            _timer -= 2.0f;
            positionToggle = !positionToggle;
            if (positionToggle)
            {
                _positionSpring.SetPositionTarget(end);
            }
            else
            {
                _positionSpring.SetPositionTarget(start);
                animationCount++;
            }
        }

        if (animationCount >= 3)
        {
            animationCount = 0;
            springConfigIndex++;
            if (springConfigIndex >= springConfigurations.Count)
            {
                springConfigIndex = 0;
            }
            springConfig = springConfigurations[springConfigIndex];
            _positionSpring.SetSpringConfig(springConfig);
            text.text = string.Format(displayText, springConfig.Tension, springConfig.Mass, springConfig.Friction);
        }
        this.transform.position = _positionSpring.GetSpringPosition();
    }
}

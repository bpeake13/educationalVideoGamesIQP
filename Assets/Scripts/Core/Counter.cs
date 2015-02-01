using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

    public int Value
    {
        get { return value; }
        set 
        {
            this.value = value;
            if (display)
                display.text = string.Format(format, value);
            onChangedEvent.Invoke(value);
        }
    }

    public CounterChangedEvent OnCounterChanged
    {
        get { return onChangedEvent; }
        set { onChangedEvent = value; }
    }

    void Awake()
    {
        Value = Value;
    }

	public
	void add(int input) {
        Value = Mathf.Clamp(value + input, minValue, maxValue);
	}

	public
	void subtract(int input) {
        Value = Mathf.Clamp(value - input, minValue, maxValue);
	}

	public
	void multiply(int input) {
        Value = Mathf.Clamp(value * input, minValue, maxValue);
	}

	public
	void divide(int input) {
        Value = Mathf.Clamp(value / input, minValue, maxValue);
	}

	public
	void setMaxValue(int input) {
		maxValue = input;
	}

	public
	int getMaxValue() {
		return maxValue;
	}

	public
	void setMinValue(int input) {
		minValue = input;
	}

	public
	int getMinValue() {
		return minValue;
	}

    [SerializeField]
    private CounterChangedEvent onChangedEvent = new CounterChangedEvent();

    [SerializeField]
    private TextMesh display;

    [SerializeField]
    private string format = "{0}";

    [SerializeField]
    private int value = 0;
    [SerializeField]
    private int minValue = 0;
    [SerializeField]
    private int maxValue = 99999999;
}

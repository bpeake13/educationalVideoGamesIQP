using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

	public
	int value = 0;
	int minValue = 0;
	int maxValue = 99999999;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public
	void add(int input) {
		value += input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	public
	void subtract(int input) {
		value -= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	public
	void multiply(int input) {
		value *= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	public
	void divide(int input) {
		value /= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	public
	void setValue(int input) {
		value = input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
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

	public
	int getValue() {
		return value;
	}
	
}

using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

	public
	int value = 0;
	int maxValue = 0;
	int minValue = 99999999;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void add(int input) {
		value += input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	void subtract(int input) {
		value -= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	void multiply(int input) {
		value *= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}
	
	void divide(int input) {
		value /= input;
		if(value > maxValue) {
			value = maxValue;
		} else
		if(value < minValue) {
			value = minValue;
		}
	}

	void setValue(int input) {
		value = input;
	}

	void setMaxValue(int input) {
		maxValue = input;
	}

	void setMinValue(int input) {
		minValue = input;
	}

	int getValue() {
		return value;
	}
	
}

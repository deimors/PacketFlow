using UnityEngine;
using System.Collections;

public class NumberDisplay : MonoBehaviour
{
	[SerializeField]
	private TextMesh _textMesh;

	[SerializeField]
	public float Value;

	[SerializeField]
	Color LowColor, HighColor;

	[SerializeField]
	float LowThreshhold, HighThreshhold;

	void Update()
	{
		_textMesh.text = Value.ToString();

		_textMesh.color = Color.Lerp(LowColor, HighColor, GetInterpolationAmount());
	}

	private float GetInterpolationAmount()
		=> (Value - LowThreshhold) / (HighThreshhold - LowThreshhold);
	
}

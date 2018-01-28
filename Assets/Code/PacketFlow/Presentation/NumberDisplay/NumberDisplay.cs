using UnityEngine;
using System.Collections;

public class NumberDisplay : MonoBehaviour
{
	[SerializeField]
	private TextMesh _textMesh;

	[SerializeField]
	public float Value;

	[SerializeField]
	Color LowColor = Color.white, HighColor = Color.white;

	[SerializeField]
	float LowThreshhold = 0, HighThreshhold = 0;

	void Update()
	{
		_textMesh.text = Value.ToString();

		_textMesh.color = Color.Lerp(LowColor, HighColor, GetInterpolationAmount());
	}

	private float GetInterpolationAmount()
		=> (Value - LowThreshhold) / (HighThreshhold - LowThreshhold);
	
}

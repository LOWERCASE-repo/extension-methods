using UnityEngine;

static class Extensions {
	
	internal static Vector2 Predict(this Vector2 relPos, Vector2 relVel, float speed) {
		float a = speed * speed - relVel.sqrMagnitude;
		float b = Vector2.Dot(relPos, relVel);
		float det = b * b + a * relPos.sqrMagnitude;
		if (det < 0f) return relPos;
		det = Mathf.Sqrt(det);
		float timeA = b - det;
		float timeB = b + det;
		if (timeA > 0f) return relPos + timeA * relVel / a;
		if (timeB > 0f) return relPos + timeB * relVel / a;
		return relPos;
	}
	
	internal static float Hermite(this float time, float slope) {
		bool reverse = ((int)time % 2 != 0);
		time %= 1f;
		slope *= 3f;
		float sqrTime = time * time;
		float startSlope = slope > 0f ? slope : 0f;
		float endSlope = slope < 0f ? -slope : 0f;
		float a = sqrTime - 2f * time + 1f;
		float b = sqrTime - time;
		float c = 3f * time - 2f * sqrTime;
		if (reverse) return 1f - time * (a * startSlope + b * endSlope + c);
		return time * (a * startSlope + b * endSlope + c);
	}
	
	internal static float Hermite(this float time) {
		bool reverse = ((int)time % 2 != 0);
		time %= 1f;
		float sqrTime = time * time;
		if (reverse) return 1f - sqrTime * (3f - 2f * time);
		return sqrTime * (3f - 2f * time);
	}
}

Shader "Custom/LineShader" {
	Properties
	{
		_Color("Surface Color", Color) = (1, 1, 1, 1)
		_ChangePoint("Change dist", Float) = 3
		_UnderColor("Underground Color", Color) = (0, 0, 0, 0)
		_MainTex("_Texture", 2D) = "white" {}
		_OtherTex("_OuterTexture", 2D) = "white"{}
		_CentrePoint("Centre", Vector) = (0,0,0,0)
		_BlendThreshold("Blend Dist", Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _OtherTex;
		float4 _CentrePoint;
		float _BlendThreshold;
		float _ChangePoint;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 main = tex2D(_MainTex, IN.uv_MainTex);
			half4 outer = tex2D(_OtherTex, IN.uv_MainTex);
			
			float startBlending = _ChangePoint - _BlendThreshold;
			float endBlending = _ChangePoint + _BlendThreshold;

			float curDistance = distance(_CentrePoint.xyz, IN.worldPos);
			float changeFactor = saturate((curDistance - startBlending) / (_BlendThreshold * 2));

			half4 c = lerp(main, outer, changeFactor);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
	ENDCG
	}
}
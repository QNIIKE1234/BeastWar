Shader "Transparent/AdditiveAlpha" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_AdditiveColor ("Additive Color", Vector) = (1,1,1,1)
		_AdditiveRange ("Additive Alpha", Range(0, 1)) = 0.9
		_MainTex ("Base (RGB) Transparency (A)", 2D) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Transparent/Diffuse"
}
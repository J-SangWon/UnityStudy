// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Magic"
{
	Properties
	{
		_c("c", 2D) = "white" {}
		_d("d", 2D) = "white" {}
		_Move_X("Move_X", Range( -1 , 1)) = 0.5
		_Move_Y("Move_Y", Range( -1 , 1)) = 0.5
		_Scale_X("Scale_X", Range( 0.5 , 10)) = 0.5
		_Scale_Y("Scale_Y", Range( 0.5 , 10)) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _c;
		uniform sampler2D _d;
		uniform float _Move_X;
		uniform float _Move_Y;
		uniform float _Scale_X;
		uniform float _Scale_Y;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float cos19 = cos( -1.0 * _Time.y );
			float sin19 = sin( -1.0 * _Time.y );
			float2 rotator19 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos19 , -sin19 , sin19 , cos19 )) + float2( 0.5,0.5 );
			float cos17 = cos( 1.0 * _Time.y );
			float sin17 = sin( 1.0 * _Time.y );
			float2 rotator17 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos17 , -sin17 , sin17 , cos17 )) + float2( 0.5,0.5 );
			float4 appendResult10 = (float4(_Move_X , _Move_Y , 0.0 , 0.0));
			float4 appendResult13 = (float4(_Scale_X , _Scale_Y , 0.0 , 0.0));
			o.Emission = ( tex2D( _c, rotator19 ) + tex2D( _d, ( ( float4( rotator17, 0.0 , 0.0 ) + appendResult10 ) * appendResult13 ).xy ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16700
48;292;1547;970;2158.855;1653.885;2.572246;True;False
Node;AmplifyShaderEditor.RangedFloatNode;5;-1078.602,-181.3769;Float;False;Property;_Move_X;Move_X;3;0;Create;True;0;0;False;0;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1061.341,-472.3377;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-1069.914,-101.4466;Float;False;Property;_Move_Y;Move_Y;4;0;Create;True;0;0;False;0;0.5;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;10;-791.8965,-160.5256;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1043.268,189.8113;Float;False;Property;_Scale_X;Scale_X;5;0;Create;True;0;0;False;0;0.5;0;0.5;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1043.268,268.2668;Float;False;Property;_Scale_Y;Scale_Y;6;0;Create;True;0;0;False;0;0.5;0;0.5;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;17;-798.0459,-424.8099;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;13;-727.9419,202.3326;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-506.695,-201.0043;Float;True;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-719.7278,-749.9044;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-230.58,-8.362936;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RotatorNode;19;-430.0978,-681.93;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;172.112,-357.6857;Float;True;Property;_c;c;1;0;Create;True;0;0;False;0;6091df1a467c7564389b60478bbe69e6;6091df1a467c7564389b60478bbe69e6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;172.9782,-152.4056;Float;True;Property;_d;d;2;0;Create;True;0;0;False;0;84924c7a35daf1049b4c5f93bf4c27f7;84924c7a35daf1049b4c5f93bf4c27f7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;20;518.5882,-222.3638;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;728.9127,-291.8395;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Magic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;5;0
WireConnection;10;1;6;0
WireConnection;17;0;4;0
WireConnection;13;0;7;0
WireConnection;13;1;8;0
WireConnection;9;0;17;0
WireConnection;9;1;10;0
WireConnection;3;0;9;0
WireConnection;3;1;13;0
WireConnection;19;0;18;0
WireConnection;1;1;19;0
WireConnection;2;1;3;0
WireConnection;20;0;1;0
WireConnection;20;1;2;0
WireConnection;0;2;20;0
ASEEND*/
//CHKSM=ABED0F18D1DD268F41C3FC34D96B59D641255362
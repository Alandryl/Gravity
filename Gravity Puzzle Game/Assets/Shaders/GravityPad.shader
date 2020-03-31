// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader " Gravity/GravityPad"
{
	Properties
	{
		_Noise1("Noise 1", 2D) = "white" {}
		_Noise2("Noise 2", 2D) = "white" {}
		_NoiseColor("Noise Color", Color) = (1,1,1,0)
		_FresnelColor("Fresnel Color", Color) = (1,1,1,0)
		_FresnelPower("Fresnel Power", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _NoiseColor;
		uniform sampler2D _Noise1;
		uniform sampler2D _Noise2;
		uniform float _FresnelPower;
		uniform float4 _FresnelColor;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord7 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float mulTime42 = _Time.y * 0.2;
			float cos41 = cos( mulTime42 );
			float sin41 = sin( mulTime42 );
			float2 rotator41 = mul( uv_TexCoord7 - float2( 1,1 ) , float2x2( cos41 , -sin41 , sin41 , cos41 )) + float2( 1,1 );
			float2 uv_TexCoord52 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float mulTime49 = _Time.y * 0.2;
			float cos51 = cos( mulTime49 );
			float sin51 = sin( mulTime49 );
			float2 rotator51 = mul( uv_TexCoord52 - float2( 0,0 ) , float2x2( cos51 , -sin51 , sin51 , cos51 )) + float2( 0,0 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNDotV22 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode22 = ( 0 + 1 * pow( 1.0 - fresnelNDotV22, _FresnelPower ) );
			o.Emission = ( ( _NoiseColor * ( tex2D( _Noise1, rotator41 ) + tex2D( _Noise2, rotator51 ) ) ) + ( fresnelNode22 * _FresnelColor ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15201
986;73;581;603;2917.871;91.96263;1.246052;False;False
Node;AmplifyShaderEditor.RangedFloatNode;48;-2746.727,424.8566;Float;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-2747.714,54.2779;Float;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;50;-2566.192,302.0807;Float;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;49;-2600.291,436.042;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;44;-2572.163,-48.56126;Float;False;Constant;_Vector3;Vector 3;1;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-2648.894,-165.7557;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;42;-2606.262,85.40009;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;52;-2642.923,184.8861;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;51;-2338.168,317.9483;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;41;-2344.139,-32.69357;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;12;-2112.548,260.7542;Float;True;Property;_Noise2;Noise 2;2;0;Create;True;0;0;False;0;66bf89bc4895cb74a9b37c933cf4cf4e;66bf89bc4895cb74a9b37c933cf4cf4e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-905.7648,624.4939;Float;False;Property;_FresnelPower;Fresnel Power;5;0;Create;True;0;0;False;0;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-2117.101,-76.52038;Float;True;Property;_Noise1;Noise 1;1;0;Create;True;0;0;False;0;66bf89bc4895cb74a9b37c933cf4cf4e;66bf89bc4895cb74a9b37c933cf4cf4e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-1718.09,42.07389;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;23;-658.0597,713.1046;Float;False;Property;_FresnelColor;Fresnel Color;4;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;22;-711.0786,486.9774;Float;True;Tangent;4;0;FLOAT3;0,0,1;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-858.6515,-85.43085;Float;False;Property;_NoiseColor;Noise Color;3;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-413.8207,647.504;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-595.7149,162.051;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-258.0217,271.166;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;31;-1919.012,768.9774;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-3185.011,295.4188;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;14;-3203.084,431.0532;Float;False;Constant;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;0,0.3;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;16;-3208.947,571.3107;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;32;-2179.126,903.9579;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;33;-2159.543,751.8362;Float;False;Constant;_Vector4;Vector 4;6;0;Create;True;0;0;False;0;0,2,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-2155.191,628.0661;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;13;-2948.833,436.3302;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TriplanarNode;28;-1543.202,703.0437;Float;True;Spherical;World;False;Top Texture 0;_TopTexture0;white;0;Assets/Art/Particles/Perlin_01_basecolor.png;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;False;8;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;3;FLOAT;1;False;4;FLOAT;1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;31.00362,109.8045;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard; Gravity/GravityPad;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;49;0;48;0
WireConnection;42;0;47;0
WireConnection;51;0;52;0
WireConnection;51;1;50;0
WireConnection;51;2;49;0
WireConnection;41;0;7;0
WireConnection;41;1;44;0
WireConnection;41;2;42;0
WireConnection;12;1;51;0
WireConnection;2;1;41;0
WireConnection;18;0;2;0
WireConnection;18;1;12;0
WireConnection;22;3;27;0
WireConnection;24;0;22;0
WireConnection;24;1;23;0
WireConnection;10;0;9;0
WireConnection;10;1;18;0
WireConnection;26;0;10;0
WireConnection;26;1;24;0
WireConnection;31;0;29;0
WireConnection;31;2;33;0
WireConnection;31;1;32;0
WireConnection;13;0;15;0
WireConnection;13;2;14;0
WireConnection;13;1;16;0
WireConnection;0;2;26;0
ASEEND*/
//CHKSM=2071C1464A6133D991A9B6B1862A9BDA35B185FF
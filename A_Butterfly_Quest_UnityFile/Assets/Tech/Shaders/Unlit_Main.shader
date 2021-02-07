// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "HDRP/Particles/MainUnlit"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][Enum(UnityEngine.Rendering.BlendMode)]_SrcBlendMode("SrcBlendMode", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)]_DstBlendMode("DstBlendMode", Float) = 10
		[Enum(UnityEngine.Rendering.CullMode)]_CullMode("CullMode", Float) = 2
		[Header(Main Texture)]_MainTexture("Main Texture", 2D) = "white" {}
		_MainColor("Main Color", Color) = (1,1,1,1)
		_SecondaryColor("Secondary Color", Color) = (0,0,0,0)
		_Glow("Glow", Float) = 1
		[Toggle]_IsPanning("Is Panning ?", Float) = 1
		_UVPanningSPEED("UV Panning SPEED", Vector) = (1,0,0,0)
		[Toggle]_UseGreenChannelasErosion("Use Green Channel as Erosion", Float) = 0
		[Toggle]_UseBlueChannelasMask("Use Blue Channel as Mask", Float) = 0
		[Toggle]_UseCustomVertexStreams2("Use Custom Vertex Streams (2.ZW)", Float) = 0
		[Toggle][Header(Mask Texture)]_ActivateEffect("ActivateEffect", Float) = 0
		_MaskCoefficient("Mask Coefficient", Range( 0 , 1)) = 1
		_MaskTexture("MaskTexture", 2D) = "white" {}
		[Toggle]_IsPanning3("Is Panning ?", Float) = 1
		_PannerSpeed2("Panner Speed 2", Vector) = (1,0,0,0)
		[Toggle]_UseCustomVertexStreams3("Use Custom Vertex Streams (3.XY)", Float) = 0
		[Toggle]_IsEmissive("IsEmissive", Float) = 0
		_EmissiveGlow("EmissiveGlow", Range( 0 , 5)) = 1
		[Toggle][Header(Distortion Texture)]_ActivateEffect3("Activate Effect", Float) = 0
		_DistortionTexture("Distortion Texture", 2D) = "white" {}
		[Toggle]_IsPanning4("Is Panning ?", Float) = 1
		_XYSpeedZWClampAlpha("X/Y Speed Z/W Clamp Alpha", Vector) = (0,0,0,1)
		_Flow("Flow", Range( 0 , 1)) = 0
		[Toggle][Header(Erosion Effect)]_ActivateEffect2("Activate Effect", Float) = 0
		_ErosionMap("Erosion Map", 2D) = "white" {}
		_Erosion("Erosion", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[Toggle]_UseCustomVertexStreams1X("Use Custom Vertex Streams (1.X)", Float) = 0
		[Toggle][Header(Mask along UVs)]_MaskAlongUVs("MaskAlongUVs", Float) = 0
		_UVMasking("U.V Masking", Vector) = (1,0,0,0)
		[Toggle]_Invert("Invert", Float) = 0
		[Header(Fake Fog)]_FakeFogColor("Fake Fog Color", Color) = (1,1,1,1)
		_AlphaCoefficient("Alpha Coefficient", Float) = 1
		[ASEEnd][Header(Soft Particle)]_Depth("Depth", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		[HideInInspector]_RenderQueueType("Render Queue Type", Float) = 5
		[HideInInspector][ToggleUI]_AddPrecomputedVelocity("Add Precomputed Velocity", Float) = 1
		//[HideInInspector]_ShadowMatteFilter("Shadow Matte Filter", Float) = 2
		[HideInInspector]_StencilRef("Stencil Ref", Int) = 0
		[HideInInspector]_StencilWriteMask("StencilWrite Mask", Int) = 6
		[HideInInspector]_StencilRefDepth("StencilRefDepth", Int) = 0
		[HideInInspector]_StencilWriteMaskDepth("_StencilWriteMaskDepth", Int) = 8
		[HideInInspector]_StencilRefMV("_StencilRefMV", Int) = 32
		[HideInInspector]_StencilWriteMaskMV("_StencilWriteMaskMV", Int) = 40
		[HideInInspector]_StencilRefDistortionVec("_StencilRefDistortionVec", Int) = 4
		[HideInInspector]_StencilWriteMaskDistortionVec("_StencilWriteMaskDistortionVec", Int) = 4
		[HideInInspector]_StencilWriteMaskGBuffer("_StencilWriteMaskGBuffer", Int) = 14
		[HideInInspector]_StencilRefGBuffer("_StencilRefGBuffer", Int) = 2
		[HideInInspector]_ZTestGBuffer("_ZTestGBuffer", Int) = 4
		[HideInInspector][ToggleUI]_RequireSplitLighting("_RequireSplitLighting", Float) = 0
		[HideInInspector][ToggleUI]_ReceivesSSR("_ReceivesSSR", Float) = 0
		[HideInInspector]_SurfaceType("_SurfaceType", Float) = 1
		[HideInInspector]_BlendMode("_BlendMode", Float) = 0
		[HideInInspector]_SrcBlend("_SrcBlend", Float) = 1
		[HideInInspector]_DstBlend("_DstBlend", Float) = 0
		[HideInInspector]_AlphaSrcBlend("Vec_AlphaSrcBlendtor1", Float) = 1
		[HideInInspector]_AlphaDstBlend("_AlphaDstBlend", Float) = 0
		[HideInInspector][ToggleUI]_ZWrite("_ZWrite", Float) = 1
		[HideInInspector][ToggleUI]_TransparentZWrite("_TransparentZWrite", Float) = 1
		[HideInInspector]_CullMode("Cull Mode", Float) = 2
		[HideInInspector]_TransparentSortPriority("_TransparentSortPriority", Int) = 0
		[HideInInspector][ToggleUI]_EnableFogOnTransparent("_EnableFogOnTransparent", Float) = 1
		[HideInInspector]_CullModeForward("_CullModeForward", Float) = 2
		[HideInInspector][Enum(Front, 1, Back, 2)]_TransparentCullMode("_TransparentCullMode", Float) = 2
		[HideInInspector]_ZTestDepthEqualForOpaque("_ZTestDepthEqualForOpaque", Int) = 4
		[HideInInspector][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestTransparent("_ZTestTransparent", Float) = 4
		[HideInInspector][ToggleUI]_TransparentBackfaceEnable("_TransparentBackfaceEnable", Float) = 0
		[HideInInspector][ToggleUI]_AlphaCutoffEnable("_AlphaCutoffEnable", Float) = 0
		[HideInInspector][ToggleUI]_UseShadowThreshold("_UseShadowThreshold", Float) = 0
		[HideInInspector][ToggleUI]_DoubleSidedEnable("_DoubleSidedEnable", Float) = 0
		[HideInInspector][Enum(Flip, 0, Mirror, 1, None, 2)]_DoubleSidedNormalMode("_DoubleSidedNormalMode", Float) = 2
		[HideInInspector]_DoubleSidedConstants("_DoubleSidedConstants", Vector) = (1, 1, -1, 0)
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="HDRenderPipeline" "RenderType"="Opaque" "Queue"="Transparent" }

		HLSLINCLUDE
		#pragma target 4.5
		#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
		#pragma instancing_options renderinglayer

		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlaneASE (float3 pos, float4 plane)
		{
			return dot (float4(pos,1.0f), plane);
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlaneASE(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlaneASE(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlaneASE(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlaneASE(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		ENDHLSL

		
		Pass
		{
			
			Name "Forward Unlit"
			Tags { "LightMode"="ForwardOnly" }

			Blend [_SrcBlendMode] [_DstBlendMode], [_SrcBlendMode] [_DstBlendMode]
			Cull [_CullMode]
			ZTest [_ZTestTransparent]
			ZWrite [_ZWrite]

			

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_FORWARD_UNLIT
			#pragma multi_compile _ DEBUG_DISPLAY

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			#if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
				#define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
				#define HAS_LIGHTLOOP
				#define SHADOW_OPTIMIZE_REGISTER_USAGE 1

				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonLighting.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Shadow/HDShadowContext.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/HDShadow.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/PunctualLightCommon.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/HDShadowLoop.hlsl"
			#endif

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_VERT_POSITION


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float3 positionRWS : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float3 Color;
				float3 Emission;
				float4 ShadowTint;
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
				surfaceData.color = surfaceDescription.Color;
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription , FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);

				#if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
					HDShadowContext shadowContext = InitShadowContext();
					float shadow;
					float3 shadow3;
					posInput = GetPositionInput(fragInputs.positionSS.xy, _ScreenSize.zw, fragInputs.positionSS.z, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
					float3 normalWS = normalize(fragInputs.tangentToWorld[1]);
					uint renderingLayers = _EnableLightLayers ? asuint(unity_RenderingLayer.x) : DEFAULT_LIGHT_LAYERS;
					ShadowLoopMin(shadowContext, posInput, normalWS, asuint(_ShadowMatteFilter), renderingLayers, shadow3);
					shadow = dot(shadow3, float3(1.0f/3.0f, 1.0f/3.0f, 1.0f/3.0f));

					float4 shadowColor = (1 - shadow)*surfaceDescription.ShadowTint.rgba;
					float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);

					#ifdef _SURFACE_TYPE_TRANSPARENT
						surfaceData.color = lerp(shadowColor.rgb*surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow), surfaceDescription.Alpha);
					#else
						surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow);
					#endif
					localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
					surfaceDescription.Alpha = localAlpha;
				#endif

				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity = surfaceDescription.Alpha;
				builtinData.emissiveColor = surfaceDescription.Emission;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord5 = screenPos52;
				
				o.ase_texcoord1 = float4(inputMesh.positionOS,1);
				o.ase_texcoord2 = inputMesh.ase_texcoord;
				o.ase_texcoord3 = inputMesh.ase_texcoord2;
				o.ase_texcoord4 = inputMesh.ase_texcoord1;
				o.ase_color = inputMesh.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS = inputMesh.normalOS;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				o.positionRWS = positionRWS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			float4 Frag( VertexOutput packedInput ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				float3 positionRWS = packedInput.positionRWS;

				input.positionSS = packedInput.positionCS;
				input.positionRWS = positionRWS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = GetWorldSpaceNormalizeViewDir( input.positionRWS );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float4 transform65 = mul(GetObjectToWorldMatrix(),float4( packedInput.ase_texcoord1.xyz , 0.0 ));
				transform65.xyz = GetAbsolutePositionWS((transform65).xyz);
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord3.z , packedInput.ase_texcoord3.w));
				float2 texCoord79 = packedInput.ase_texcoord2.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_2 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_3 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_2 , temp_cast_3 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord4.z , packedInput.ase_texcoord4.w));
				float2 texCoord26 = packedInput.ase_texcoord2.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float4 lerpResult55 = lerp( _SecondaryColor , _MainColor , tex2DNode44.r);
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord3.x , packedInput.ase_texcoord3.y));
				float2 texCoord19 = packedInput.ase_texcoord2.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord2.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_4 = (temp_output_43_0).xxxx;
				float4 temp_cast_5 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord2.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_4 , temp_cast_5 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				float4 screenPos52 = packedInput.ase_texcoord5;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				
				surfaceDescription.Color = ( ( saturate( distance( transform65 , float4( _WorldSpaceCameraPos , 0.0 ) ) ) * _FakeFogColor ) * ( ( lerpResult55 * _Glow * packedInput.ase_color * lerpResult104 * tex2DNode44.r * temp_output_156_0 * packedInput.ase_color.a ) + ( packedInput.ase_color * ( _EmissiveGlow * ( temp_output_165_0 * temp_output_143_0 ) ) ) ) * temp_output_89_0 * distanceDepth52 ).rgb;
				surfaceDescription.Emission = 0;
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;
				surfaceDescription.ShadowTint = float4( 0, 0 ,0 ,1 );
				float2 Distortion = float2 ( 0, 0 );
				float DistortionBlur = 0;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				BSDFData bsdfData = ConvertSurfaceDataToBSDFData( input.positionSS.xy, surfaceData );

				float4 outColor = ApplyBlendMode( bsdfData.color + builtinData.emissiveColor * GetCurrentExposureMultiplier(), builtinData.opacity );
				outColor = EvaluateAtmosphericScattering( posInput, V, outColor );

				#ifdef DEBUG_DISPLAY
					int bufferSize = int(_DebugViewMaterialArray[0]);
					for (int index = 1; index <= bufferSize; index++)
					{
						int indexMaterialProperty = int(_DebugViewMaterialArray[index]);
						if (indexMaterialProperty != 0)
						{
							float3 result = float3(1.0, 0.0, 1.0);
							bool needLinearToSRGB = false;

							GetPropertiesDataDebug(indexMaterialProperty, result, needLinearToSRGB);
							GetVaryingsDataDebug(indexMaterialProperty, input, result, needLinearToSRGB);
							GetBuiltinDataDebug(indexMaterialProperty, builtinData, result, needLinearToSRGB);
							GetSurfaceDataDebug(indexMaterialProperty, surfaceData, result, needLinearToSRGB);
							GetBSDFDataDebug(indexMaterialProperty, bsdfData, result, needLinearToSRGB);

							if (!needLinearToSRGB)
								result = SRGBToLinear(max(0, result));

							outColor = float4(result, 1.0);
						}
					}

					if (_DebugFullScreenMode == FULLSCREENDEBUGMODE_TRANSPARENCY_OVERDRAW)
					{
						float4 result = _DebugTransparencyOverdrawWeight * float4(TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_A);
						outColor = result;
					}
				#endif
				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			Cull [_CullMode]
			ZWrite On
			ZClip [_ZClip]
			ColorMask 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_SHADOWS

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE (BuiltinData, builtinData);
				builtinData.opacity = surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord3 = screenPos52;
				
				o.ase_texcoord = inputMesh.ase_texcoord;
				o.ase_texcoord1 = inputMesh.ase_texcoord2;
				o.ase_texcoord2 = inputMesh.ase_texcoord1;
				o.ase_color = inputMesh.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =  defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					#ifdef WRITE_NORMAL_BUFFER
					, out float4 outNormalBuffer : SV_Target0
						#ifdef WRITE_MSAA_DEPTH
						, out float1 depthColor : SV_Target1
						#endif
					#elif defined(WRITE_MSAA_DEPTH)
					, out float4 outNormalBuffer : SV_Target0
					, out float1 depthColor : SV_Target1
					#elif defined(SCENESELECTIONPASS)
					, out float4 outColor : SV_Target0
					#endif
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );

				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);

				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord1.z , packedInput.ase_texcoord1.w));
				float2 texCoord79 = packedInput.ase_texcoord.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_0 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_1 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_0 , temp_cast_1 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord2.z , packedInput.ase_texcoord2.w));
				float2 texCoord26 = packedInput.ase_texcoord.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord1.x , packedInput.ase_texcoord1.y));
				float2 texCoord19 = packedInput.ase_texcoord.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float4 screenPos52 = packedInput.ase_texcoord3;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_2 = (temp_output_43_0).xxxx;
				float4 temp_cast_3 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_2 , temp_cast_3 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription,input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );
				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.positionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.positionCS.z;
				#elif defined(SCENESELECTIONPASS)
				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
				#endif
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "META"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_LIGHT_TRANSPORT

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_VERT_POSITION


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_color : COLOR;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			CBUFFER_START( UnityMetaPass )
			bool4 unity_MetaVertexControl;
			bool4 unity_MetaFragmentControl;
			CBUFFER_END

			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float3 Color;
				float3 Emission;
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData( FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData )
			{
				ZERO_INITIALIZE( SurfaceData, surfaceData );
				surfaceData.color = surfaceDescription.Color;
			}

			void GetSurfaceAndBuiltinData( SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData )
			{
				#if _ALPHATEST_ON
				DoAlphaTest( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData( fragInputs, surfaceDescription, V, surfaceData );
				ZERO_INITIALIZE( BuiltinData, builtinData );
				builtinData.opacity = surfaceDescription.Alpha;
				builtinData.emissiveColor = surfaceDescription.Emission;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID( inputMesh );
				UNITY_TRANSFER_INSTANCE_ID( inputMesh, o );

				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord4 = screenPos52;
				
				o.ase_texcoord = float4(inputMesh.positionOS,1);
				o.ase_texcoord1 = inputMesh.ase_texcoord;
				o.ase_texcoord2 = inputMesh.uv2;
				o.ase_texcoord3 = inputMesh.uv1;
				o.ase_color = inputMesh.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =  defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float2 uv = float2( 0.0, 0.0 );
				if( unity_MetaVertexControl.x )
				{
					uv = inputMesh.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				}
				else if( unity_MetaVertexControl.y )
				{
					uv = inputMesh.uv2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				}

				o.positionCS = float4( uv * 2.0 - 1.0, inputMesh.positionOS.z > 0 ? 1.0e-4 : 0.0, 1.0 );
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.uv1 = v.uv1;
				o.uv2 = v.uv2;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.uv1 = patch[0].uv1 * bary.x + patch[1].uv1 * bary.y + patch[2].uv1 * bary.z;
				o.uv2 = patch[0].uv2 * bary.x + patch[1].uv2 * bary.y + patch[2].uv2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			float4 Frag( VertexOutput packedInput  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				FragInputs input;
				ZERO_INITIALIZE( FragInputs, input );
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput( input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS );

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float4 transform65 = mul(GetObjectToWorldMatrix(),float4( packedInput.ase_texcoord.xyz , 0.0 ));
				transform65.xyz = GetAbsolutePositionWS((transform65).xyz);
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord2.z , packedInput.ase_texcoord2.w));
				float2 texCoord79 = packedInput.ase_texcoord1.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_2 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_3 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_2 , temp_cast_3 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord3.z , packedInput.ase_texcoord3.w));
				float2 texCoord26 = packedInput.ase_texcoord1.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float4 lerpResult55 = lerp( _SecondaryColor , _MainColor , tex2DNode44.r);
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord2.x , packedInput.ase_texcoord2.y));
				float2 texCoord19 = packedInput.ase_texcoord1.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord1.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_4 = (temp_output_43_0).xxxx;
				float4 temp_cast_5 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord1.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_4 , temp_cast_5 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				float4 screenPos52 = packedInput.ase_texcoord4;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				
				surfaceDescription.Color = ( ( saturate( distance( transform65 , float4( _WorldSpaceCameraPos , 0.0 ) ) ) * _FakeFogColor ) * ( ( lerpResult55 * _Glow * packedInput.ase_color * lerpResult104 * tex2DNode44.r * temp_output_156_0 * packedInput.ase_color.a ) + ( packedInput.ase_color * ( _EmissiveGlow * ( temp_output_165_0 * temp_output_143_0 ) ) ) ) * temp_output_89_0 * distanceDepth52 ).rgb;
				surfaceDescription.Emission = 0;
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData( surfaceDescription,input, V, posInput, surfaceData, builtinData );

				BSDFData bsdfData = ConvertSurfaceDataToBSDFData( input.positionSS.xy, surfaceData );
				LightTransportData lightTransportData = GetLightTransportData( surfaceData, builtinData, bsdfData );

				float4 res = float4( 0.0, 0.0, 0.0, 1.0 );
				if( unity_MetaFragmentControl.x )
				{
					res.rgb = clamp( pow( abs( lightTransportData.diffuseColor ), saturate( unity_OneOverOutputBoost ) ), 0, unity_MaxOutputValue );
				}

				if( unity_MetaFragmentControl.y )
				{
					res.rgb = lightTransportData.emissiveColor;
				}

				return res;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "SceneSelectionPass"
			Tags { "LightMode"="SceneSelectionPass" }

			Cull [_CullMode]
			ZWrite On

			ColorMask 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_DEPTH_ONLY
			#define SCENESELECTIONPASS
			#pragma editor_sync_compilation

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			int _ObjectId;
			int _PassValue;

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord3 = screenPos52;
				
				o.ase_texcoord = inputMesh.ase_texcoord;
				o.ase_texcoord1 = inputMesh.ase_texcoord2;
				o.ase_texcoord2 = inputMesh.ase_texcoord1;
				o.ase_color = inputMesh.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =   defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					, out float4 outColor : SV_Target0
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceData surfaceData;
				BuiltinData builtinData;
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord1.z , packedInput.ase_texcoord1.w));
				float2 texCoord79 = packedInput.ase_texcoord.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_0 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_1 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_0 , temp_cast_1 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord2.z , packedInput.ase_texcoord2.w));
				float2 texCoord26 = packedInput.ase_texcoord.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord1.x , packedInput.ase_texcoord1.y));
				float2 texCoord19 = packedInput.ase_texcoord.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float4 screenPos52 = packedInput.ase_texcoord3;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_2 = (temp_output_43_0).xxxx;
				float4 temp_cast_3 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_2 , temp_cast_3 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthForwardOnly"
			Tags { "LightMode"="DepthForwardOnly" }

			Cull [_CullMode]
			ZWrite On
			Stencil
			{
				Ref [_StencilRefDepth]
				WriteMask [_StencilWriteMaskDepth]
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}


			ColorMask 0 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_DEPTH_ONLY
			#pragma multi_compile _ WRITE_MSAA_DEPTH

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord3 = screenPos52;
				
				o.ase_texcoord = inputMesh.ase_texcoord;
				o.ase_texcoord1 = inputMesh.ase_texcoord2;
				o.ase_texcoord2 = inputMesh.ase_texcoord1;
				o.ase_color = inputMesh.ase_color;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =   defaultVertexValue ;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					#ifdef WRITE_NORMAL_BUFFER
					, out float4 outNormalBuffer : SV_Target0
						#ifdef WRITE_MSAA_DEPTH
						, out float1 depthColor : SV_Target1
						#endif
					#elif defined(WRITE_MSAA_DEPTH)
					, out float4 outNormalBuffer : SV_Target0
					, out float1 depthColor : SV_Target1
					#elif defined(SCENESELECTIONPASS)
					, out float4 outColor : SV_Target0
					#endif
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);

				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord1.z , packedInput.ase_texcoord1.w));
				float2 texCoord79 = packedInput.ase_texcoord.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_0 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_1 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_0 , temp_cast_1 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord2.z , packedInput.ase_texcoord2.w));
				float2 texCoord26 = packedInput.ase_texcoord.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord1.x , packedInput.ase_texcoord1.y));
				float2 texCoord19 = packedInput.ase_texcoord.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float4 screenPos52 = packedInput.ase_texcoord3;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_2 = (temp_output_43_0).xxxx;
				float4 temp_cast_3 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_2 , temp_cast_3 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );
				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.positionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.positionCS.z;
				#elif defined(SCENESELECTIONPASS)
				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
				#endif
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Motion Vectors"
			Tags { "LightMode"="MotionVectors" }

			Cull [_CullMode]

			ZWrite On

			Stencil
			{
				Ref [_StencilRefMV]
				WriteMask [_StencilWriteMaskMV]
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}


			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70502

			#define SHADERPASS SHADERPASS_MOTION_VECTORS
			#pragma multi_compile _ WRITE_MSAA_DEPTH

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float3 previousPositionOS : TEXCOORD4;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					float3 precomputedVelocity : TEXCOORD5;
				#endif
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 vmeshPositionCS : SV_Position;
				float3 vmeshInterp00 : TEXCOORD0;
				float3 vpassInterpolators0 : TEXCOORD1; //interpolators0
				float3 vpassInterpolators1 : TEXCOORD2; //interpolators1
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_color : COLOR;
				float4 ase_texcoord6 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _XYSpeedZWClampAlpha;
			float4 _FakeFogColor;
			float4 _SecondaryColor;
			float4 _MainColor;
			float4 _ErosionMap_ST;
			float4 _MaskTexture_ST;
			float4 _DistortionTexture_ST;
			float4 _MainTexture_ST;
			float2 _PannerSpeed2;
			float2 _UVMasking;
			float2 _UVPanningSPEED;
			float _MaskAlongUVs;
			float _Invert;
			float _EmissiveGlow;
			float _SrcBlendMode;
			float _UseCustomVertexStreams1X;
			float _Smoothness;
			float _UseGreenChannelasErosion;
			float _ActivateEffect2;
			float _Erosion;
			float _ActivateEffect;
			float _UseCustomVertexStreams3;
			float _IsEmissive;
			float _Depth;
			float _IsPanning3;
			float _UseBlueChannelasMask;
			float _Glow;
			float _ActivateEffect3;
			float _IsPanning4;
			float _Flow;
			float _UseCustomVertexStreams2;
			float _IsPanning;
			float _DstBlendMode;
			float _MaskCoefficient;
			float _AlphaCoefficient;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _MainTexture;
			sampler2D _DistortionTexture;
			sampler2D _MaskTexture;
			sampler2D _ErosionMap;


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexInput ApplyMeshModification(VertexInput inputMesh, float3 timeParameters, inout VertexOutput o )
			{
				_TimeParameters.xyz = timeParameters;
				float3 vertexPos52 = inputMesh.positionOS;
				float4 ase_clipPos52 = TransformWorldToHClip( TransformObjectToWorld(vertexPos52));
				float4 screenPos52 = ComputeScreenPos( ase_clipPos52 , _ProjectionParams.x );
				o.ase_texcoord6 = screenPos52;
				
				o.ase_texcoord3 = inputMesh.ase_texcoord;
				o.ase_texcoord4 = inputMesh.ase_texcoord2;
				o.ase_texcoord5 = inputMesh.ase_texcoord1;
				o.ase_color = inputMesh.ase_color;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =  defaultVertexValue ;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif
				inputMesh.normalOS =  inputMesh.normalOS ;
				return inputMesh;
			}

			VertexOutput VertexFunction(VertexInput inputMesh)
			{
				VertexOutput o = (VertexOutput)0;
				VertexInput defaultMesh = inputMesh;

				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				inputMesh = ApplyMeshModification( inputMesh, _TimeParameters.xyz, o);

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				float3 normalWS = TransformObjectToWorldNormal(inputMesh.normalOS);

				float3 VMESHpositionRWS = positionRWS;
				float4 VMESHpositionCS = TransformWorldToHClip(positionRWS);

				//#if defined(UNITY_REVERSED_Z)
				//	VMESHpositionCS.z -= unity_MotionVectorsParams.z * VMESHpositionCS.w;
				//#else
				//	VMESHpositionCS.z += unity_MotionVectorsParams.z * VMESHpositionCS.w;
				//#endif

				float4 VPASSpreviousPositionCS;
				float4 VPASSpositionCS = mul(UNITY_MATRIX_UNJITTERED_VP, float4(VMESHpositionRWS, 1.0));

				bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
				if (forceNoMotion)
				{
					VPASSpreviousPositionCS = float4(0.0, 0.0, 0.0, 1.0);
				}
				else
				{
					bool hasDeformation = unity_MotionVectorsParams.x > 0.0;
					float3 effectivePositionOS = (hasDeformation ? inputMesh.previousPositionOS : defaultMesh.positionOS);
					#if defined(_ADD_PRECOMPUTED_VELOCITY)
					effectivePositionOS -= inputMesh.precomputedVelocity;
					#endif

					#if defined(HAVE_MESH_MODIFICATION)
						VertexInput previousMesh = defaultMesh;
						previousMesh.positionOS = effectivePositionOS ;
						VertexOutput test = (VertexOutput)0;
						float3 curTime = _TimeParameters.xyz;
						previousMesh = ApplyMeshModification(previousMesh, _LastTimeParameters.xyz, test);
						_TimeParameters.xyz = curTime;
						float3 previousPositionRWS = TransformPreviousObjectToWorld(previousMesh.positionOS);
					#else
						float3 previousPositionRWS = TransformPreviousObjectToWorld(effectivePositionOS);
					#endif

					#ifdef ATTRIBUTES_NEED_NORMAL
						float3 normalWS = TransformPreviousObjectToWorldNormal(defaultMesh.normalOS);
					#else
						float3 normalWS = float3(0.0, 0.0, 0.0);
					#endif

					#if defined(HAVE_VERTEX_MODIFICATION)
						//ApplyVertexModification(inputMesh, normalWS, previousPositionRWS, _LastTimeParameters.xyz);
					#endif

					VPASSpreviousPositionCS = mul(UNITY_MATRIX_PREV_VP, float4(previousPositionRWS, 1.0));
				}

				o.vmeshPositionCS = VMESHpositionCS;
				o.vmeshInterp00.xyz = VMESHpositionRWS;

				o.vpassInterpolators0 = float3(VPASSpositionCS.xyw);
				o.vpassInterpolators1 = float3(VPASSpreviousPositionCS.xyw);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float3 previousPositionOS : TEXCOORD4;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					float3 precomputedVelocity : TEXCOORD5;
				#endif
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.previousPositionOS = v.previousPositionOS;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					o.precomputedVelocity = v.precomputedVelocity;
				#endif
				o.ase_texcoord = v.ase_texcoord;
				o.ase_texcoord2 = v.ase_texcoord2;
				o.ase_texcoord1 = v.ase_texcoord1;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.previousPositionOS = patch[0].previousPositionOS * bary.x + patch[1].previousPositionOS * bary.y + patch[2].previousPositionOS * bary.z;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					o.precomputedVelocity = patch[0].precomputedVelocity * bary.x + patch[1].precomputedVelocity * bary.y + patch[2].precomputedVelocity * bary.z;
				#endif
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_texcoord2 = patch[0].ase_texcoord2 * bary.x + patch[1].ase_texcoord2 * bary.y + patch[2].ase_texcoord2 * bary.z;
				o.ase_texcoord1 = patch[0].ase_texcoord1 * bary.x + patch[1].ase_texcoord1 * bary.y + patch[2].ase_texcoord1 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
						, out float4 outMotionVector : SV_Target0
						#ifdef WRITE_NORMAL_BUFFER
						, out float4 outNormalBuffer : SV_Target1
							#ifdef WRITE_MSAA_DEPTH
								, out float1 depthColor : SV_Target2
							#endif
						#elif defined(WRITE_MSAA_DEPTH)
						, out float4 outNormalBuffer : SV_Target1
						, out float1 depthColor : SV_Target2
						#endif

						#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
						#endif
						
					)
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				UNITY_SETUP_INSTANCE_ID( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.vmeshPositionCS;
				input.positionRWS = packedInput.vmeshInterp00.xyz;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = GetWorldSpaceNormalizeViewDir(input.positionRWS);

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float temp_output_7_0 = step( 0.9 , _UseCustomVertexStreams2 );
				float2 appendResult114 = (float2(_XYSpeedZWClampAlpha.x , _XYSpeedZWClampAlpha.y));
				float2 appendResult76 = (float2(packedInput.ase_texcoord4.z , packedInput.ase_texcoord4.w));
				float2 texCoord79 = packedInput.ase_texcoord3.xy * _DistortionTexture_ST.xy + appendResult76;
				float2 panner81 = ( ( _TimeParameters.x * _IsPanning4 ) * appendResult114 + texCoord79);
				float4 tex2DNode82 = tex2D( _DistortionTexture, panner81 );
				float2 appendResult83 = (float2(tex2DNode82.g , tex2DNode82.b));
				float2 temp_cast_0 = (_XYSpeedZWClampAlpha.z).xx;
				float2 temp_cast_1 = (_XYSpeedZWClampAlpha.w).xx;
				float2 clampResult86 = clamp( ( ( appendResult83 + -0.5 ) * 2.0 ) , temp_cast_0 , temp_cast_1 );
				float2 flowedUV73 = ( _Flow * clampResult86 * step( 0.5 , _ActivateEffect3 ) );
				float2 appendResult8 = (float2(packedInput.ase_texcoord5.z , packedInput.ase_texcoord5.w));
				float2 texCoord26 = packedInput.ase_texcoord3.xy * _MainTexture_ST.xy + ( _MainTexture_ST.zw + ( appendResult8 * temp_output_7_0 ) );
				float2 panner35 = ( ( _TimeParameters.x * _IsPanning * ( 1.0 - temp_output_7_0 ) ) * _UVPanningSPEED + ( flowedUV73 + texCoord26 ));
				float4 tex2DNode44 = tex2D( _MainTexture, panner35 );
				float mainBlueChannel129 = tex2DNode44.b;
				float temp_output_6_0 = step( 0.9 , _UseCustomVertexStreams3 );
				float2 appendResult5 = (float2(packedInput.ase_texcoord4.x , packedInput.ase_texcoord4.y));
				float2 texCoord19 = packedInput.ase_texcoord3.xy * _MaskTexture_ST.xy + ( _MaskTexture_ST.zw + ( appendResult5 * temp_output_6_0 ) );
				float2 panner32 = ( ( _TimeParameters.x * _IsPanning3 * ( 1.0 - temp_output_6_0 ) ) * _PannerSpeed2 + texCoord19);
				float temp_output_143_0 = ( ( mainBlueChannel129 * step( 0.5 , _UseBlueChannelasMask ) ) + ( step( _UseBlueChannelasMask , 0.0 ) * tex2D( _MaskTexture, panner32 ).r ) );
				float temp_output_165_0 = step( 0.5 , _IsEmissive );
				float lerpResult104 = lerp( 1.0 , temp_output_143_0 , ( ( 1.0 - temp_output_165_0 ) * saturate( ( _MaskCoefficient * step( 0.5 , _ActivateEffect ) ) ) ));
				float2 break151 = ( texCoord19 * _UVMasking );
				float lerpResult149 = lerp( 1.0 , ( break151.x + break151.y ) , step( 0.5 , _MaskAlongUVs ));
				float temp_output_156_0 = ( ( ( 1.0 - lerpResult149 ) * _Invert ) + ( ( 1.0 - _Invert ) * lerpResult149 ) );
				float4 screenPos52 = packedInput.ase_texcoord6;
				float4 ase_screenPosNorm52 = screenPos52 / screenPos52.w;
				ase_screenPosNorm52.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm52.z : ase_screenPosNorm52.z * 0.5 + 0.5;
				float screenDepth52 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm52.xy ),_ZBufferParams);
				float distanceDepth52 = saturate( abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm52.z,_ZBufferParams ) ) / ( _Depth ) ) );
				float temp_output_53_0 = ( ( tex2DNode44.r * lerpResult104 * _AlphaCoefficient * packedInput.ase_color.a * temp_output_156_0 ) * distanceDepth52 );
				float clampResult59 = clamp( temp_output_53_0 , temp_output_53_0 , 1.0 );
				float clampResult36 = clamp( ( _UseCustomVertexStreams1X * packedInput.ase_texcoord3.z ) , 0.0 , 1.0 );
				float temp_output_43_0 = ( ( _Erosion * ( 1.0 - _UseCustomVertexStreams1X ) ) + clampResult36 );
				float4 temp_cast_2 = (temp_output_43_0).xxxx;
				float4 temp_cast_3 = (( temp_output_43_0 + ( temp_output_43_0 * _Smoothness ) )).xxxx;
				float mainGreenChannel128 = tex2DNode44.g;
				float2 uv_ErosionMap = packedInput.ase_texcoord3.xy * _ErosionMap_ST.xy + _ErosionMap_ST.zw;
				float4 smoothstepResult61 = smoothstep( temp_cast_2 , temp_cast_3 , ( ( mainGreenChannel128 * step( 0.5 , _UseGreenChannelasErosion ) ) + ( step( _UseGreenChannelasErosion , 0.0 ) * tex2D( _ErosionMap, uv_ErosionMap ) ) ));
				float4 temp_output_89_0 = saturate( ( ( smoothstepResult61 * _ActivateEffect2 ) + step( 0.5 , ( 1.0 - _ActivateEffect2 ) ) ) );
				
				surfaceDescription.Alpha = ( clampResult59 * temp_output_89_0 ).r;
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				float4 VPASSpositionCS = float4(packedInput.vpassInterpolators0.xy, 0.0, packedInput.vpassInterpolators0.z);
				float4 VPASSpreviousPositionCS = float4(packedInput.vpassInterpolators1.xy, 0.0, packedInput.vpassInterpolators1.z);

				#ifdef _DEPTHOFFSET_ON
				VPASSpositionCS.w += builtinData.depthOffset;
				VPASSpreviousPositionCS.w += builtinData.depthOffset;
				#endif

				float2 motionVector = CalculateMotionVector( VPASSpositionCS, VPASSpreviousPositionCS );
				EncodeMotionVector( motionVector * 0.5, outMotionVector );

				bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
				if( forceNoMotion )
					outMotionVector = float4( 2.0, 0.0, 0.0, 0.0 );

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );

				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.vmeshPositionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.vmeshPositionCS.z;
				#endif

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif
			}

			ENDHLSL
		}

	
	}
	CustomEditor "UnityEditor.Rendering.HighDefinition.HDLitGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18800
-1127;29;1127;978;4279.434;1263.166;4.530717;True;False
Node;AmplifyShaderEditor.CommentaryNode;96;-3957.913,2365.848;Inherit;False;2251.152;809.438;Flow;21;72;74;77;76;75;79;80;81;82;83;84;87;86;88;73;111;112;113;114;116;117;Distortion effect;0.7373281,0.3349057,1,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;72;-3907.913,2627.185;Inherit;False;2;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;74;-3305.735,2862.44;Float;False;Property;_IsPanning4;Is Panning ?;22;1;[Toggle];Create;False;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;113;-3756.862,2926.031;Inherit;False;Property;_XYSpeedZWClampAlpha;X/Y Speed Z/W Clamp Alpha;23;0;Create;True;0;0;0;False;0;False;0,0,0,1;-1,0.5,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureTransformNode;77;-3540.4,2494.614;Inherit;False;82;False;1;0;SAMPLER2D;_Sampler077;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.DynamicAppendNode;76;-3676.184,2700.752;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;75;-3302.013,2782.224;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;114;-3268.073,2642.083;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-3057.639,2796.464;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-3274.36,2486.381;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;81;-2987.728,2578.09;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;82;-2802.37,2473.296;Inherit;True;Property;_DistortionTexture;Distortion Texture;21;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;125;-3681.808,-970.0246;Inherit;False;2408.891;1133.6;Comment;22;129;128;55;48;50;44;35;31;29;30;25;23;20;22;26;17;11;12;8;7;4;3;Main Texture;0.25178,0.4339623,0.3680638,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;83;-2652.487,2720.546;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-2278.998,2878.308;Inherit;False;Property;_ActivateEffect3;Activate Effect;20;1;[Toggle];Create;False;0;0;0;False;1;Header(Distortion Texture);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;117;-2446.893,3007.834;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;116;-2412.248,3053.469;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;84;-2652.841,2842.258;Inherit;False;ConstantBiasScale;-1;;1;63208df05c83e8e49a48ffbdce2e43a0;0;3;3;FLOAT2;0,0;False;1;FLOAT;-0.5;False;2;FLOAT;2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;3;-3631.808,-697.0638;Inherit;False;1;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-3629.219,-377.3701;Inherit;False;Property;_UseCustomVertexStreams2;Use Custom Vertex Streams (2.ZW);11;1;[Toggle];Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;160;-3789.358,286.2303;Inherit;False;2725.86;1182.65;Secondary texture to mask out the main texture;35;162;165;168;169;104;167;163;166;105;143;142;110;141;103;37;138;140;108;139;93;32;137;27;21;15;13;14;19;16;10;9;6;5;2;1;Mask Texture;0.3962264,0.3756675,0.3756675,1;0;0
Node;AmplifyShaderEditor.StepOpNode;112;-2052.93,2940.961;Inherit;True;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;86;-2288.691,2673.787;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-3352.16,-639.8743;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;7;-3278.03,-395.3349;Inherit;True;2;0;FLOAT;0.9;False;1;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-2371.349,2415.848;Inherit;False;Property;_Flow;Flow;24;0;Create;True;0;0;0;False;0;False;0;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-3739.358,1196.933;Inherit;False;Property;_UseCustomVertexStreams3;Use Custom Vertex Streams (3.XY);17;1;[Toggle];Create;False;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureTransformNode;12;-3220.308,-853.5518;Inherit;False;44;False;1;0;SAMPLER2D;_Sampler012;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-2130.745,2572.489;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-3130.457,-685.5678;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;2;-3712.746,728.355;Inherit;False;2;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-2948.042,-696.7573;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;73;-1930.758,2644.608;Inherit;False;flowedUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StepOpNode;6;-3385.633,1178.969;Inherit;True;2;0;FLOAT;0.9;False;1;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;-3464.404,776.8578;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2944.179,-325.4395;Float;False;Property;_IsPanning;Is Panning ?;7;1;[Toggle];Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureTransformNode;10;-3339.343,592.1852;Inherit;False;37;False;1;0;SAMPLER2D;_Sampler010;False;2;FLOAT2;0;FLOAT2;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-3211.395,739.8511;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;20;-2941.823,-187.448;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-2803.486,-788.463;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;25;-2815.796,-920.0246;Inherit;False;73;flowedUV;1;0;OBJECT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleTimeNode;23;-2949.797,-409.4673;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-2513.208,-805.2326;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-3045.3,732.7361;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-2688.179,-389.4399;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;161;-3346.923,1567.635;Inherit;False;1814.08;509.317;Additional Masking option to hide specific UV parts;14;153;151;152;149;146;147;155;156;158;157;154;159;145;144;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;30;-2949.437,-548.1766;Float;False;Property;_UVPanningSPEED;UV Panning SPEED;8;0;Create;True;0;0;0;False;0;False;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;35;-2342.749,-509.7229;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;144;-3296.923,1876.558;Inherit;False;Property;_UVMasking;U.V Masking;31;0;Create;True;0;0;0;False;0;False;1,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;15;-3038.045,964.8351;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-3032.427,1048.863;Float;False;Property;_IsPanning3;Is Panning ?;15;1;[Toggle];Create;False;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-2886.244,667.4332;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;13;-3030.071,1186.855;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;101;-1424.784,2049.432;Inherit;False;2341.106;1240.034;Comment;27;89;122;119;120;121;61;58;118;57;51;98;40;43;38;36;33;28;34;18;24;132;133;130;127;135;136;134;Erosion;1,0.5058824,0.5920227,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;24;-1270.725,3010.634;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-1384.068,2885.639;Inherit;False;Property;_UseCustomVertexStreams1X;Use Custom Vertex Streams (1.X);29;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;-1988.642,-205.389;Inherit;True;Property;_MainTexture;Main Texture;3;0;Create;True;0;0;0;False;1;Header(Main Texture);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-3056.094,1755.325;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-2776.427,984.8628;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;21;-3185.214,862.1887;Float;False;Property;_PannerSpeed2;Panner Speed 2;16;0;Create;True;0;0;0;False;0;False;1,0;-2,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;93;-2579.965,1229.564;Inherit;False;Property;_ActivateEffect;ActivateEffect;12;1;[Toggle];Create;True;0;0;0;False;1;Header(Mask Texture);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;151;-2904.687,1768.987;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.PannerNode;32;-2615.226,828.7137;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;34;-1001.527,2808.254;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1013.626,2976.317;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;108;-2405.764,1017.641;Float;False;Property;_MaskCoefficient;Mask Coefficient;13;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1090.361,2723.513;Inherit;False;Property;_Erosion;Erosion;27;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;137;-2797.54,490.8693;Inherit;False;Property;_UseBlueChannelasMask;Use Blue Channel as Mask;10;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;103;-2343.816,1214.88;Inherit;True;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;129;-1647.087,51.05122;Float;False;mainBlueChannel;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-2883.492,1903.051;Inherit;False;Property;_MaskAlongUVs;MaskAlongUVs;30;1;[Toggle];Create;True;0;0;0;False;1;Header(Mask along UVs);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;162;-2057.157,784.0004;Inherit;False;Property;_IsEmissive;IsEmissive;18;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-386.3911,2320.11;Inherit;False;Property;_UseGreenChannelasErosion;Use Green Channel as Erosion;9;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-769.5101,2724.233;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;165;-1926.834,681.9995;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;152;-2681.569,1767.247;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;37;-2369.862,697.6234;Inherit;True;Property;_MaskTexture;MaskTexture;14;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;128;-1653.637,-25.32333;Float;False;mainGreenChannel;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-2095.28,1129.103;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;140;-2487.536,413.2014;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;36;-849.6361,2923.645;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;139;-2489.775,539.2314;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;138;-2715.89,346.107;Inherit;False;129;mainBlueChannel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;147;-2685.884,1880.557;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;135;-78.62607,2368.472;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;133;-262.6262,2139.471;Inherit;False;128;mainGreenChannel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;130;-76.38736,2242.442;Inherit;False;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-617.0411,2866.425;Inherit;False;Property;_Smoothness;Smoothness;28;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;57;-226.7245,2560.517;Inherit;True;Property;_ErosionMap;Erosion Map;26;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-531.3547,2679.281;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;141;-2279.43,336.2303;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;149;-2520.264,1736.55;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;105;-1951.814,1100.803;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;154;-2305.353,1750.374;Inherit;False;Property;_Invert;Invert;32;1;[Toggle];Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;142;-2351.775,582.2314;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;169;-1797.581,818.7891;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;168;-1640.171,932.2808;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;159;-2294.924,1964.589;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;98;-211.6843,2768.663;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;153;-2281.559,1617.635;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;131.7189,2165.471;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;143;-2126.241,490.7935;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;158;-2133.68,1850.297;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;59.37396,2411.472;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-335.1526,2774.071;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;118;-26.01353,3049.849;Inherit;False;Property;_ActivateEffect2;Activate Effect;25;1;[Toggle];Create;False;0;0;0;False;1;Header(Erosion Effect);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;134;234.374,2300.472;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;-130.9281,2806.754;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;102;-496.2893,686.1247;Inherit;False;700.0386;236.5992;Soft Particle;3;41;52;46;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;155;-2116.799,1682.923;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;94;-640.2153,138.9301;Inherit;False;1229.412;410.1256;Comment;6;47;42;63;59;53;49;Alpha Calculations;1,0.08018869,0.08018869,1;0;0
Node;AmplifyShaderEditor.LerpOp;104;-1537.423,695.3126;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;-1949.034,1941.952;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;41;-446.2893,736.1245;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;61;129.5511,2720.088;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-590.793,263.8287;Float;False;Property;_AlphaCoefficient;Alpha Coefficient;34;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;121;264.908,3077.036;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;47;-608.414,352.955;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;156;-1767.844,1714.93;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;170;-1080.766,740.2755;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-246.5754,806.7234;Float;False;Property;_Depth;Depth;35;0;Create;True;0;0;0;False;1;Header(Soft Particle);False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;52;-64.25092,738.5437;Inherit;False;True;True;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-342.58,188.9301;Inherit;True;5;5;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;119;457.7251,3013.809;Inherit;True;2;0;FLOAT;0.5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;120;405.1655,2754.891;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;122;574.5152,2789.544;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-65.93159,208.7684;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;89;745.1734,2769.115;Inherit;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;59;77.59753,201.6248;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;97;-1056.662,-1561.03;Inherit;False;991.6815;587.8083;Comment;7;64;66;65;67;68;71;69;Fake fog;0.4622642,0.3292542,0.3292542,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;100;-477.4383,-385.1637;Inherit;False;971.92;401.9838;Comment;6;56;54;62;60;164;172;Emissive Calculations;0.1503288,0.9622642,0,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;166;-1353.895,581.5851;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;294.5184,178.2997;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-405.3097,-267.3083;Float;False;Property;_Glow;Glow;6;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;55;-1569.118,-537.1486;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;171;-1062.131,796.6245;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;64;-1006.662,-1511.03;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldSpaceCameraPos;66;-805.6336,-1297.472;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;65;-755.6506,-1478.283;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;167;-1180.159,522.7495;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;67;-500.4624,-1429.777;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;99;199.6466,45.92113;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;50;-1983.324,-726.2662;Float;False;Property;_SecondaryColor;Secondary Color;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4901961,0.4901961,0.4901961,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;126;1098.77,-415.2508;Inherit;False;Property;_CullMode;CullMode;2;0;Create;True;0;0;0;True;1;Enum(UnityEngine.Rendering.CullMode);False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;48;-2000.205,-560.2864;Float;False;Property;_MainColor;Main Color;4;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;123;1095.75,-331.0996;Inherit;False;Property;_SrcBlendMode;SrcBlendMode;0;0;Create;True;0;0;0;True;1;Enum(UnityEngine.Rendering.BlendMode);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;56;-414.0217,-173.8637;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;68;-502.6606,-1185.221;Float;False;Property;_FakeFogColor;Fake Fog Color;33;0;Create;True;0;0;0;False;1;Header(Fake Fog);False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;164;163.4812,-308.9923;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-232.7724,-1265.464;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;163;-1454.4,431.3057;Inherit;False;Property;_EmissiveGlow;EmissiveGlow;19;0;Create;True;0;0;0;False;0;False;1;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;124;1093.876,-247.187;Inherit;False;Property;_DstBlendMode;DstBlendMode;1;0;Create;True;0;0;0;True;1;Enum(UnityEngine.Rendering.BlendMode);False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-150.6694,-328.7426;Inherit;True;7;7;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;60.23334,-159.4141;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;71;-229.981,-1363.702;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;393.5727,-331.7436;Inherit;True;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;174;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;ShadowCaster;0;1;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;True;0;True;126;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;175;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;META;0;2;META;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;176;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;SceneSelectionPass;0;3;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;True;0;True;126;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=SceneSelectionPass;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;177;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;DepthForwardOnly;0;4;DepthForwardOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;True;0;True;126;True;False;False;False;False;0;False;-1;False;False;False;True;True;0;True;-7;255;False;-1;255;True;-8;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;False;False;True;1;LightMode=DepthForwardOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;179;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;DistortionVectors;0;6;DistortionVectors;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;1;False;-1;1;False;-1;False;False;False;False;False;False;False;True;0;True;126;False;False;False;False;True;True;0;True;-11;255;False;-1;255;True;-12;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;False;True;1;LightMode=DistortionVectors;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;178;1049.348,-144.2285;Float;False;False;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;Motion Vectors;0;5;Motion Vectors;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;True;0;True;126;False;False;False;False;True;True;0;True;-9;255;False;-1;255;True;-10;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;False;False;True;1;LightMode=MotionVectors;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;173;1049.348,-144.2285;Float;False;True;-1;2;UnityEditor.Rendering.HighDefinition.HDLitGUI;0;13;HDRP/Particles/MainUnlit;7f5cb9c3ea6481f469fdd856555439ef;True;Forward Unlit;0;0;Forward Unlit;9;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Transparent=Queue=0;True;5;0;True;1;0;True;123;0;True;124;1;0;True;123;0;True;124;False;False;False;False;False;False;False;False;True;0;True;126;False;False;False;False;True;False;0;True;-5;255;False;-1;255;True;-6;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;0;True;-24;True;0;True;-32;False;True;1;LightMode=ForwardOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;29;Surface Type;1;  Rendering Pass ;0;  Rendering Pass;1;  Blending Mode;0;  Receive Fog;1;  Distortion;0;    Distortion Mode;0;    Distortion Only;1;  Depth Write;1;  Cull Mode;0;  Depth Test;4;Double-Sided;0;Alpha Clipping;0;Motion Vectors;1;  Add Precomputed Velocity;0;Shadow Matte;0;Cast Shadows;1;DOTS Instancing;0;GPU Instancing;1;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;7;True;True;True;True;True;True;False;False;;False;0
WireConnection;76;0;72;3
WireConnection;76;1;72;4
WireConnection;114;0;113;1
WireConnection;114;1;113;2
WireConnection;80;0;75;0
WireConnection;80;1;74;0
WireConnection;79;0;77;0
WireConnection;79;1;76;0
WireConnection;81;0;79;0
WireConnection;81;2;114;0
WireConnection;81;1;80;0
WireConnection;82;1;81;0
WireConnection;83;0;82;2
WireConnection;83;1;82;3
WireConnection;117;0;113;3
WireConnection;116;0;113;4
WireConnection;84;3;83;0
WireConnection;112;1;111;0
WireConnection;86;0;84;0
WireConnection;86;1;117;0
WireConnection;86;2;116;0
WireConnection;8;0;3;3
WireConnection;8;1;3;4
WireConnection;7;1;4;0
WireConnection;88;0;87;0
WireConnection;88;1;86;0
WireConnection;88;2;112;0
WireConnection;11;0;8;0
WireConnection;11;1;7;0
WireConnection;17;0;12;1
WireConnection;17;1;11;0
WireConnection;73;0;88;0
WireConnection;6;1;1;0
WireConnection;5;0;2;1
WireConnection;5;1;2;2
WireConnection;9;0;5;0
WireConnection;9;1;6;0
WireConnection;20;0;7;0
WireConnection;26;0;12;0
WireConnection;26;1;17;0
WireConnection;31;0;25;0
WireConnection;31;1;26;0
WireConnection;16;0;10;1
WireConnection;16;1;9;0
WireConnection;29;0;23;0
WireConnection;29;1;22;0
WireConnection;29;2;20;0
WireConnection;35;0;31;0
WireConnection;35;2;30;0
WireConnection;35;1;29;0
WireConnection;19;0;10;0
WireConnection;19;1;16;0
WireConnection;13;0;6;0
WireConnection;44;1;35;0
WireConnection;145;0;19;0
WireConnection;145;1;144;0
WireConnection;27;0;15;0
WireConnection;27;1;14;0
WireConnection;27;2;13;0
WireConnection;151;0;145;0
WireConnection;32;0;19;0
WireConnection;32;2;21;0
WireConnection;32;1;27;0
WireConnection;34;0;18;0
WireConnection;28;0;18;0
WireConnection;28;1;24;3
WireConnection;103;1;93;0
WireConnection;129;0;44;3
WireConnection;38;0;33;0
WireConnection;38;1;34;0
WireConnection;165;1;162;0
WireConnection;152;0;151;0
WireConnection;152;1;151;1
WireConnection;37;1;32;0
WireConnection;128;0;44;2
WireConnection;110;0;108;0
WireConnection;110;1;103;0
WireConnection;140;1;137;0
WireConnection;36;0;28;0
WireConnection;139;0;137;0
WireConnection;147;1;146;0
WireConnection;135;0;127;0
WireConnection;130;1;127;0
WireConnection;43;0;38;0
WireConnection;43;1;36;0
WireConnection;141;0;138;0
WireConnection;141;1;140;0
WireConnection;149;1;152;0
WireConnection;149;2;147;0
WireConnection;105;0;110;0
WireConnection;142;0;139;0
WireConnection;142;1;37;1
WireConnection;169;0;165;0
WireConnection;168;0;169;0
WireConnection;168;1;105;0
WireConnection;159;0;149;0
WireConnection;98;0;43;0
WireConnection;153;0;149;0
WireConnection;132;0;133;0
WireConnection;132;1;130;0
WireConnection;143;0;141;0
WireConnection;143;1;142;0
WireConnection;158;0;154;0
WireConnection;136;0;135;0
WireConnection;136;1;57;0
WireConnection;51;0;43;0
WireConnection;51;1;40;0
WireConnection;134;0;132;0
WireConnection;134;1;136;0
WireConnection;58;0;98;0
WireConnection;58;1;51;0
WireConnection;155;0;153;0
WireConnection;155;1;154;0
WireConnection;104;1;143;0
WireConnection;104;2;168;0
WireConnection;157;0;158;0
WireConnection;157;1;159;0
WireConnection;61;0;134;0
WireConnection;61;1;43;0
WireConnection;61;2;58;0
WireConnection;121;0;118;0
WireConnection;156;0;155;0
WireConnection;156;1;157;0
WireConnection;170;0;104;0
WireConnection;52;1;41;0
WireConnection;52;0;46;0
WireConnection;49;0;44;1
WireConnection;49;1;170;0
WireConnection;49;2;42;0
WireConnection;49;3;47;4
WireConnection;49;4;156;0
WireConnection;119;1;121;0
WireConnection;120;0;61;0
WireConnection;120;1;118;0
WireConnection;122;0;120;0
WireConnection;122;1;119;0
WireConnection;53;0;49;0
WireConnection;53;1;52;0
WireConnection;89;0;122;0
WireConnection;59;0;53;0
WireConnection;59;1;53;0
WireConnection;166;0;165;0
WireConnection;166;1;143;0
WireConnection;63;0;59;0
WireConnection;63;1;89;0
WireConnection;55;0;50;0
WireConnection;55;1;48;0
WireConnection;55;2;44;1
WireConnection;171;0;104;0
WireConnection;65;0;64;0
WireConnection;167;0;163;0
WireConnection;167;1;166;0
WireConnection;67;0;65;0
WireConnection;67;1;66;0
WireConnection;99;0;89;0
WireConnection;164;0;60;0
WireConnection;164;1;172;0
WireConnection;69;0;71;0
WireConnection;69;1;68;0
WireConnection;60;0;55;0
WireConnection;60;1;54;0
WireConnection;60;2;56;0
WireConnection;60;3;171;0
WireConnection;60;4;44;1
WireConnection;60;5;156;0
WireConnection;60;6;56;4
WireConnection;172;0;56;0
WireConnection;172;1;167;0
WireConnection;71;0;67;0
WireConnection;62;0;69;0
WireConnection;62;1;164;0
WireConnection;62;2;99;0
WireConnection;62;3;52;0
WireConnection;173;0;62;0
WireConnection;173;2;63;0
ASEEND*/
//CHKSM=90A14D77633D3ED644B86E30956047D61FC8F8B4
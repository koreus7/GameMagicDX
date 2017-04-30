#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Our texture sampler
texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
};

// Uniforms
float time;
float2 res;
float4 col;
float seed;

// This data comes from the sprite batch vertex shader
struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TextureCordinate : TEXCOORD0;
};

float rand(float2 uv)
{
	return frac(cos(dot(uv, float2(12.9898, 4.1414)))  * 43758.5453);
}

float noise(float2 uv)
{
	uv.x += seed;
    uv.y += seed*0.5;
	float2 v = floor(uv);
	float2 f = pow(sin(frac(uv)*1.57079632679), float2(2.0,2.0));
	return lerp(lerp(rand(v), rand(v + float2(1.0, 0.0)), f.x),
		lerp(rand(v + float2(0.0, 1.0)), rand(v + float2(1.0, 1.0)), f.x), f.y)
		* 2.0 - 1.0;
}

float fbm(float2 uv)
{
	float fqi = 2.2;
	float amp = 0.47;

	const int octaves = 3;

	float r = 0.0;
	float f = 1.0;
	float a = 1.0;

	for (int o = 0; o < octaves; ++o)
	{
		r += a * abs(noise(uv * f));
		f *= fqi;
		a *= amp;
	}

	return r;
}


float Orb(VertexShaderOutput input)
{
   
	float2 p = (input.TextureCordinate - 0.5)*6.0;
	
    float c = 1.0/length(0.9*p + 0.1*p*abs(sin(time*0.0005)) );

	c*=step(0.01,c);
	
    return c;
}


float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float o = Orb(input);
	float2 p = input.TextureCordinate - 0.5;
	float2 uv = p;
	uv *= 8.0;
    uv +=  res*0.00001;

	float2 tv = float2(1.0, 1.0);
	tv = normalize(tv) * time * 0.0005;

	float2x2 b = {fbm(uv-tv*0.21+0.4), fbm(uv - tv*0.34 + 0.3),
		      fbm(uv-tv*0.16+0.1), fbm (uv-tv* 0.28+02) 
					   };

    float2 xyz = mul(b , uv);
	float c = fbm(xyz) ;//* min(Orb(p - 0. 5),0.1);

	float4 color = float4(0.9, 1.0, 1.1, 1.0);

	return color*col*c*(min(o,1.0) - 0.5);
}

// Compile our shader
technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0 PixelShaderFunction();
	}
}
#define DECLARE_TEXTURE(Name, index) \
    texture Name: register(t##index); \
    sampler Name##Sampler: register(s##index)

#define SAMPLE_TEXTURE(Name, texCoord) tex2D(Name##Sampler, texCoord)

uniform float Time;
uniform float2 CamPos;
uniform float2 Dimensions;
uniform float sizeMult = 1.0;
uniform float speedMult = 1.0;
uniform float4x4 TransformMatrix;
uniform float4x4 ViewMatrix;

DECLARE_TEXTURE(text, 0);

// Simple hash function for pseudo-random
float hash(float2 p) {
    return frac(sin(dot(p, float2(12.9898,78.233))) * 43758.5453);
}

// Smooth circle mask
float circle(float2 uv, float2 center, float radius) {
    float d = distance(uv, center);
    return smoothstep(radius, radius-0.5, d);
}

// HSV to RGB
float4 HsvToColor(float h, float s, float v) {
    float c = v * s;
    float x = c * (1 - abs(fmod(h*6, 2) - 1));
    float m = v - c;
    float3 rgb;
    if(h < 1.0/6) rgb = float3(c,x,0);
    else if(h < 2.0/6) rgb = float3(x,c,0);
    else if(h < 3.0/6) rgb = float3(0,c,x);
    else if(h < 4.0/6) rgb = float3(0,x,c);
    else if(h < 5.0/6) rgb = float3(x,0,c);
    else rgb = float3(c,0,x);
    return float4(rgb + m,1);
}

#define STAR_COUNT 20

float rand(float2 co) {
    return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
}

float4 SpritePixelShader(float2 uv : TEXCOORD0) : COLOR0
{
    float2 worldPos = uv * Dimensions + CamPos;
    float4 baseColor = SAMPLE_TEXTURE(text, uv);
    float4 color = float4(0,0,0,1); // black background
    //float sizeMult = 50.0;
    //float speedMult = 5.0;
    //float countMult = 0.5;
    float aspect = 4; // width / height ratio
    for(int i=0;i<STAR_COUNT;i++)
    {
        float2 starSeed = float2(i, i*7.0);

        // normalized star position [0,1]
        float2 starNorm = float2(rand(starSeed), rand(starSeed.yx));

        // scale to actual rectangle
        float2 starPos = starNorm * Dimensions;

        float speed = (5.0 + rand(starSeed.xy)*5.0) * speedMult;

        // gentle drift
        // speed in pixels per second
        float2 driftDir = float2(1.0, 1.0); // down-left
        float driftDistance = speed * 1;   // tweak how fast stars move

        // continuous motion based on Time
        starPos += driftDir * (Time * speedMult * 20.0); 

        // wrap stars back into screen bounds
        starPos = fmod(starPos, Dimensions); 

        // star radius
        float radius = (2.5 + rand(starSeed.xy)*2.5) * sizeMult;

        // compute difference and correct for aspect
        float2 diff = worldPos - starPos;
        diff.x *= aspect; // scale x to match y, keeps circles round

        float d = length(diff);

        // circular mask
        float mask = saturate(1.0 - d / radius);

        // star color
        float hue = rand(starSeed.xy*3.0);
        float4 starColor = HsvToColor(hue, 0.6, 1.0);

        color += starColor * mask;
    }

    return baseColor * saturate(color);
}






void SpriteVertexShader(inout float4 color    : COLOR0,
                        inout float2 texCoord : TEXCOORD0,
                        inout float4 position : SV_Position)
{
    position = mul(position, ViewMatrix);
    position = mul(position, TransformMatrix);
}

technique Shader
{
    pass pass0
    {
        VertexShader = compile vs_3_0 SpriteVertexShader();
        PixelShader = compile ps_3_0 SpritePixelShader();
    }
}

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
uniform float4 backColor = (1, 1, 0, 1);

DECLARE_TEXTURE(text, 0);

float YoYo(float value)
{
    if (value <= 0.5)
    {
        return value * 2;
    }

    return 1 - (value - 0.5) * 2;
}


float4 HsvToColor(float hue, float s, float v)
{
    int num = (int)(hue * 360.0);
    float num2 = s * v;
    float num3 = num2 * (1.0 - abs((float)num / 60.0 % 2.0 - 1.0));
    float num4 = v - num2;
    if (num < 60)
    {
        return float4(num4 + num2, num4 + num3, num4, 1);
    }

    if (num < 120)
    {
        return float4(num4 + num3, num4 + num2, num4, 1);
    }

    if (num < 180)
    {
        return float4(num4, num4 + num2, num4 + num3, 1);
    }

    if (num < 240)
    {
        return float4(num4, num4 + num3, num4 + num2, 1);
    }

    if (num < 300)
    {
        return float4(num4 + num3, num4, num4 + num2,1 );
    }

    return float4(num4 + num2, num4, num4 + num3, 1);
}

uniform float amount = 40.0;

float fract(float x) 
{
    return x - floor(x);
}

float4 SpritePixelShader(float2 uv : TEXCOORD0) : COLOR0
{
    //float3 backColor = float3(5,0,0);
    float2 worldPos = (uv * Dimensions) + CamPos;
    float num = 280.0 * sizeMult;
    float stripeScale = sizeMult; // how many diagonal stripes you want
    float value = frac((uv.x + uv.y) * stripeScale + Time * 0.5 * (speedMult / 50)); //% num / num;;
    float4 rainbowCol = HsvToColor(value, 1, 1);
    float2 Nuv = uv*5;
	float a = fract(sin(dot(Nuv, float2(12.9898, 78.233) * Time)) * 438.5453);
    //float a = fract(sin(dot(Nuv, float2(12.9898, 78.233) * Time)) * 43758.5453) * 1.9;
    float4 color = SAMPLE_TEXTURE(text, uv);
	//color.rgb *= pow(a, amount * 0.1);
    color.rgb *= rainbowCol;
    return color;
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

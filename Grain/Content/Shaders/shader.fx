float AspectRatio = 0.0;
float Time = 0.0;

float3 palette(float t)
{
    float3 a = float3(0.5, 0.5, 0.5);
    float3 b = float3(0.5, 0.5, 0.5);
    float3 c = float3(1.0, 1.0, 1.0);
    float3 d = float3(0.263, 0.416, 0.557);

    return a + b * cos((c * t + d) * 6.28318);
}

float4 pixel_shader(float4 position : SV_Position, float4 color : COLOR0, float2 uv : TEXCOORD0) : COLOR
{
    float2 xy = float2(
        (uv.x * 2.0 - 1.0) * AspectRatio,
        (1.0 - uv.y) * 2.0 - 1.0
    );

    float2 xyFraction = frac(xy * 2.0) - 0.5;

    float d = length(xyFraction);
    float3 c = palette(length(xy) + Time);

    d = sin(d * 8.0 + Time) / 8.0;
    d = abs(d);

    d = 0.02 / d;

    c *= d;

    return float4(c, 1.0);
}

technique
{

    pass
    {
        PixelShader = compile ps_3_0 pixel_shader();
    }

}

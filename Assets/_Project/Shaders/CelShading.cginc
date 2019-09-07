#ifndef CelShading
#define CelShading
 
static const int _TOON_DEFAULT_STEPS = 4;
static const float _TOON_EDGE_SMOOTHING_RATIO = 0.1;
 
float CalcCelShadingSampled(float ndotl, sampler2D toonlut) {
	if (ndotl < 0) ndotl = 0;
    return tex2D(toonlut, ndotl).x;
}
float CalcCelShadingStepped(float ndotl, int steps) {
 
    // Make sure shading can never be below 0
	if (ndotl <= 0) { return 0; }
 
    // Scale the value to be in the interval ]0;steps]
    float scaled = ndotl * steps;
    // Round to the nearest shading step
    float nearestStep = round(scaled);
    // Normalize back to range [0;1]
    float normalized = nearestStep / steps;
    // Return the normalized value
    return normalized;
}
float CalcCelShadingSteppedDefault(float ndotl) {
    return CalcCelShadingStepped(ndotl, _TOON_DEFAULT_STEPS);
}
 
#endif